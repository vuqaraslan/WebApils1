using System.Linq.Expressions;
using WebApi_LS1.Entities;
using WebApi_LS1.Repositories.Abstract;
using WebApi_LS1.Services.Abstract;

namespace WebApi_LS1.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository? _studentRepository;

        public StudentService(IStudentRepository? studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task Add(Student entity)
        {
            await _studentRepository.Add(entity);
        }

        public async Task Delete(Student entity)
        {
            await _studentRepository.Delete(entity);
        }

        public async Task<Student> Get(Expression<Func<Student, bool>> predicate)
        {
            var item = await _studentRepository.Get(predicate);
            return item;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentRepository.GetAll();
        }

        public async Task Update(Student entity)
        {
            await _studentRepository.Update(entity);
        }
    }
}
