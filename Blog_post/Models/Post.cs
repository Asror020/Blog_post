using Blog_post.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_post.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        [ForeignKey("Status")]
        public StatusEnum StatusId { get; set; }
        public Status Status { get; set; }  
    }
}
