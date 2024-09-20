using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Sulpak.TestAssigment.Domain.Entities;

public class Price
{
    public Guid Id { get; set; }

    /// <summary>
    /// Article
    /// </summary>
    [JsonProperty("article")]
    public int ProductId { get; set; }

    public Product Product { get; set; }
    private decimal _cost;

    public decimal Cost
    {
        get => _cost;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Стоимость должна быть больше 0.");
            _cost = value;
        }
    }

    public int PriceTypeId { get; set; }
    public PriceType PriceType { get; set; }
    public ICollection<int> Departments { get; set; }
    public int Priority { get; set; }

    public void ValidateOrder()
    {
        if (ProductId.Equals(Guid.Empty))
            throw new ArgumentException("Артикул товара обязателен.");

        if (PriceTypeId.Equals(Guid.Empty))
            throw new ArgumentException("Тип цены обязателен.");
    }
}