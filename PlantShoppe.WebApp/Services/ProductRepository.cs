using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlantShoppe.WebApp.Entities;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace PlantShoppe.WebApp.Services
{
    public class ProductRepository: IProductRepository
    {
        private readonly ILogger<IProductRepository> _logger;
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;
        public ProductRepository(HttpClient httpClient, NavigationManager navigationManager, ILogger<IProductRepository> logger)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _logger = logger;
        }
        public List<ProductsModel> ProductList { get; set; } = new List<ProductsModel>();
        protected Boolean isDelete = false;
   

        public async Task GetAllProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductsModel>>("/api/Product");
            if (result != null)
            {
                ProductList = result;
            }
        }
        public async Task<ProductsModel> GetProductById(int ProductId)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductsModel>($"/api/Product/{ProductId}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("Product not found");
        }
        public async Task AddProduct(ProductsViewModel productsViewModel)
        {
            ProductsModel products = new ProductsModel
            {
                ProductName = productsViewModel.ProductName,
                ProductDescription = productsViewModel.ProductDescription,
                ProductType = productsViewModel.ProductType,
                ProductPhoto = await UploadAsync(productsViewModel.ProductPhoto)
            };

            var addProduct = await _httpClient.PostAsJsonAsync("/api/Product", products);
            HttpResponseMessage response = addProduct;
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("productsoverview");
                }
            }
            catch (Exception )
            {
                //
            }
                
        }
        public async Task UpdateProduct(ProductsModel product)
        {
            var updateProduct = await _httpClient.PutAsJsonAsync($"/api/Product", product);

            HttpResponseMessage response = updateProduct;
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("productsoverview");
                }
            }
            catch (Exception)
            {
                // Not Found error
            }

        }
        public async Task DeleteProduct(int ProductId)
        {
            var deleteProduct = await _httpClient.DeleteAsync($"/api/Product/{ProductId}");
            HttpResponseMessage response = deleteProduct;
            try
            {
                if (response.IsSuccessStatusCode)
                {
                     _navigationManager.NavigateTo("productsoverview");
                    //this.isDelete = false;

                }
            }
            catch (Exception)
            {
                // Not Found error
            }
        }

        public async Task<string> UploadAsync([FromForm] MultipartFormDataContent file)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Product/Images", file);
            return await response.Content.ReadAsStringAsync();
             

        }
    }
}
