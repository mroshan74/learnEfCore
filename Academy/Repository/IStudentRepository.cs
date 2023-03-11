using Academy.Models;

namespace Academy.Repository;

public interface IStudentRepository
{
    Task<Guid> CreateStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task<Student?> GetStudentByIdAsync(Guid studentId);
    Task<List<Student>> GetStudentsAsync();
    Task DeleteStudentAsync(int studentId);
}