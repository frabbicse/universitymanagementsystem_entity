using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMRS.Models.DB;
using UCMRS.Models.UnivContext;

namespace UCMRS.Controllers
{
    public class ProviderController : Controller
    {
        private UniContext db = new UniContext(); 
        // GET: Provider
        public ActionResult Index()
        {
            return View();
        }

        #region Semester

        public ActionResult SemesterIndex()
        {
            try
            {
                var semesters = db.Semesters.ToList();
                if (semesters != null)
                {
                    return View(semesters);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("");
        }

        /// <summary>
        /// Load input form for Semester
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveSemester()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSemester(Semester semester)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Semesters.Add(semester);
                    db.SaveChanges();
                    return RedirectToAction("SemesterIndex");
                }
                return RedirectToAction("SaveSemester");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion

        #region Designation

        public ActionResult IndexDesignation()
        {
            try
            {
                var designation = db.Designations.ToList();
                if (designation.Any())
                {
                    return View(designation);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("");
        }

        /// <summary>
        /// Load input form for Semester
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDesignation()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDesignation(Designation designation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Designations.Add(designation);
                    db.SaveChanges();
                    return RedirectToAction("IndexDesignation");
                }
                return RedirectToAction("SaveSemester");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        #endregion

        #region Rooms

        public ActionResult IndexRoom()
        {
            try
            {
                var rooms = db.Rooms.ToList();
                if (rooms.Any())
                {
                    return View(rooms);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("");
        }

        /// <summary>
        /// Load input form for Semester
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveRoom()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRoom(Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                    return RedirectToAction("IndexRoom");
                }
                return RedirectToAction("SaveRoom");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        #endregion
    }
}