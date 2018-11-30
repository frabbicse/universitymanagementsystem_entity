using System;
using System.Collections.Generic;
using System.Linq;
using UCMRS.Models.Entity;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models.View
{
    public class VM_EnrollCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string RegNo { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<VM_EnrollCourse> EnrollCourses { get; set; }

        public UniContext db = new UniContext();

        public void EnrolledCourse()
        {
            var courses = (from ec in db.EnrollCourses
                join st in db.Students on ec.StudentId equals st.Id
                join course in db.Courses on ec.CourseId equals course.Id
                select new VM_EnrollCourse()
                {
                    Id= ec.Id, StudentId = st.Id, RegNo = st.RegNo,
                    CourseId = course.Id, CourseName = course.Name, DateTime = ec.Date
                }).ToList();
            EnrollCourses = courses;
        }

        public VM_EnrollCourse EnrollCourse(int? id)
        {
            var courses = (from ec in db.EnrollCourses
                join st in db.Students on ec.StudentId equals st.Id
                join course in db.Courses on ec.CourseId equals course.Id
                select new VM_EnrollCourse()
                {
                    Id = ec.Id,
                    StudentId = st.Id,
                    RegNo = st.RegNo,
                    CourseId = course.Id,
                    CourseName = course.Name,
                    DateTime = ec.Date
                }).SingleOrDefault();
            return courses;
        }
    }
}