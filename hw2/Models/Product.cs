namespace hw2.Models;

public class Product
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Weight { get; set; }
    public TypeProduct TypeProduct { get; set; }
    public DateTime DateCreation { get; set; }
    public int WarehouseNumber { get; set; }
}