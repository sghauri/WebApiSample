using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime? LastUpdatedDate { get; set;}
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
