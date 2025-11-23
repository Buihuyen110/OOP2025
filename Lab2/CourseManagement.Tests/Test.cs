using Xunit;
using CourseManagement; // để truy cập CourseManager, Student, Teacher


namespace CourseManagement.Tests
{
    public class CourseManagerTests
    {
        [Fact]
        public void AddCourse_test1()
        {
            var manager = new CourseManager();
            bool result = manager.AddCourse("Математика", "online", "Zoom");

            Assert.True(result);
            var courses = manager.GetAllCourses();
            Assert.Single(courses);
            Assert.IsType<OnlineCourse>(courses[0]);
            Assert.Equal("Математика", courses[0].Name);
            Assert.Equal("Zoom", ((OnlineCourse)courses[0]).Platform);
        }

        [Fact]
        public void AddCourse_test2()
        {
            var manager = new CourseManager();
            bool result = manager.AddCourse("Физика", "offline", "Аудитория 101");

            Assert.True(result);
            var courses = manager.GetAllCourses();
            Assert.Single(courses);
            Assert.IsType<OfflineCourse>(courses[0]);
            Assert.Equal("Физика", courses[0].Name);
            Assert.Equal("Аудитория 101", ((OfflineCourse)courses[0]).Location);
        }

        [Fact]
        public void RemoveCourse_test()
        {
            var manager = new CourseManager();
            manager.AddCourse("Программирование", "online", "Teams");
            bool removed = manager.RemoveCourse(1);

            Assert.True(removed);
            Assert.Empty(manager.GetAllCourses());
        }

        [Fact]
        public void AssignTeacher_test()
        {
            var manager = new CourseManager();
            manager.AddTeacher("Алиса");
            manager.AddCourse("Математика", "online", "Zoom");

            bool assigned = manager.AssignTeacher(1, 1);
            Assert.True(assigned);
            var courses = manager.GetCoursesByTeacher(1);
            Assert.Single(courses);
            Assert.Equal("Алиса", courses[0].AssignedTeacher?.Name);
        }

        [Fact]
        public void EnrollStudent_test()
        {
            var manager = new CourseManager();
            manager.AddStudent("Джон");
            manager.AddCourse("Математика", "online", "Zoom");

            bool enrolled = manager.EnrollStudent(1, 1);
            Assert.True(enrolled);
            var students = manager.GetStudentsInCourse(1);
            Assert.Single(students);
            Assert.Equal("Джон", students[0].Name);
        }
    }
}
