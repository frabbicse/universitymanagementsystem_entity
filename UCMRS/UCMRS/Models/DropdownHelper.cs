using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models
{
    
    public class DropDownData
    {
        private UniContext db = new UniContext();
        

        public string Text { get; set; }

        public string Value { get; set; }

        public DropDownData()
        {

        }

        public DropDownData(string txt, string val)
        {
            Text = txt;
            Value = val;
        }

        /// <summary>
        /// Get Department data from Database and Load dropdown list....
        /// </summary>
        /// <returns></returns>
        public List<object> GetDeptList()
        {
            var deptList = new List<object>();

            foreach (var Depts in db.Departments)
            {
                deptList.Add(new { Text = Depts.Name, Value = Depts.Id });
            }
            return deptList;
        }


        /// <summary>
        /// Get Department data from Database and Load dropdown list....
        /// </summary>
        /// <returns></returns>
        public List<object> GetDesignationList()
        {
            var designationList = new List<object>();

            foreach (var designation in db.Designations)
            {
                designationList.Add(new { Text = designation.Name, Value = designation.Id });
            }
            return designationList;
        }

       /// <summary>
       /// Teacher List For dropdown.....
       /// </summary>
       /// <returns></returns>
        public List<object> GetTeacherList()
        {
            var teacherList = new List<object>();

            foreach (var teacher in db.Teachers)
            {
                teacherList.Add(new { Text = teacher.Name, Value = teacher.TeacherId });
            }
            return teacherList;
        }

        /// <summary>
        /// Course List for dropdown
        /// </summary>
        /// <returns></returns>
        public List<object> GetCourseList()
        {
            var courseList = new List<object>();

            foreach (var course in db.Courses)
            {
                courseList.Add(new { Text = course.Name, Value = course.Id });
            }
            return courseList;
        }
    }
}