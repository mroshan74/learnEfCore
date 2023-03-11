namespace Academy.Models;

public class Student : BaseModel
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public List<Class> Classes { get; set; } = default!;
}