using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UCMRS.Models.DB;
using UCMRS.Models.Entity;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models.View
{
    public class VM_Course
    {
        public int Id { get; set; }

        [MinLength(5)]
        [Required(ErrorMessage = "Enter Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Enter Course Name")]
        public string Name { get; set; }
    
        [Required]
        [Range(.5,5.0)]
        public decimal Credit { get; set; }
        public string Description { get; set; }

        [DisplayName("Department")]
        public int  DeptId { get; set; }
        [DisplayName("Department")]
        public string DeptName { get; set; }

        [DisplayName("Semester")]
        public int SemId { get; set; }
        [DisplayName("Semester")]
        public string SemesterName { get; set; }

        public ICollection<VM_Course> Courses { get; set; }

        private UniContext db = new UniContext();

        public void GetCourseList()
        {
            var courses = (from c in db.Courses
                join d in db.Departments on c.DeptId equals d.Id
                join s in db.Semesters on c.SemId equals s.Id
                select new VM_Course()
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Credit = c.Credit,
                    Description = c.Description,
                    DeptName = d.Name,
                    SemesterName = s.Name
                }).ToList();
            Courses = courses;
        }

        public VM_Course GetCourse(int? id)
        {
            var course = (from c in db.Courses
                join d in db.Departments on c.DeptId equals d.Id
                join s in db.Semesters on c.SemId equals s.Id
                           where c.Id == id
                select new VM_Course()
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Credit = c.Credit,
                    Description = c.Description,
                    DeptName = d.Name,
                    SemesterName = s.Name
                }).FirstOrDefault();
            return course;
        }
    }


    // View Results 
    public class VM_ViewResult
    {
        [DisplayName("Reg No")]
        public string RegNo { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
    }
}