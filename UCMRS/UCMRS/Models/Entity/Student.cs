using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UCMRS.Models.DB;

namespace UCMRS.Models.Entity
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Contact No")]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Current Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }
        [DisplayName("Reg. No.")]
        public string RegNo { get; set; }
    }

    public class StudentResult
    {
        public int Id { get; set; }
        [DisplayName("Reg No")]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [DisplayName("Course")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        [DisplayName("Grade")]
        public int GradeId { get; set; }
        [ForeignKey("GradeId")]
        public Grade Grade { get; set; }

        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Department { get; set; }
    }
}