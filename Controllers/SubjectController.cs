using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppData;
using System.Drawing;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly ILogger<SubjectController> log;

        public SubjectController(
            AppDbContext ctx,
            
            ILogger<SubjectController> logger
            )
        {
            db = ctx;
            log = logger;
        }

        [HttpPost("/upload")]
        public IActionResult Upload(IFormFile file,[FromServices] IWebHostEnvironment env)
        {
            try
            {
                if (file != null)
                {
                    string filePath = $"/images/{file.FileName}";
                    string uploadPath = env.WebRootPath + filePath;
                    using var stream = System.IO.File.Create(uploadPath);
                    file.CopyTo(stream);
                    return Ok(filePath);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {

            var data = db.Subjects.Include(a => a.Students).ToList();
            log.LogInformation("data retrieved success");
            return Ok(data);


        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var data = db.Subjects.Include(a => a.Students).FirstOrDefault(a => a.SubId == id);
                log.LogInformation("data retrieved success");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Save(Subject sub)
        {
            try
            {
                //if (sub.ExamDate.Date > DateTime.Today.Date)
                //{
                //    ModelState.AddModelError("ExamDate", "invalid exam date");

                //}
                //if (sub.Students is not null && sub.Students.Any(s => s.Marks > sub.TotalMarks))
                //{
                //    ModelState.AddModelError("", "invalid marks");
                //}

                if (ModelState.IsValid)
                {
                    db.Subjects.Add(sub);
                    db.SaveChanges();
                    log.LogInformation("data insert success");
                    return Ok(sub);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update(Subject sub)
        {
            try
            {
                //if (sub.ExamDate.Date > DateTime.Today.Date)
                //{
                //    ModelState.AddModelError("ExamDate", "invalid exam date");

                //}
                //if (sub.Students is not null && sub.Students.Any(s => s.Marks > sub.TotalMarks))
                //{
                //    ModelState.AddModelError("", "invalid marks");
                //}
                if (ModelState.IsValid)
                {
                    var oldData = db.Subjects.Include(a => a.Students).First(a => a.SubId == sub.SubId);
                    db.Subjects.Remove(oldData);

                    db.Subjects.Add(sub);
                    db.SaveChanges();
                    log.LogInformation("data update success");
                    return Ok(sub);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var data = db.Subjects.Include(a => a.Students).FirstOrDefault(a => a.SubId == id);
                if (data == null)
                {
                    return NotFound($"Data not found by subject id {id}");
                }
                db.Subjects.Remove(data);
                db.SaveChanges();
                log.LogTrace("data delete success");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
