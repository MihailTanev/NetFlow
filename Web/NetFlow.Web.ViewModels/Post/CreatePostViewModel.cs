namespace NetFlow.Web.ViewModels.Post
{
    using NetFlow.Common.GlobalConstants;
    using System.ComponentModel.DataAnnotations;

    public class CreatePostViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(PostsConstants.POST_TITLE_MIN_LENGTH)]
        [MaxLength(PostsConstants.POST_TITLE_MAX_LENGTH)]
        public string Title { get; set; }

        [Required]
        [MinLength(PostsConstants.POST_MIN_DESCRIPTION_LENGTH)]
        [MaxLength(PostsConstants.POST_MAX_DESCRIPTION_LENGTH)]
        public string Content { get; set; }
    }
}
