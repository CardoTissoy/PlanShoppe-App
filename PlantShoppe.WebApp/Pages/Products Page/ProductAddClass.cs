using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PlantShoppe.WebApp.Entities;
using PlantShoppe.WebApp.Services;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PlantShoppe.WebApp.Pages.Products_Page
{
    public class ProductAddClass : ComponentBase
    {
        [Parameter]
        public string ProductId { get; set; } = string.Empty;
        public ProductsViewModel ProductView { get; set; } = new ProductsViewModel();
        
        // Inject dependencies
        [Inject]
        public NavigationManager _navigationManager { get; set; } = null!;
        [Inject]
        public IProductRepository _productRepository { get; set; } = null!;
        [Inject]
        public HttpClient _httpClient { get; set; } = null!;


        protected string text = string.Empty;
        protected int MAX_TEXT_COUNT = 255;

        // used to store state of screen
        protected string Message = string.Empty;
        protected string Status = string.Empty;
        protected bool Saved;

        public string btnText = string.Empty;

        

        // Upload Photo
        [Parameter]
        public long maxImageSize { get; set; } = 20 * 1024000L;
        public List<ProductsModel> ImageUpload { get; set; } = new();
        public string AcceptedFileFormat { get; set; } = "image/png, image/gif, image/jpg";

        protected async Task AddProduct() => await _productRepository.AddProduct(ProductView);

        // Handles valid data submission
        protected async Task HandleValidSubmit()
        {
            Saved = false;
            if (ProductId == null)
            {
                await _productRepository.AddProduct(ProductView);
                Status = "alert-success";
                Message = "New message has been added";
                Saved = true;
            }
        }

        // Handles invalid data submission
        protected void HandleInvalidSubmit()
        {
            Status = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        // Handles Upload Image
        public async Task HandleFileInputChanged(InputFileChangeEventArgs e)
        {

            var image = e.File;
            var imageFormat = AcceptedFileFormat.Contains(image.ContentType) ? image.ContentType : AcceptedFileFormat.Split(',').First();
            var imageFile = await image.RequestImageFileAsync(imageFormat, 2048, 2048);

            var ms = new MemoryStream();
            await imageFile.OpenReadStream(maxImageSize).CopyToAsync(ms);
            var buffer = ms.GetBuffer();

            ImageUpload.Add(new ProductsModel
            
            {
                Name= imageFile.Name,
                ContentType= imageFile.ContentType,
                Content= buffer,
                IsImage= true,
                ImageString= GetImageString(buffer,imageFormat),
            });
            await UploadImage();
        }
        private string GetImageString(byte[] content, string contentType)
        {
            return $"data: {contentType}; base64, {Convert.ToBase64String(content)}";
        }
        public async Task UploadImage()
        {
            using var formData = new MultipartFormDataContent();
            foreach (var file in ImageUpload)
            {
                var content = new ByteArrayContent(file.Content);
                content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                formData.Add(content, "UploadAsync", file.Name);
            }
        }
    }
}
