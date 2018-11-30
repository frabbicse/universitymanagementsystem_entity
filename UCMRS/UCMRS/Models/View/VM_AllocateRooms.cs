using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCMRS.Models.UnivContext;

namespace UCMRS.Models.View
{
    public class VM_AllocateRooms
    {
        public int Id { get; set; }

        public int DeptId { get; set; }
        public string DeptName { get; set; }
        
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int RoomId { get; set; }
        public string RoomNo { get; set; }
       
        public int DayId { get; set; }
        public string DayName { get; set; }

        public DateTime TimeFrom { get; set; }

       public DateTime TimeTo { get; set; }

        public ICollection<VM_AllocateRooms> Rooms { get; set; }

        public UniContext db = new UniContext();

        public void AllocatedRooms()
        {
            var roomsAllocated = (from ra in db.AllocateRooms
                join dept in db.Departments on ra.DeptId equals dept.Id
                join course in db.Courses on ra.CourseId equals course.Id
                join room in db.Rooms on ra.RoomId equals room.Id
                join day in db.Days on ra.DayId equals day.Id
                select new VM_AllocateRooms()
                {
                    Id = ra.Id, DeptId = dept.Id, DeptName = dept.Name,
                    CourseId = course.Id, CourseName = course.Name, 
                    RoomId = room.Id, RoomNo = room.RoomNo, DayId = day.Id, DayName = day.Name,
                    TimeTo = ra.TimeTo, TimeFrom = ra.TimeFrom
                }).ToList();

            Rooms = roomsAllocated;
        }

        public VM_AllocateRooms AllocatedRoom(int? id)
        {
            var roomsAllocated = (from ra in db.AllocateRooms
                join dept in db.Departments on ra.DeptId equals dept.Id
                join course in db.Courses on ra.CourseId equals course.Id
                join room in db.Rooms on ra.RoomId equals room.Id
                join day in db.Days on ra.DayId equals day.Id

                where ra.Id == id
                select new VM_AllocateRooms()
                {
                    Id = ra.Id,
                    DeptId = dept.Id,
                    DeptName = dept.Name,
                    CourseId = course.Id,
                    CourseName = course.Name,
                    RoomId = room.Id,
                    RoomNo = room.RoomNo,
                    DayId = day.Id,
                    DayName = day.Name,
                    TimeTo = ra.TimeTo,
                    TimeFrom = ra.TimeFrom
                }).FirstOrDefault();

            return roomsAllocated;
        }
    }
}