using Microsoft.AspNetCore.Mvc;
using WebApi_LS1.Dtos;
using WebApi_LS1.Entities;
using WebApi_LS1.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_LS1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService? _studentService;

        public StudentController(IStudentService? studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            var items = await _studentService.GetAll();
            var dataToReturn = items.Select(st => new StudentDto
            {
                Id = st.Id,
                Fullname=st.Fullname,
                SeriaNo = st.SeriaNo,   
                Age = st.Age,   
                Score = st.Score
            });
            return dataToReturn;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _studentService.Get(st => st.Id == id);
            if (item == null)
            {
                return NotFound("Student is not finded !");
            }
            var stDto = new StudentDto
            {
                Id=item.Id,
                Fullname=item.Fullname,
                SeriaNo = item.SeriaNo,
                Age = item.Age,
                Score = item.Score
            };
            return Ok(stDto);
        }

        // POST api/<StudentController>
        [HttpPost]
        //[Produces("application/json")]
        //[Consumes("text/vcard")]
        public async Task<IActionResult> Post([FromBody] StudentAddDto stDto)
        {
            var student = new Student
            {
                Fullname = stDto.Fullname,
                Age= stDto.Age,
                Score = stDto.Score,
                SeriaNo= stDto.SeriaNo
            };
            await _studentService.Add(student);
            return Ok(stDto);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
