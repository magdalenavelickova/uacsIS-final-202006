using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using uascWebApi.Models;

namespace uascWebApi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public static List<Student> students = new List<Student> {
            new Student { FirstName= "Mary" , LastName= "Johnson", Id="1"},
            new Student { FirstName= "John" , LastName= "Smith", Id="2"}
        };
        
        // GET api/student/GetAll
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Student> Get()
        {
            return ReturnAllStudents();
        }

        // GET api/student/Get/2
        [HttpGet]
        [Route("Get/{id:int}")]
        public IActionResult Get(string id)
        {
            Student student = ReturnStudentById(id);

            if (student == null)
            {
                return NotFound("Student with that id does not exist!");
            }
            return Ok(student);
        }

        // POST api/student/Create
        [HttpPost]
        [Route("Create")]
        public IActionResult Post([FromBody] Student student)
        {

            if (students.Exists(s => s.Id == student.Id))
            {
                return Conflict("Student with that id already exist!");
            }
            students.Add(student);
            return Created($"Student with id {student.Id} is created", student.Id);
        }

        // PUT api/student/Update/2
        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult Put(string id, [FromBody] Student updatedStudent)
        {
            Student student = ReturnStudentById(id);
            if (student == null)
            {
                return NotFound("Student with that id does not exist!");
            }
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            return Ok($"Student with id {updatedStudent.Id} is sucessfully updated");
        }

        // DELETE api/student/Delete/2
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(string id)
        {
            Student student = ReturnStudentById(id);
            if (student == null)
            {
                return NotFound("Student with that id does not exist");
            }
            students.Remove(student);
            return Ok( $"Student with id {student.Id} is sucessfully deleted");
        }

        private IEnumerable<Student> ReturnAllStudents()
        {
            return students;
        }

        private Student ReturnStudentById(string id)
        {
            Student student = students.FirstOrDefault(s => s.Id == id);
            return student;
        }

    }
}