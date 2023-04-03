using System;
using System.Collections.Generic;

namespace PlantShoppe.WebApp.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ProductPhoto { get; set; } = null!;
}
