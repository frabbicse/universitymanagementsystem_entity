using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models.View
{
    public class VMDepartment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [DisplayName("Department")]
        public string  Name { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        
        public int CourseAssignId { get; set; }
        public string AssignedTeacherName { get; set; } 
        public List<VMDepartment> Departments { get; set; }
        public List<VMDepartment> CourseStat { get; set; }

        UniContext db  = new UniContext();

        //public void CourseStatistics(int? id)
        //{
        //    var courseStat = (from d in db.Departments
        //        join ca in db.CourseAssigns on d.Id equals ca.DeptId
        //        join c in db.Courses on d.Id equals c.DeptId
        //        join s in db.Semesters on c.SemId equals s.Id
        //            where d.Id == id
        //                  select new VMDepartment()
        //                  {
        //                      Id = d.Id, Code = c.Code, CourseName = c.Name, SemesterName = s.Name, 
        //                      //AssignedTeacherName = ca.Name
        //                  }).ToList();
        //    CourseStat = courseStat;
        //}
    }
}