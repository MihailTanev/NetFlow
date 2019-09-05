namespace NetFlow.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Common.Messages.Blog;
    using NetFlow.Data.Models;
    using NetFlow.Services.Blog.Interface;
    using NetFlow.Services.Cloudinary;
    using NetFlow.Services.HtmlSanitizer;
    using NetFlow.Web.ViewModels.Post;
    using System.Threading.Tasks;
    using X.PagedList;

    public class PostsController : BasePostsController
    {
        private readonly IBlogPostService blogPostService;
        private readonly IHtmlSanitizerService htmlSanitizerService;
        private readonly UserManager<User> userManager;
        private readonly ICommentService commentService;
        private readonly ICloudinaryService cloudinaryService;


        public PostsController(ICloudinaryService cloudinaryService, ICommentService commentService, IBlogPostService blogPostService, IHtmlSanitizerService htmlSanitizerService, UserManager<User> userManager)
        {
            this.cloudinaryService = cloudinaryService;
            this.commentService = commentService;
            this.userManager = userManager;
            this.htmlSanitizerService = htmlSanitizerService;
            this.blogPostService = blogPostService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page)
        {
            var users = await this.blogPostService.GetAllPostsAsync();

            var pageNumber = page ?? 1;

            var model = new PostViewModel
            {
                Posts = await users.ToPagedListAsync(pageNumber, 10)
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

            this.TempData[BlogMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{model.Title}' {BlogMessagesConstants.POST_WAS_CREATED}";

            return RedirectToAction(nameof(Index), new { AreaConstants.BLOG_AREA });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int postId)
        {
            var post = await this.blogPostService.GetPostByIdAsync(postId);

            var model = new PostDetailsViewModel
            {
                Id = postId,
                Title = post.Title,
                Picture = post.Picture,
                Content = post.Description,
                CreatedDate = post.CreatedDate,
                FirstName = post.PublisherFirstName,
                LastName = post.PublisherLastName,
                Comments = await this.commentService.GetAllCommentsAsync(postId),
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

        public async Task<IActionResult> Delete(int postId)
        {
            var post = await this.blogPostService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var model = new DeletePostViewModel
            {
                PostId = post.Id,
                PostTitle = post.Title
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(DeletePostViewModel model, int postId)
        {
            var post = await this.blogPostService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.blogPostService.DeletePost(post);

                this.TempData[BlogMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $"Post '{post.Title}' {BlogMessagesConstants.POST_WAS_DELETED}";

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.BLOG_AREA });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int postId)
        {
            var post = await this.blogPostService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            EditPostViewModel model = new EditPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = this.htmlSanitizerService.Sanitize(post.Description),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model, int postId)
        {
            var post = await this.blogPostService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = model.Title;
            post.Description = model.Description;

            if (ModelState.IsValid)
            {
                await this.blogPostService.UpdatePost(post);

                this.TempData[BlogMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{post.Title}' {BlogMessagesConstants.POST_WAS_UPDATED}";

                return this.RedirectToAction(nameof(Index), new { postId });
            }
            else
            {
                return this.View(model);
            }
        }
    }
    
}