using UCMRS.Models.Entity;

namespace UCMRS.Models.View
{
    public class ClassSchedule
    {
        public int Id { get; set; }
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}