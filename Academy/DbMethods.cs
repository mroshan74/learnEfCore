using Academy.Database;
using Academy.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy;

public class DbMethods
{
    private readonly ApplicationDbContext _dbContext;

    public DbMethods(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ProcessUpdate(DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        // var stud = _dbContext.Students.Include(s => s.Classes).First();
        
        // AsNoTracking() removes the SaveTrack reference
        _dbContext.Dispose();
        var dbContext = new ApplicationDbContext(dbContextOptions);
        var stud = dbContext.Students.AsNoTracking().First();
        var studentUntracked = new Student()
        {
            Id = stud.Id,
            FirstName = "Alan",
            LastName = "Miss"
        };
        dbContext.Attach(studentUntracked);
        dbContext.Students.Entry(studentUntracked).State = EntityState.Modified;
        dbContext.SaveChanges();
    }

    public void ProcessSelect()
    {
        var pro = _dbContext.Professors
            .Include(p => p.Address)
            .Single(p => p.FirstName == "John");
        
        var stud = _dbContext.Students
            .Include(s => s.Classes)
            .Where(s => s.FirstName.StartsWith("L"))
            .ToList();
        
        Console.WriteLine(pro);
        Console.WriteLine(stud);
    }

    public void ProcessDelete()
    {
        var studs = _dbContext.Students.ToList();
        var pros = _dbContext.Professors.ToList();
        var adds = _dbContext.Addresses.ToList();
        var cls = _dbContext.Classes.ToList();
        
        _dbContext.RemoveRange(studs);
        _dbContext.RemoveRange(pros);
        _dbContext.RemoveRange(adds);
        _dbContext.RemoveRange(cls);

        _dbContext.SaveChanges();
    }
    
    public void ProcessInsert()
    {
        var add1 = new Address() { HouseNumber = 12, City = "Kozhikode", Street = "Mukkam", Zip = "673001"};
        var add2 = new Address() { HouseNumber = 234, City = "Trivandrum", Street = "Palayam", Zip = "636701"};
        var add3 = new Address() { HouseNumber = 45, City = "Cochin", Street = "Wellington", Zip = "639587"};
    
        var pr1 = new Professor() { FirstName = "John", LastName = "Doe", Address = add1 };
        var pr2 = new Professor() { FirstName = "Big", LastName = "Brain", Address = add2 };
    
        var std1 = new Student() { FirstName = "Caramel", LastName = "Mix", Address = add3 };
        var std2 = new Student() { FirstName = "Lehman", LastName = "Scott", Address = add3 };
        var std3 = new Student() { FirstName = "Man", LastName = "River", Address = add3 };
        var std4 = new Student() { FirstName = "Love", LastName = "Rose", Address = add3 };
    
        var cl1 = new Class() { Title = "English", Professor = pr1, Students = new List<Student>(){ std1, std3} };
        var cl2 = new Class() { Title = "Math", Professor = pr2, Students = new List<Student>(){ std1, std2 }};
        var cl3 = new Class() { Title = "Social", Professor = pr1, Students = new List<Student>(){ std3, std4 }};

        _dbContext.Addresses.Add(add1);
        _dbContext.Addresses.Add(add2);
        _dbContext.Addresses.Add(add3);

        _dbContext.Professors.Add(pr1);
        _dbContext.Professors.Add(pr2);

        _dbContext.Students.Add(std1);
        _dbContext.Students.Add(std2);
        _dbContext.Students.Add(std3);
        _dbContext.Students.Add(std4);

        _dbContext.Classes.Add(cl1);
        _dbContext.Classes.Add(cl2);
        _dbContext.Classes.Add(cl3);

        _dbContext.SaveChanges();
    }
}