namespace Sulpak.TestAssigment.Domain.Entities;

public class Price
{
    public int Id { get; set; }
    public string Article  { get; set; }
    public decimal Cost { get; set; } 
    public string PriceType { get; set; } 
    public List<string> Departments { get; set; } 
    public int Priority { get; set; }
}
