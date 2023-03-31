using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace PlantShoppe.WebApp.Entities
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;

        public string ProductType { get; set; } = null!;
        public MultipartFormDataContent ProductPhoto { get; set; } = null!;

    }
}
