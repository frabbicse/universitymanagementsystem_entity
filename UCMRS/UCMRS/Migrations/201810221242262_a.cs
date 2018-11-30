namespace UCMRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllocateRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        TimeFrom = c.DateTime(nullable: false),
                        TimeTo = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Days", t => t.DeptId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.DeptId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        DeptId = c.Int(nullable: false),
                        SemId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Semester_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: false)
                .ForeignKey("dbo.Semesters", t => t.Semester_Id)
                .ForeignKey("dbo.Semesters", t => t.SemId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.DeptId)
                .Index(t => t.SemId)
                .Index(t => t.TeacherId)
                .Index(t => t.Semester_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 7),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 60, unicode: false),
                        ContactNo = c.String(nullable: false),
                        DesigId = c.Int(nullable: false),
                        DeptId = c.Int(nullable: false),
                        CreditTaken = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainingCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: false)
                .ForeignKey("dbo.Designations", t => t.DesigId, cascadeDelete: true)
                .Index(t => t.Email, unique: true, name: "UX_Teacher_Email")
                .Index(t => t.DesigId)
                .Index(t => t.DeptId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnrollCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        DeptId = c.Int(nullable: false),
                        RegNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: true)
                .Index(t => t.DeptId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GradeName = c.String(),
                        GradePoint = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.GradeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentResults", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentResults", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.StudentResults", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.EnrollCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "DeptId", "dbo.Departments");
            DropForeignKey("dbo.EnrollCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AllocateRooms", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.AllocateRooms", "DeptId", "dbo.Departments");
            DropForeignKey("dbo.AllocateRooms", "DeptId", "dbo.Days");
            DropForeignKey("dbo.AllocateRooms", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "DesigId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "DeptId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "SemId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "DeptId", "dbo.Departments");
            DropIndex("dbo.StudentResults", new[] { "GradeId" });
            DropIndex("dbo.StudentResults", new[] { "CourseId" });
            DropIndex("dbo.StudentResults", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "DeptId" });
            DropIndex("dbo.EnrollCourses", new[] { "CourseId" });
            DropIndex("dbo.EnrollCourses", new[] { "StudentId" });
            DropIndex("dbo.Teachers", new[] { "DeptId" });
            DropIndex("dbo.Teachers", new[] { "DesigId" });
            DropIndex("dbo.Teachers", "UX_Teacher_Email");
            DropIndex("dbo.Departments", new[] { "Code" });
            DropIndex("dbo.Courses", new[] { "Semester_Id" });
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropIndex("dbo.Courses", new[] { "SemId" });
            DropIndex("dbo.Courses", new[] { "DeptId" });
            DropIndex("dbo.AllocateRooms", new[] { "RoomId" });
            DropIndex("dbo.AllocateRooms", new[] { "CourseId" });
            DropIndex("dbo.AllocateRooms", new[] { "DeptId" });
            DropTable("dbo.StudentResults");
            DropTable("dbo.Grades");
            DropTable("dbo.Students");
            DropTable("dbo.EnrollCourses");
            DropTable("dbo.Rooms");
            DropTable("dbo.Days");
            DropTable("dbo.Designations");
            DropTable("dbo.Teachers");
            DropTable("dbo.Semesters");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.AllocateRooms");
        }
    }
}
