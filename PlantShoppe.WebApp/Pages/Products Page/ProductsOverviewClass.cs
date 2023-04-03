using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlantShoppe.WebApp.Entities;
using PlantShoppe.WebApp.Pages.Products_Page;
using PlantShoppe.WebApp.Services;

namespace PlantShoppe.WebApp.Pages
{
    public class ProductsOverviewClass: ComponentBase
    {
        public ProductsModel ProductView { get; set; } = new ProductsModel();
        
        protected string modalTitle { get; set; } = string.Empty;
        protected Boolean isDelete = false;
        protected Boolean isAdd = false;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; } = null!;

        [Inject]
        public NavigationManager _navigationManager { get; set; } = null!;
        [Inject]
        public IProductRepository  _productRepository { get; set; } = null!;

        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/identity/account/login");
            }
            await _productRepository.GetAllProducts();
        }

        public void ProductDetails(int ProductId) => _navigationManager.NavigateTo($"/details/{ProductId}");
        public void EditProduct(int ProductId) => _navigationManager.NavigateTo($"/edit/{ProductId}");
        public  async Task DeleteConfirm(int ProductId)
        {
            ProductView = await _productRepository.GetProductById(ProductId);
            this.isDelete = true;
        }

        public void AddConfirm()
        {
            _navigationManager.NavigateTo($"/add");

        }

        public void closeModal()
        {
            this.isAdd = false;
            this.isDelete = false;
        }
        public async Task DeleteProductConfirm(int ProductId)
        {
            await Task.Run(() =>
            {
                _productRepository.DeleteProduct(ProductId);
            });
            this.isDelete = false;
            await GetProductList();
        }
       protected async Task GetProductList()
        {
             await _productRepository.GetAllProducts();
        }



    }
}
