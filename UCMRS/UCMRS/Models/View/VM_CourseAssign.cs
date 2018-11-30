using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UCMRS.Models.Entity;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models.View
{
    public class VM_CourseAssign
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Select Department")]
        [DisplayName("Department")]
        public int DeptId { get; set; }

        public string Department { get; set; }

        [Required(ErrorMessage = "Select Teacher")]
        [DisplayName("Teacher")]
        public int TeacherId { get; set; }

        [DisplayName("Name")]
        public string TeacherName { get; set; }

        [DisplayName("Credit to be Taken")]
        public decimal CreditTaken { get; set; }

        [DisplayName("Remaining Credit")]
        public decimal RemainingCredit { get; set; }

        [Required(ErrorMessage = "Select Course")]
        [DisplayName("Course Code")]
        public int CourseId { get; set; }

        [DisplayName("Name")]
        public string CourseName { get; set; }

        [DisplayName("Credit")]
        public decimal Credit { get; set; }

        public ICollection<VM_CourseAssign> CourseAssigns { get; set; }

        private UniContext db = new UniContext();

        public void GetAssiendCourses()
        {
            var courses = (from c in db.Courses
                           join t in db.Teachers on c.TeacherId equals t.TeacherId
                           join d in db.Departments on t.DeptId equals d.Id
                           select new VM_CourseAssign()
                           {
                               Id = c.Id,
                               DeptId = d.Id,
                               Department = d.Name,
                               TeacherId = t.TeacherId,
                               TeacherName = t.Name,
                               //CourseId = cr.Id,
                               //CourseName = cr.Name, //CreditTaken = t.CreditTaken, //RemainingCredit = c.RemainingCredit,
                               //Credit = cr.Credit
                           }).ToList();

            CourseAssigns = courses;
        }

        //public VM_CourseAssign GetAssiendCoursesById(int? id)
        //{
        //    var courses = (from c in db.CourseAssigns
        //                   join t in db.Teachers on c.TeacherId equals t.Id
        //                   join cr in db.Courses on c.CourseId equals cr.Id
        //                   join d in db.Departments on t.DeptId equals d.Id
        //                   where c.Id == id
        //                   select new VM_CourseAssign()
        //                   {
        //                       Id = c.Id,
        //                       //DeptId = d.Id,
        //                       //Department = d.Name,
        //                       TeacherId = t.Id,
        //                       TeacherName = t.Name,
        //                       CourseId = cr.Id,
        //                       CourseName = cr.Name, //CreditTaken = t.CreditTaken, //RemainingCredit = c.RemainingCredit,
        //                       Credit = cr.Credit
        //                   }).FirstOrDefault();
        //    return courses;
        //}
    }
}