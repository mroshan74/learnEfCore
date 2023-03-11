using Academy.Database;
using Academy.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> CreateStudentAsync(Student student)
    {
        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return student.Id;
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _dbContext.Students.Attach(student);
        _dbContext.Entry(student).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<Student?> GetStudentByIdAsync(Guid studentId)
    {
        return await _dbContext.Students.FindAsync(studentId).ConfigureAwait(false);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _dbContext.Students.ToListAsync().ConfigureAwait(false);
    }

    public async Task DeleteStudentAsync(int studentId)
    {
        var student = await _dbContext.Students.FindAsync(studentId).ConfigureAwait(false);
        if (student != null)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        throw new InvalidOperationException("Id not found !");
    }
}