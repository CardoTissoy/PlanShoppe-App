using Microsoft.AspNetCore.Components;
using PlantShoppe.WebApp.Entities;
using PlantShoppe.WebApp.Services;

namespace PlantShoppe.WebApp.Pages.Products_Page
{
    public class ProductEditClass: ComponentBase
    {
        [Parameter]
        public string ProductId { get; set; } = string.Empty;
        public ProductsModel Product = new ProductsModel();

        // Inject Services
        [Inject]
        public IProductRepository _productRepository{ get; set; } = null!;
        [Inject]
        public NavigationManager _navigationManager { get; set; } = null!;

        // used to store state of screen
        protected string Message = string.Empty;
        protected string Status = string.Empty;
        protected bool Saved;

        public string btnText = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            if (ProductId == null)
            {
                btnText = "Save";
                new ProductsModel();
            }
            else
            {
                btnText = "Update";
                Product = await _productRepository.GetProductById(int.Parse(ProductId));
            }

        }
        // Handles valid data submission
        protected async Task HandleValidSubmit()
        {
           
            Saved = false;
            if (ProductId != null)
            {

                await _productRepository.UpdateProduct(Product);
                Status = "alert-success";
                Message = "New message has been updated";
                Saved = true;
            }
        }
        
        protected void HandleInvalidSubmit()
        {
            Status = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task UpdateProduct() => await _productRepository.UpdateProduct(Product);


    }
}
