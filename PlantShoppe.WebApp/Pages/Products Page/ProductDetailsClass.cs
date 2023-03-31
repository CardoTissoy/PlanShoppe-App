using Microsoft.AspNetCore.Components;
using PlantShoppe.WebApp.Entities;
using PlantShoppe.WebApp.Services;

namespace PlantShoppe.WebApp.Pages
{
    public class ProductDetailsClass: ComponentBase
    {
        
        
        public ProductsModel Product { get; set; } = new ProductsModel();
        [Parameter]
        public string ProductId { get; set; } = null!;

        [Inject]
        public IProductRepository productRepository { get; set; } = null!;


        protected async override Task OnInitializedAsync() => Product = await productRepository.GetProductById(int.Parse(ProductId));
    }
}
