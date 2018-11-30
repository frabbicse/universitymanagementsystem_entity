using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMRS.Models.UnivContext;

namespace UCMRS.Controllers
{
    public class HelperController : Controller
    {
        UniContext db = new UniContext();
        // GET: Helper


            // Generate Registration No--------------
        public JsonResult GenerateRegNo(int id)
        {
            string regNo;
            var deptCode = db.Departments.Where(d => d.Id == id).Select(s => s.Code).SingleOrDefault();
            int codeCount = db.Students.Count(d => d.DeptId == id)+1;
            int year = DateTime.Now.Year;
            string date = db.Students.Where(d => d.DeptId == id).Select(y => y.Date).FirstOrDefault().ToShortDateString();
            //string year = dt.Substring(4,4);

            if (codeCount < 10)
            {
                regNo = deptCode +"-"+ year +"-00" + codeCount;
            }
            else if (codeCount < 100)
            {
                regNo = deptCode + "0" + codeCount;
            }
            else 
            {
                regNo = deptCode + " " +codeCount;
            }

            return Json(regNo, JsonRequestBehavior.AllowGet);
        }


        //------------ Load Name, Email, Department upon Selecting Registration No---- 

        public JsonResult RegDropDown(int id)
        {
            var studentInfo = (from st in db.Students
                join d in db.Departments on st.DeptId equals d.Id
                where st.Id == id
                select new
                {
                    Name = st.Name,
                    Email = st.Email,
                    DeptName = d.Name
                }).FirstOrDefault();
            return Json(studentInfo, JsonRequestBehavior.AllowGet);
        }


        //---------Dropdown for Course Statistics---------------

        //public JsonResult CourseStat(int id)
        //{
        //    var cStat = (from cstat in db.CourseAssigns
        //        join t in db.Teachers on cstat.TeacherId equals t.Id
        //        join c in db.Courses on cstat.CourseId equals c.Id
        //        join s in db.Semesters on c.SemId equals s.Id
        //        where t.DeptId == id
        //        select new
        //        {
        //            code = c.Code,
        //            name = c.Name,
        //            sem = s.Name,  //(from s in db.Semesters where c.SemId == s.Id select new { s.Name }),
        //            assignTo = t.Name
        //        }).ToList();

        //    return Json(cStat, JsonRequestBehavior.AllowGet);
        //}
    }
}