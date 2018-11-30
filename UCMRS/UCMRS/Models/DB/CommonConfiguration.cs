using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCMRS.Migrations;
using UCMRS.Models.Entity;

namespace UCMRS.Models.DB
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class Designation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public decimal GradePoint { get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomNo { get; set; }
    }

    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}