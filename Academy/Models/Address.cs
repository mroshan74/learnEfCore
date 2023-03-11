namespace Academy.Models;

public class Address : BaseModel
{
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Zip { get; set; } = default!;
    public int HouseNumber { get; set; }
}