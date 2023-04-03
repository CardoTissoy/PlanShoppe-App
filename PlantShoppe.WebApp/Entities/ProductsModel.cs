
using System.ComponentModel.DataAnnotations;

public class ProductsModel
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

    public string ProductPhoto { get; set; } = null!;
    public string Name { get; set; } = null!;
    public byte[] Content { get; set; } = new byte[0];
    public string ContentType { get; set; } = string.Empty;
    public bool IsImage { get; set; }
    public string ImageString { get; set; } = string.Empty;
}

