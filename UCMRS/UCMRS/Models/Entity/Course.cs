using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UCMRS.Models.DB;

namespace UCMRS.Models.Entity
{
    public class Course
    {
        public int Id { get; set; }

        [MinLength(5)]
        [Required(ErrorMessage = "Enter Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Enter Course Name")]
        public string Name { get; set; }
    
        [Required]
        [Range(.5,5.0)]
        public decimal Credit { get; set; }
        public string Description { get; set; }

        [DisplayName("Department")]
        public int  DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }

        [DisplayName("Semester")]
        public int SemId { get; set; }
        [ForeignKey("SemId")]
        public Semester Semester { get; set; }

        [DisplayName("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        public bool IsActive { get; set; }
    }

    public class EnrollCourse
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

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Department { get; set; }
    }
}