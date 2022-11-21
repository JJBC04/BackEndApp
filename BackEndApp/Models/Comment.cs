using System.ComponentModel.DataAnnotations;

namespace BackEndApp.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
