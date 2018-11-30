namespace UCMRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _as : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AssignCourses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.AssignCourses", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Courses", "SemId", "dbo.Semesters");
            DropIndex("dbo.AssignCourses", new[] { "TeacherId" });
            DropIndex("dbo.AssignCourses", new[] { "CourseId" });
            DropIndex("dbo.AssignCourses", new[] { "Department_Id" });
            DropPrimaryKey("dbo.Teachers");
            AddColumn("dbo.Courses", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "Semester_Id", c => c.Int());
            AddColumn("dbo.Teachers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Teachers", "Id");
            CreateIndex("dbo.Courses", "TeacherId");
            CreateIndex("dbo.Courses", "Semester_Id");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "Semester_Id", "dbo.Semesters", "Id");
            DropColumn("dbo.Teachers", "TeacherId");
            DropTable("dbo.AssignCourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AssignCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Teachers", "TeacherId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Courses", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Courses", new[] { "Semester_Id" });
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropPrimaryKey("dbo.Teachers");
            DropColumn("dbo.Teachers", "Id");
            DropColumn("dbo.Courses", "Semester_Id");
            DropColumn("dbo.Courses", "IsActive");
            DropColumn("dbo.Courses", "TeacherId");
            AddPrimaryKey("dbo.Teachers", "TeacherId");
            CreateIndex("dbo.AssignCourses", "Department_Id");
            CreateIndex("dbo.AssignCourses", "CourseId");
            CreateIndex("dbo.AssignCourses", "TeacherId");
            AddForeignKey("dbo.Courses", "SemId", "dbo.Semesters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignCourses", "Department_Id", "dbo.Departments", "Id");
            AddForeignKey("dbo.AssignCourses", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
            AddForeignKey("dbo.AssignCourses", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
