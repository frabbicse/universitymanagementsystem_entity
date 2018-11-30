using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models.View
{
    public class VM_StudentResult
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        [DisplayName("Reg No")]
        public string StudentRegNo { get; set; }
        [DisplayName("Name")]
        public string StudentName { get; set; }
        [DisplayName("Email")]
        public string StudentEmail { get; set; }
        [DisplayName("Department")]
        public string StudentDepartment { get; set; }
        
        public int CourseId { get; set; }
        [DisplayName("Course")]
        public string CourseName { get; set; }
        
        public int GradeId { get; set; }
        [DisplayName("Grade")]
        public string GradeName { get; set; }

        public ICollection<VM_StudentResult> Results { get; set; }

        public UniContext db = new UniContext();

        public void GetResultList()
        {
            var sreult = (from result in db.StudentsResults 
                          join st in db.Students on result.StudentId equals st.Id
                          join c in db.Courses on result.CourseId equals c.Id
                          join g in db.Grades on result.GradeId equals g.Id
                          select new VM_StudentResult()
                          {
                              Id = result.Id,
                              StudentId = st.Id,StudentRegNo = st.RegNo,StudentName =  st.Name, StudentEmail = st.Email, 
                              CourseId = c.Id,CourseName = c.Name,
                              GradeId = g.Id, GradeName = g.GradeName
                          }).ToList();
            Results = sreult;
        }

        public VM_StudentResult GetResult(int? id)
        {
            var sreult = (from result in db.StudentsResults
                join st in db.Students on result.StudentId equals st.Id
                join c in db.Courses on result.CourseId equals c.Id
                join g in db.Grades on result.GradeId equals g.Id
                          where result.Id == id 
                select new VM_StudentResult()
                {
                    Id = result.Id,
                    StudentId = st.Id,
                    StudentRegNo = st.RegNo,
                    StudentName = st.Name,
                    StudentEmail = st.Email,
                    CourseId = c.Id,
                    CourseName = c.Name,
                    GradeId = g.Id,
                    GradeName = g.GradeName
                }).SingleOrDefault();
            return sreult;
        }
    }
}