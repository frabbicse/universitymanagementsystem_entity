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
    public class VM_Teacher
    {
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Need Name")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Address")] 
        public string Address { get; set; }

        [Required(ErrorMessage = "Email Please!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number Please!")]
        [DisplayName("Contact No.")]
        public string ContactNo { get; set; }

        [DisplayName("Designation")]
        [Required(ErrorMessage = "Designation Please!")]
        public int DesigId { get; set; }
        public string Designation { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage = "Department Please!")]
        public int DeptId { get; set; }

        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Credit Limit!")]
        [Range(0, 30)]
        [DisplayName("Credit Taken")] 
        public decimal CreditTaken { get; set; }

        [DisplayName("Remaining Credit")]
        public decimal RemainingCredit { get; set; }

        public ICollection<VM_Teacher> Teachers { get; set; }

        private UniContext db = new UniContext();

        /// <summary>
        /// Collective Teacher Information
        /// </summary>
        public void GetTeacherList()
        {
            var teachers = (from t in db.Teachers
                join d in db.Departments on t.DeptId equals d.Id
                join ds in db.Designations on t.DesigId equals ds.Id
                select new VM_Teacher()
                {
                    TeacherId = t.TeacherId, Name = t.Name, Address = t.Address,Email = t.Email,
                    CreditTaken = t.CreditTaken,
                    RemainingCredit = t.RemainingCredit,
                    ContactNo = t.ContactNo, Designation = ds.Name, DepartmentName = d.Name
                }).ToList();
            Teachers = teachers;
        }

        /// <summary>
        /// Individual Teacher Details
        /// </summary>
        public VM_Teacher GetTeacherDetails(int? id)
        {
            var teachers = (from t in db.Teachers
                join d in db.Departments on t.DeptId equals d.Id
                join ds in db.Designations on t.DesigId equals ds.Id
                where id == t.TeacherId
                select new VM_Teacher()
                {
                    TeacherId = t.TeacherId, Name = t.Name, Address = t.Address,Email = t.Email,
                    CreditTaken = t.CreditTaken,ContactNo = t.ContactNo, Designation = ds.Name, DepartmentName = d.Name
                }).FirstOrDefault();
            return teachers;
        }
    }
}