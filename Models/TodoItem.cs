using System.ComponentModel.DataAnnotations;

namespace WebApiSample.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime? LastUpdatedDate { get; set;}
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
