namespace Academy.Models;

public class Class : BaseModel
{
    public string Title { get; set; } = default!;
    public Professor Professor { get; set; } = default!;
    public List<Student> Students { get; set; } = default!;
}