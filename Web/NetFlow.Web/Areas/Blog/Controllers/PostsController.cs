namespace NetFlow.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.Messages.Blog;
    using NetFlow.Data.Models;
    using NetFlow.Services.Blog.Interface;
    using NetFlow.Services.Cloudinary;
    using NetFlow.Services.HtmlSanitizer;
    using NetFlow.Web.ViewModels.Post;
    using System.Threading.Tasks;

    public class PostsController : BasePostsController
    {
        private readonly IBlogPostService blogPostService;
        private readonly IHtmlSanitizerService htmlSanitizerService;
        private readonly UserManager<User> userManager;
        private readonly ICommentService commentService;
        private readonly ICloudinaryService cloudinaryService;


        public PostsController(ICloudinaryService cloudinaryService, ICommentService commentService, IBlogPostService blogPostService, IHtmlSanitizerService htmlSanitizerService, UserManager<User>userManager)
        {
            this.cloudinaryService = cloudinaryService;
            this.commentService = commentService;
            this.userManager = userManager;
            this.htmlSanitizerService = htmlSanitizerService;
            this.blogPostService = blogPostService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new PostViewModel
            {
                Posts = await this.blogPostService.GetAllPostsAsync()
            };

            return this.View(model);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(CreatePostViewModel model)
        {
            string pictureUrl = await this.cloudinaryService.UploadPostPictureAsync(model.Picture, model.Title);

            model.Content = this.htmlSanitizerService.Sanitize(model.Content);

            var authorId = this.userManager.GetUserId(User);

            await this.blogPostService.CreatePostAsync(model.Title, model.Content, authorId, pictureUrl);

            this.TempData[BlogMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = BlogMessagesConstants.POST_WAS_CREATED;

            return RedirectToAction(nameof(Add));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var post = await this.blogPostService.GetPostByIdAsync(id);

            var model = new PostDetailsViewModel
            {
                Id = id,
                Title = post.Title,
                Picture = post.Picture,
                Content = post.Description,
                CreatedDate = post.CreatedDate,
                FirstName = post.PublisherFirstName,
                LastName = post.PublisherLastName,
                Comments = await this.commentService.GetAllCommentsAsync(id),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(PostDetailsViewModel model)
        {
            model.Content = this.htmlSanitizerService.Sanitize(model.Description);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest();
            }

            await this.commentService.CreateCommentAsync(userId, model.Id, model.Description);

            return RedirectToAction(nameof(Details), new { model.Id, model.Title });
        }

    }
}