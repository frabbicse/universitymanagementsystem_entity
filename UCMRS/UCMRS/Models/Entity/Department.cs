using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UCMRS.Models.Entity
{
    public class Department
    {
        [Key]
        [DisplayName("Department")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter code")]
       [MaxLength(7),MinLength(2)]
       [Index(IsUnique = true)]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "Provide Department Name Properly")]
        public string  Name { get; set; }
    }
}