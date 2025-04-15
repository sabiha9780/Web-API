using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppData;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext db;

        public ValuesController(AppDbContext ctx)
        {
            db = ctx;
        }
        [HttpGet("/Max")]
        public IActionResult GetMax()
        {
            var marks = db.Students.Max(a => a.Marks);
            return Ok(marks);
        }
        [HttpGet("/Min")]
        public IActionResult GetMin()
        {
            var marks = db.Students.Min(a => a.Marks);
            return Ok(marks);
        }


        [HttpGet("/SubjectHighest")]
        public IActionResult GetSubMax()
        {
            try
            {
                var data = db.Students.Include(s => s.Subject).
               Select(s => new { Name = s.Name, Marks = s.Marks, SubName = s.Subject.SubName });


                var grpData = data.GroupBy(a => a.SubName).Select(s => new { HighestMarks = s.Max(a => a.Marks), SubjectName = s.Key });



                return Ok(grpData);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }
        [HttpGet("/StudentHighest")]
        public IActionResult GetStudentMax()
        {
            try
            {
                var data = db.Students.GroupBy(a => a.Name).Select(s => new { HighestMarks = s.Max(a => a.Marks), StudentName = s.Key });

                return Ok(data);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }
        [HttpGet("/StudentTotal")]
        public IActionResult GetStudentTotal()
        {
            try
            {
                var data = db.Students.GroupBy(a => a.Name).Select(s => new { Total = s.Sum(a => a.Marks), StudentName = s.Key });

                return Ok(data);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }
        [HttpGet("/StudentExamCount")]
        public IActionResult GetStudentExamCount()
        {

            var data = db.Students.GroupBy(a => a.Name).Select(s => new { Count = s.Count(), StudentName = s.Key });

            return Ok(data);


        }
        [HttpGet("/StudentMin")]
        public IActionResult GetStudentMin()
        {

            var data = db.Students.GroupBy(a => a.Name).Select(s => new { Minimum = s.Min(a => a.Marks), StudentName = s.Key });

            return Ok(data);


        }
        [HttpGet("/StudentAvg")]
        public IActionResult GetStudentAverage()
        {

            var data = db.Students.GroupBy(a => a.Name).Select(s => new { Average = s.Average(a => a.Marks), StudentName = s.Key });

            return Ok(data);

        }
    }
}
