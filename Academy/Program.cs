using System.Linq.Expressions;
using Academy;
using Academy.Database;
using Academy.Models;
using Academy.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

/*
 * #(fix) links - Read more
 * https://stackoverflow.com/questions/39157781/the-configuration-file-appsettings-json-was-not-found-and-is-not-optional
 * https://stackoverflow.com/questions/38114761/asp-net-core-configuration-for-net-core-console-application
 * https://stackoverflow.com/questions/31885912/how-to-read-values-from-config-json-in-console-application
 * https://stackoverflow.com/questions/53605249/json-configuration-in-full-net-framework-console-app
 */

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", false);

IConfigurationRoot configuration = builder.Build();
// var dbString = configuration[Constants.ConnectionString];
var dbString = configuration.GetConnectionString("DefaultSQLConnection");

var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseMySql(dbString, ServerVersion.AutoDetect(dbString))
    .Options;

var dbContext = new ApplicationDbContext(options);
dbContext.Database.Migrate();

var dbAct = new DbMethods(dbContext);

dbAct.ProcessDelete();
dbAct.ProcessInsert();
// dbAct.ProcessSelect();
// dbAct.ProcessUpdate(options);
dbContext.Dispose();

ProcessRepository();

async void ProcessRepository()
{
    dbContext = new ApplicationDbContext(options);
    var repository = new GenericRepository<Student>(dbContext);
    
    //simple select
    var students = await repository.GetAsync(null, null);
    var student = await repository.GetByIdAsync(students.First().Id);
    
    //includes
    student = await repository.GetByIdAsync(student.Id, (student) => student.Address, (student) => student.Classes);
    
    //filters
    Expression<Func<Student, bool>> filter = (student) => student.FirstName == "Man";
    students = await repository.GetFilteredAsync(new[] { filter }, null, null);
}