
public class ProductsModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public string ProductPhoto { get; set; } = null!;
    public string Name { get; set; } = null!;
    public byte[] Content { get; set; } = new byte[0];
    public string ContentType { get; set; } = string.Empty;
    public bool IsImage { get; set; }
    public string ImageString { get; set; } = string.Empty;
}

