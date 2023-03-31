
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using PlantShoppe.WebApp.Entities;

namespace PlantShoppe.WebApp.Services
{
    public interface IProductRepository
    {
        List<ProductsModel> ProductList { get; set; }
        Task GetAllProducts();
        Task<ProductsModel> GetProductById(int ProductId);
        Task AddProduct(ProductsViewModel product);
        Task UpdateProduct(ProductsModel product);
        Task DeleteProduct(int ProductId);
        Task<string> UploadAsync(MultipartFormDataContent file);
    }
}
