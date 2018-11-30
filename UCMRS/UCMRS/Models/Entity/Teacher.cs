using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UCMRS.Models.DB;

namespace UCMRS.Models.Entity
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Need Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email Please!")]
        [EmailAddress]
        [Index("UX_Teacher_Email",IsUnique = true)]
        [MaxLength(60)]
        [Column(TypeName = "Varchar")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number Please!")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Designation Please!")]
        public int DesigId { get; set; }
        [ForeignKey("DesigId")]
        public Designation Designation { get; set; }

        [Required(ErrorMessage = "Department Please!")]
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }

        [Required(ErrorMessage = "Credit Limit!")]
        [Range(0,30)]
        public decimal CreditTaken { get; set; }

        [DisplayName("Remaining Credit")]
        public decimal RemainingCredit { get; set; }
    }
}