using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi_LS1.Data;
using WebApi_LS1.Entities;
using WebApi_LS1.Repositories.Abstract;

namespace WebApi_LS1.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext? _studentDbContext;

        public StudentRepository(StudentDbContext? studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task Add(Student entity)
        {
            await _studentDbContext.AddAsync(entity);
            await _studentDbContext.SaveChangesAsync();
        }

        public async Task Delete(Student entity)
        {
            await Task.Run(() =>
            {
                _studentDbContext.Remove(entity);
            });
            await _studentDbContext.SaveChangesAsync();
        }

        public async Task<Student> Get(Expression<Func<Student, bool>> predicate)
        {
            var student = await _studentDbContext.Students.FirstOrDefaultAsync(predicate);
            return student;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await Task.Run(() =>
            {
                return _studentDbContext.Students;
            });
        }

        public async Task Update(Student entity)
        {
            await Task.Run(() =>
            {
                _studentDbContext.Students.Update(entity);
            });
            _studentDbContext.SaveChangesAsync();
        }
    }
}
