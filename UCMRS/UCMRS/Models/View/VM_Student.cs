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
    public class VM_Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int DeptId { get; set; }
        [DisplayName("Department")]
        public string DeptName { get; set; }
        public string RegNo { get; set; }

        public ICollection<VM_Student> VmStudents { get; set; }
        public UniContext db = new UniContext();

        public void GetStudents()
        {
            var students = (from s in db.Students
                join d in db.Departments on s.DeptId equals d.Id
                select new VM_Student()
                {
                    Id = s.Id, Name = s.Name, Email = s.Email, ContactNo = s.ContactNo,
                    Date = s.Date, Address = s.Address, RegNo = s.RegNo,
                    DeptId = d.Id, DeptName = d.Name
                }).ToList();
            VmStudents = students;
        }

        public VM_Student GetStudent(int ? id)
        {
            var students = (from s in db.Students
                join d in db.Departments on s.DeptId equals d.Id
                            where s.Id == id
                select new VM_Student()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    ContactNo = s.ContactNo,
                    Date = s.Date,
                    Address = s.Address,
                    RegNo = s.RegNo,
                    DeptId = d.Id,
                    DeptName = d.Name
                }).SingleOrDefault();
            return students;
        }
    }
}