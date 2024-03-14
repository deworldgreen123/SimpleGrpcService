namespace hw2.Models;

public class Filter
{
    public TypeProduct TypeProduct { get; set; }
    public DateTime DateCreation { get; set; }
    public int WarehouseNumber { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}