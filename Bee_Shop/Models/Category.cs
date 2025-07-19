using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bee_Shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        public int? SupplyId { get; set; }

        public int? CategoryPosition { get; set; }

        [Required]
        public string Slug { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("SupplyId")]
        public virtual Category? ParentCategory { get; set; }

        public virtual ICollection<Category>? Children { get; set; }
    }
}
