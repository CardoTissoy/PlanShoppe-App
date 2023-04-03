using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace PlantShoppe.WebApp.Entities
{
    public class ProductsViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 255, MinimumLength = 10,
        ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string ProductDescription { get; set; } = null!;
        [Required]
        public string ProductType { get; set; } = null!;
        public MultipartFormDataContent ProductPhoto { get; set; } = null!;

    }
}
