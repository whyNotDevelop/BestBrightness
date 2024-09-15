using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestBrightness.Data
{
    public class Product
    {
        [Key]
        public long Barcode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Size { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        // Navigation property
        //public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
