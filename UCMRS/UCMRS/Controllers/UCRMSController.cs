using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UCMRS.Models;
using UCMRS.Models.Entity;
using UCMRS.Models.UnivContext;
using UCMRS.Models.View;

namespace UCMRS.Controllers
{
    public class UCRMSController : Controller
    {
        private UniContext db = new UniContext();
        private DropDownData d = new DropDownData();

        #region Department Settings...

        // GET: UCRMS
        public ActionResult Index()
        {
            try
            {
                return View(db.Departments.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET: UCRMS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: UCRMS/Create
        public ActionResult AddDepartment()
        {
            return View();
        }

        // POST: UCRMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartment([Bind(Include = "Id,Code,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                if (!db.Departments.Any(d => d.Code == department.Code))
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AddDepartment");
                }
            }
            return View();
        }

        // GET: UCRMS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: UCRMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: UCRMS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: UCRMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
        
        #region Course Settings

        
        // GET: UCRMS
        public async Task<ActionResult> IndexCourse()
        {
            VM_Course vmCourse = new VM_Course();
            await Task.Run(() => vmCourse.GetCourseList());
            return View(vmCourse);
        }

        // GET: UCRMS/Details/5
        public ActionResult CourseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VM_Course course = new VM_Course();
            
            if (course.GetCourse(id) == null)
            {
                return HttpNotFound();
            }
            
            return View(course.GetCourse(id.Value));
        }

        // GET: UCRMS/Create
        public ActionResult CreateCourse()
        {
            ViewBag.Dept = new SelectList(db.Departments.ToList().OrderBy(s=>s.Id),"Id","Name");
            ViewBag.Semesters = new SelectList(db.Semesters.ToList().OrderBy(s=>s.Id),"Id","Name");
             return View();
        }

        // POST: UCRMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Dept = new SelectList(db.Departments.ToList().OrderBy(s=>s.Id),"Id","Name");
                ViewBag.Semesters = new SelectList(db.Semesters.ToList().OrderBy(s=>s.Id),"Id","Name");
                if (!db.Courses.Any(n => n.Code == course.Code && n.Name == course.Name))
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("IndexCourse");
                }
                else
                {
                    return RedirectToAction("CreateCourse");
                }
                
            }
            return View();
        }

        // GET: UCRMS/Edit/5
        public ActionResult EditCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course Course = db.Courses.Find(id);
            if (Course == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dept = new SelectList(db.Departments.ToList().OrderBy(s=>s.Id),"Id","Name");
            ViewBag.Semesters = new SelectList(db.Semesters.ToList().OrderBy(s=>s.Id),"Id","Name");
            return View(Course);
        }

        // POST: UCRMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(Course Course)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Dept = new SelectList(db.Departments.ToList().OrderBy(s=>s.Id),"Id","Name");
                ViewBag.Semesters = new SelectList(db.Semesters.ToList().OrderBy(s=>s.Id),"Id","Name");
                db.Entry(Course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexCourse");
            }
            return View(Course);
        }

        // GET: UCRMS/Delete/5
        public ActionResult DeleteCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: UCRMS/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult CourseDeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("IndexCourse");
        }
        
        #endregion

        #region Teacher Settings

        /// <summary>
        /// Teacher List View------
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> IndexTeacher()
        {
            try
            {
                VM_Teacher vmTeacher = new VM_Teacher();
                await Task.Run(() => vmTeacher.GetTeacherList());
                return View(vmTeacher);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Load Form for Teacher Entry
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTeacherInfo()
        {
            try
            {
                ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");
                ViewBag.Designation = new SelectList(d.GetDesignationList(), "Value", "Text");
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Submit form for Teacher Infomation
        /// </summary>
        /// <param name="vmTeacher"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTeacherInfo(VM_Teacher vmTeacher)
        {
            if (db.Teachers.FirstOrDefault(t=>t.Name == vmTeacher.Name) == null)
            {
                try
                {
                    ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");
                    ViewBag.Designation = new SelectList(d.GetDesignationList(), "Value", "Text");

                    Teacher teacher = new Teacher()
                    {
                        Name = vmTeacher.Name, Address = vmTeacher.Address,Email = vmTeacher.Email,
                        ContactNo = vmTeacher.ContactNo,DesigId = vmTeacher.DesigId,
                        DeptId = vmTeacher.DeptId,CreditTaken = vmTeacher.CreditTaken,
                        RemainingCredit = vmTeacher.CreditTaken
                    };
                    db.Teachers.Add(teacher);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexTeacher");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            ViewBag.Message = "Teacher Information exists.";
            return RedirectToAction("IndexTeacher", "UCRMS");
        }

        /// <summary>
        /// Delete teacher information by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteTeacherInfo(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexTeacher");
                }

                var teacher = db.Teachers.Find(id);
                if (teacher != null)
                {
                    db.Teachers.Remove(teacher);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexTeacher");
                }
                return RedirectToAction("IndexTeacher");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        /// <summary>
        /// Load Teacher information for editing..... 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditTeacherInfo(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Id = "Teacher is not found.";
                    return RedirectToAction("IndexTeacher");
                }
                ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");
                ViewBag.Designation = new SelectList(d.GetDesignationList(), "Value", "Text");
                var teacher = db.Teachers.Find(id);

                VM_Teacher vmTeacher = new VM_Teacher()
                {
                    TeacherId = teacher.TeacherId,
                    Name = teacher.Name, Address = teacher.Address, ContactNo = teacher.ContactNo,
                    Email = teacher.Email, DeptId = teacher.DeptId, DesigId = teacher.DesigId,
                    CreditTaken = teacher.CreditTaken,RemainingCredit = teacher.RemainingCredit
                };
                return View(vmTeacher);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// submit edited teacher information-----
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTeacherInfo(VM_Teacher vmTeacher)
        {
            try
            {
                ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");
                ViewBag.Designation = new SelectList(d.GetDesignationList(), "Value", "Text");
                
                Teacher Teacher = new Teacher()
                {
                    TeacherId = vmTeacher.TeacherId,
                    Name = vmTeacher.Name, Address = vmTeacher.Address, ContactNo = vmTeacher.ContactNo,
                    Email = vmTeacher.Email, DeptId = vmTeacher.DeptId, DesigId = vmTeacher.DesigId,
                    CreditTaken = vmTeacher.CreditTaken,RemainingCredit = vmTeacher.CreditTaken
                };
                db.Entry(Teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ViewBag.Edit = "Successfully Edited.";
                return RedirectToAction("IndexTeacher");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<ActionResult> DetailsTeacher(int? id)
        {
            if (id != null)
            {
                try
                {
                    VM_Teacher vmTeacher = new VM_Teacher();
                    await Task.Run(() => vmTeacher = vmTeacher.GetTeacherDetails(id.Value));
                    return View(vmTeacher);
                }
                catch (Exception e)
                {
                    return RedirectToAction("IndexTeacher");
                }
            }
            return RedirectToAction("IndexTeacher");
        }
        #endregion
        
        #region Assign Course To Teacher

        /// <summary>
        /// Cummulative view of Assigned Courses.....
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> IndexCourseAssign()
        {
            try
            {
                VM_CourseAssign vm = new VM_CourseAssign();
                await Task.Run(() => vm.GetAssiendCourses());
                return View(vm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Load form for assign course to the teacher....
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddCourseToAssign()
        {
            try
            {
                ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");
                ViewBag.Teacher = new SelectList(d.GetTeacherList(), "Value", "Text");
                ViewBag.Course = new SelectList(d.GetCourseList(), "Value", "Text");
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Store assign courses to the database... 
        /// </summary>
        /// <param name="vmCourseAssign"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCourseToAssign(Course course, Teacher teacher)
        {
            try
            {
                ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");
                var t = db.Teachers.Find(teacher.TeacherId);
                //if (!db.CourseAssigns.Any(n => n.TeacherId == CourseAssign.TeacherId && n.CourseId == CourseAssign.CourseId)
                //    && db.Teachers.Any(tt => tt.RemainingCredit > teacher.RemainingCredit)
                //    )
                {
                    db.Courses.Add(course);
                    db.Teachers.Attach(t);
                    t.RemainingCredit = teacher.RemainingCredit;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexCourseAssign");
                }
                Session["success_div"] = "true";
                Session["success_msg"] = "Same course Assigned to This .";
                return RedirectToAction("IndexCourseAssign");
            }
            catch (Exception e)
            {
                return RedirectToAction("IndexCourseAssign");
            }
        }


        public async Task<ActionResult> DeleteAssignedCourse(int? id)
        {
            if (id != null)
            {
                try
                {
                    var assignedCourse = db.Courses.Find(id);
                    if (assignedCourse != null)
                    {
                        db.Courses.Remove(assignedCourse);
                        await db.SaveChangesAsync();
                        return RedirectToAction("IndexCourseAssign");
                    }
                    return RedirectToAction("IndexCourseAssign");
                }
                catch (Exception e)
                {
                    return RedirectToAction("IndexCourseAssign");
                }
            }
            return RedirectToAction("IndexCourseAssign");
        }


        /// <summary>
        /// Load Edit form with desired data----
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAssigendCourse(int? id)
        {
            if (id != null)
            {
                try
                {
                    ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");

                    //AssignCourse ca = db.CourseAssigns.Find(id);
                    //if (ca == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    //return View(ca);
                }
                catch (Exception e)
                {
                    return RedirectToAction("IndexCourseAssign");
                }
            }
            return RedirectToAction("IndexCourseAssign");
        }

        /// <summary>
        /// Submit form updated information....
        /// </summary>
        /// <param name = "vmCourseAssign" ></ param >
        /// < returns ></ returns >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAssigendCourse(Course ca)
        {
            try
            {
                ViewBag.Department = new SelectList(d.GetDeptList(), "Value", "Text");


                db.Entry(ca).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexCourseAssign", "UCRMS");
            }
            catch (Exception e)
            {
                return RedirectToAction("IndexCourseAssign", "UCRMS");
            }
        }


        public ActionResult IndexCourseStat()
        {
            try
            {
                ViewBag.Dept = new SelectList(d.GetDeptList(), "Value", "Text");
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("IndexCourseStat");
            }
        }

        //[HttpPost][ValidateAntiForgeryToken]
        //public async Task<ActionResult> IndexCourseStat(int? id)
        //{
        //    try
        //    {
        //        ViewBag.Dept = new SelectList(d.GetDeptList(), "Value", "Text");

        //        VMDepartment vm = new VMDepartment();
        //        await Task.Run(() => vm.CourseStatistics(id));
        //        return View(vm);
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("IndexCourseStat");
        //    }
        //}
        #endregion
            
        #region Student Settings

        // GET: UCRMS
        public async Task<ActionResult> IndexStudent()
        {
            VM_Student student = new VM_Student();
            await Task.Run(() => student.GetStudents());
            return View(student);
        }

        // GET: UCRMS/Details/5
        public ActionResult StudentDetails(int? id)
        {
            VM_Student st = new VM_Student();
            ViewBag.student = st.GetStudent(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: UCRMS/Create
        public ActionResult AddStudent()
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s=>s.Id), "Id", "Name");
            return View();
        }

        // POST: UCRMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(Student student)
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            try
            {
                if (ModelState.IsValid)
                {
                    if (!db.Students.Any(dept => dept.Email == student.Email))
                    {
                        db.Students.Add(student);
                        db.SaveChanges();
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Insert Successfully.";
                        return RedirectToAction("IndexStudent");
                    }
                    else
                    {
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Email is not unique.";
                        return RedirectToAction("AddStudent");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return View();
        }

        // GET: UCRMS/Edit/5
        public ActionResult EditStudent(int? id)
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: UCRMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(Student student)
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexStudent");
            }
            return View(student);
        }

        // GET: UCRMS/Delete/5
        public ActionResult DeleteStudent(int? id)
        {
            VM_Student st = new VM_Student();
            ViewBag.student = st.GetStudent(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: UCRMS/Delete/5
        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudentConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("IndexStudent");
        }


        #endregion

        #region Allocate Rooms Settings


        // GET: UCRMS
        public async Task<ActionResult> IndexAllocateRoom()
        {
            VM_AllocateRooms rooms = new VM_AllocateRooms();
            await Task.Run(() => rooms.AllocatedRooms());
            return View(rooms);
        }

        // GET: UCRMS/Details/5
        //public ActionResult AllocatedRoomDetails(int? id)
        //{
        //    VM_Student st = new VM_Student();
        //    ViewBag.student = st.GetStudent(id);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // GET: UCRMS/Create
        public ActionResult AddAllocatedRoom()
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Room = new SelectList(db.Rooms.ToList().OrderBy(s => s.Id), "Id", "RoomNo");
            ViewBag.Day = new SelectList(db.Days.ToList().OrderBy(s => s.Id), "Id", "Name");
            return View();
        }

        // POST: UCRMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAllocatedRoom(AllocateRoom allocateRoom)
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Room = new SelectList(db.Rooms.ToList().OrderBy(s => s.Id), "Id", "RoomNo");
            ViewBag.Day = new SelectList(db.Days.ToList().OrderBy(s => s.Id), "Id", "Name");
            try
            {
                if (ModelState.IsValid)
                {
                    if (!db.AllocateRooms.Any(t => t.TimeTo == allocateRoom.TimeTo && t.TimeFrom == allocateRoom.TimeFrom &&
                    t.RoomId == allocateRoom.RoomId))
                    {
                        allocateRoom.IsActive = false;
                        db.AllocateRooms.Add(allocateRoom);
                        db.SaveChanges();
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Insert Successfully.";
                        return RedirectToAction("IndexAllocateRoom");
                    }
                    else
                    {
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Email is not unique.";
                        return RedirectToAction("AddAllocatedRoom");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("IndexAllocateRoom");
        }

        // GET: UCRMS/Edit/5
        public ActionResult EditAllocatedRoom(int? id)
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Room = new SelectList(db.Rooms.ToList().OrderBy(s => s.Id), "Id", "RoomNo");
            ViewBag.Day = new SelectList(db.Days.ToList().OrderBy(s => s.Id), "Id", "Name");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateRoom allocate = db.AllocateRooms.Find(id);
            if (allocate == null)
            {
                return HttpNotFound();
            }
            return View(allocate);
        }

        // POST: UCRMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAllocatedRoom(AllocateRoom allocateRoom)
        {
            ViewBag.Department = new SelectList(db.Departments.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Room = new SelectList(db.Rooms.ToList().OrderBy(s => s.Id), "Id", "RoomNo");
            ViewBag.Day = new SelectList(db.Days.ToList().OrderBy(s => s.Id), "Id", "Name");

            if (ModelState.IsValid)
            {
                db.Entry(allocateRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAllocateRoom");
            }
            return View(allocateRoom);
        }

        // GET: UCRMS/Delete/5
        public ActionResult DeleteAllocatedRoom(int? id)
        {
            VM_AllocateRooms ar = new VM_AllocateRooms();
            ViewBag.allocatedRoom = ar.AllocatedRoom(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateRoom allocateRoom = db.AllocateRooms.Find(id);
            if (allocateRoom == null)
            {
                return HttpNotFound();
            }
            return View(allocateRoom);
        }

        // POST: UCRMS/Delete/5
        [HttpPost, ActionName("DeleteAllocatedRoom")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllocatedRoomConfirmed(int id)
        {
            AllocateRoom allocateRoom = db.AllocateRooms.Find(id);
            db.AllocateRooms.Remove(allocateRoom ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("IndexAllocateRoom");
        }


        #endregion

        #region Course Enrollment Settings


        // GET: UCRMS
        public async Task<ActionResult> IndexEnrollCourse()
        {
            VM_EnrollCourse enrolledCourse = new VM_EnrollCourse();
            await Task.Run(() => enrolledCourse.EnrolledCourse());
            return View(enrolledCourse);
        }

        // GET: UCRMS/Details/5
        //public ActionResult AllocatedRoomDetails(int? id)
        //{
        //    VM_Student st = new VM_Student();
        //    ViewBag.student = st.GetStudent(id);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // GET: UCRMS/Create
        public ActionResult AddEnrollCourse()
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");

            return View();
        }

        // POST: UCRMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEnrollCourse(EnrollCourse enrollCourse)
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (!db.EnrollCourses.Any(t => t.CourseId == enrollCourse.CourseId && t.StudentId == enrollCourse.StudentId))
                    {
                        db.EnrollCourses.Add(enrollCourse);
                        db.SaveChanges();
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Insert Successfully.";
                        return RedirectToAction("IndexEnrollCourse");
                    }
                    else
                    {
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Email is not unique.";
                        return RedirectToAction("AddEnrollCourse");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("IndexEnrollCourse");
        }

        // GET: UCRMS/Edit/5
        public ActionResult EditEnrollCourse(int? id)
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enroll = db.EnrollCourses.Find(id);
            if (enroll == null)
            {
                return HttpNotFound();
            }
            return View(enroll);
        }

        // POST: UCRMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEnrollCourse(EnrollCourse enrollCourse)
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            if (ModelState.IsValid)
            {
                db.Entry(enrollCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexenrollCourse");
            }
            return View(enrollCourse);
        }

        // GET: UCRMS/Delete/5
        public ActionResult DeleteEnrollCourse(int? id)
        {
            VM_EnrollCourse ec = new VM_EnrollCourse();
            ViewBag.enrolled = ec.EnrollCourse(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // POST: UCRMS/Delete/5
        [HttpPost, ActionName("DeleteEnrollCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEnrollCourseConfirmed(int id)
        {
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            db.EnrollCourses.Remove(enrollCourse ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("IndexEnrollCourse");
        }


        #endregion

        #region Student Result Settings


        // GET: UCRMS
        public async Task<ActionResult> IndexStudentResult()
        {
            VM_StudentResult result = new VM_StudentResult();
            await Task.Run(() => result.GetResultList());
            return View(result);
        }

        // GET: UCRMS/Details/5
        //public ActionResult AllocatedRoomDetails(int? id)
        //{
        //    VM_Student st = new VM_Student();
        //    ViewBag.student = st.GetStudent(id);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // GET: UCRMS/Create
        public ActionResult AddStudentResult()
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Grade = new SelectList(db.Grades.ToList().OrderBy(s => s.Id), "Id", "GradeName");

            return View();
        }

        // POST: UCRMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentResult(StudentResult studentResult)
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Grade = new SelectList(db.Grades.ToList().OrderBy(s => s.Id), "Id", "GradeName");

            try
            {
                if (ModelState.IsValid)
                {
                    if (!db.StudentsResults.Any(t => t.CourseId == studentResult.CourseId && t.StudentId == studentResult.StudentId))
                    {
                        db.StudentsResults.Add(studentResult);
                        db.SaveChanges();
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Insert Successfully.";
                        return RedirectToAction("IndexStudentResult");
                    }
                    else
                    {
                        Session["success_div"] = "true";
                        Session["success_msg"] = "Email is not unique.";
                        return RedirectToAction("AddStudentResult");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("IndexStudentResult");
        }

        // GET: UCRMS/Edit/5
        public ActionResult EditStudentResult(int? id)
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Grade = new SelectList(db.Grades.ToList().OrderBy(s => s.Id), "Id", "GradeName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResult result = db.StudentsResults.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: UCRMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudentResult(StudentResult studentResult)
        {
            ViewBag.Student = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            ViewBag.Course = new SelectList(db.Courses.ToList().OrderBy(s => s.Id), "Id", "Name");
            ViewBag.Grade = new SelectList(db.Grades.ToList().OrderBy(s => s.Id), "Id", "GradeName");

            if (ModelState.IsValid)
            {
                db.Entry(studentResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexStudentResult");
            }
            return View(studentResult);
        }

        // GET: UCRMS/Delete/5
        public ActionResult DeleteStudentResult(int? id)
        {
            VM_StudentResult sr = new VM_StudentResult();
            ViewBag.SR = sr.GetResult(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResult studentResult = db.StudentsResults.Find(id);
            if (studentResult == null)
            {
                return HttpNotFound();
            }
            return View(studentResult);
        }

        // POST: UCRMS/Delete/5
        [HttpPost, ActionName("DeleteStudentResult")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudentResultConfirmed(int id)
        {
            StudentResult studentResult = db.StudentsResults.Find(id);
            db.StudentsResults.Remove(studentResult ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("IndexEnrollCourse");
        }


        #endregion

        #region View Result

        public ActionResult IndexViewResult()
        {
            ViewBag.RegNo = new SelectList(db.Students.ToList().OrderBy(s => s.Id), "Id", "RegNo");
            return View();
        }

        #endregion

        #region JSON

        public JsonResult GetTeacher(int id)
        {
            var list = db.Teachers.Where(t => t.DeptId == id);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourse(int id)
        {
            var list = db.Courses.Where(t => t.DeptId == id);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherDetails(int id)
        {
            var list = db.Teachers.FirstOrDefault(t=>t.TeacherId ==id);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseDetails(int id)
        {
            var list = db.Courses.FirstOrDefault(t=>t.Id ==id);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetTeacherRemainingCredit(int courseId)
        //{
        //    var list = db.CourseAssigns.FirstOrDefault(/*ca => ca.CourseId == courseId*/);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }
}
