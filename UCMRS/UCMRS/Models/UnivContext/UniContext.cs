using System.Data.Entity;
using UCMRS.Models.DB;
using UCMRS.Models.Entity;

namespace UCMRS.Models.UnivContext
{
    public class UniContext: DbContext
    {
        public UniContext(): base("UniCRMSystem")
        {
            
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AllocateRoom> AllocateRooms { get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<AssignCourse> CourseAssigns { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentResult> StudentsResults { get; set; }



        public DbSet<Day> Days { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Semester> Semesters { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasRequired(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DeptId);

            //Student
            modelBuilder.Entity<Student>().HasRequired(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DeptId);

            //Allocate Rooms
            modelBuilder.Entity<AllocateRoom>().HasRequired(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DeptId);

            modelBuilder.Entity<AllocateRoom>().HasRequired(c => c.Course)
                .WithMany()
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<AllocateRoom>().HasRequired(c => c.Room)
                .WithMany()
                .HasForeignKey(c => c.RoomId);

            modelBuilder.Entity<AllocateRoom>().HasRequired(c => c.Day)
                .WithMany()
                .HasForeignKey(c => c.DeptId);

            //Enrol Course
            modelBuilder.Entity<EnrollCourse>().HasRequired(c => c.Student) // relation with Student
                .WithMany()
                .HasForeignKey(c => c.StudentId);

            modelBuilder.Entity<EnrollCourse>().HasRequired(c => c.Course) // relation with Course
                .WithMany()
                .HasForeignKey(c => c.CourseId);

            //Student Result
            modelBuilder.Entity<StudentResult>().HasRequired(c => c.Student) // relation with Student
                .WithMany()
                .HasForeignKey(c => c.StudentId);

            modelBuilder.Entity<StudentResult>().HasRequired(c => c.Course) // relation with Course
                .WithMany()
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<StudentResult>().HasRequired(c => c.Grade) // relation with Grade
                .WithMany()
                .HasForeignKey(c => c.GradeId);

            // Course 
            modelBuilder.Entity<Course>().HasRequired(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DeptId);

            modelBuilder.Entity<Course>().HasRequired(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId);

            modelBuilder.Entity<Course>().HasRequired(c => c.Semester)
                .WithMany()
                .HasForeignKey(c => c.SemId);

            //Teacher
            modelBuilder.Entity<Teacher>().HasRequired(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DeptId);

            modelBuilder.Entity<Teacher>().HasRequired(c => c.Designation)
                .WithMany()
                .HasForeignKey(c => c.DesigId);

            //semester
            modelBuilder.Entity<Student>().HasRequired(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DeptId);
        }
    }
}