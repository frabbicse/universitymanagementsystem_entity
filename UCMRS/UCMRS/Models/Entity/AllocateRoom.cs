using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UCMRS.Models.DB;

namespace UCMRS.Models.Entity
{
    public class AllocateRoom
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Must select Department")]
        [DisplayName("Department")]
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }

        [Required(ErrorMessage = "Must select Course")]
        [DisplayName("Course")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [Required(ErrorMessage = "Must select Room")]
        [DisplayName("Room No.")]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [Required(ErrorMessage = "Must select Day")]
        [DisplayName("Day")]
        public int DayId { get; set; }
        [ForeignKey("DayId")]
        public Day Day { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DisplayName("From")]
        public DateTime TimeFrom { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:HH:mm}")]
        [DisplayName("To")]
        public DateTime TimeTo { get; set; }

        public bool IsActive { get; set; }
    }
}