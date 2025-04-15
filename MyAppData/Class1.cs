using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyAppData
{
    [Table("tblSubject")]
    public class Subject//: IValidatableObject
    {
        [Key]
        public Guid SubId { get; set; } = Guid.NewGuid();

        [Required, Column("Subject"), StringLength(50, MinimumLength = 2 )]
        public string SubName { get; set; }

        [DataType(DataType.Date)]      
        public DateTime ExamDate { get; set; } = DateTime.Today;
        [Range(0, 1000)]
        public int TotalMarks { get; set; }

        public string? ImagePath { get; set; }


        //[Range((int)Gender.Male, (int) Gender.Female)]
        //public Gender Gender { get; set; }

        public IList<Student>? Students { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var result = new List<ValidationResult>();
        //    if (this.ExamDate.Date > DateTime.Today.Date)
        //    {

        //      var examDatResult =  new ValidationResult("invalid exam date", new[] { "Examdate"});

        //        result.Add(examDatResult);
        //    }
        //    if (this.Students is not null && this.Students.Any(s => s.Marks > this.TotalMarks))
        //    {
        //        var examDatResult = new ValidationResult("invalid marks");

        //        result.Add(examDatResult);
        //    }
        //    return result;
        //}
    }
    //public enum Gender :int{
    //    Male, Female,  Unspecified = 2, 
    //}

    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, Column("StudentName"), StringLength(50, MinimumLength = 4)]
        public string Name { get; set; }
        [Required, Column("ObtainedMarks"), Range(0, 1000)]
        public int Marks { get; set; }

        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; } = Guid.NewGuid();

        //[JsonIgnore]
        public Subject? Subject { get; set; }
    }


    public class AppDbContext:DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public AppDbContext(DbContextOptions dbo):base(dbo)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Subject>().HasData(
                new Subject() { SubName = "Sub 1", TotalMarks = 100 },
                new Subject() { SubName = "Sub 2", TotalMarks = 100 },
                new Subject() { SubName = "Sub 3", TotalMarks = 100 }

                );




        }



    }

}
