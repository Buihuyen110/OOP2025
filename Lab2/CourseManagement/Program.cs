using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagement
{
    // Модели
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public abstract class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Teacher? AssignedTeacher { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();
        public abstract void DisplayInfo();
    }

    public class OnlineCourse : Course
    {
        public string? Platform { get; set; }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Онлайн] Id: {Id}, Название: {Name}, Преподаватель: {AssignedTeacher?.Name ?? "Нет"}, Платформа: {Platform}, Студентов: {EnrolledStudents.Count}");
        }
    }

    public class OfflineCourse : Course
    {
        public string? Location { get; set; }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Офлайн] Id: {Id}, Название: {Name}, Преподаватель: {AssignedTeacher?.Name ?? "Нет"}, Место: {Location}, Студентов: {EnrolledStudents.Count}");
        }
    }

    // Класс для управления бизнес-логикой
    public class CourseManager
    {
        private List<Student> _students = new List<Student>();
        private List<Teacher> _teachers = new List<Teacher>();
        private List<Course> _courses = new List<Course>();
        private int _nextStudentId = 1;
        private int _nextTeacherId = 1;
        private int _nextCourseId = 1;

        // Добавить студента (для тестов или расширения)
        public void AddStudent(string? name)
        {
            _students.Add(new Student { Id = _nextStudentId++, Name = name });
        }

        // Добавить преподавателя (для тестов или расширения)
        public void AddTeacher(string? name)
        {
            _teachers.Add(new Teacher { Id = _nextTeacherId++, Name = name });
        }

        // Добавить курс
        public bool AddCourse(string? name, string type, string? platformOrLocation)
        {
            Course? course = null;
            if (type.ToLower() == "online")
            {
                course = new OnlineCourse { Id = _nextCourseId++, Name = name, Platform = platformOrLocation };
            }
            else if (type.ToLower() == "offline")
            {
                course = new OfflineCourse { Id = _nextCourseId++, Name = name, Location = platformOrLocation };
            }
            else
            {
                return false; // Неверный тип
            }
            _courses.Add(course);
            return true;
        }

        // Удалить курс
        public bool RemoveCourse(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                _courses.Remove(course);
                return true;
            }
            return false;
        }

        // Назначить преподавателя на курс
        public bool AssignTeacher(int courseId, int teacherId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            var teacher = _teachers.FirstOrDefault(t => t.Id == teacherId);
            if (course != null && teacher != null)
            {
                course.AssignedTeacher = teacher;
                return true;
            }
            return false;
        }

        // Записать студента на курс
        public bool EnrollStudent(int courseId, int studentId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            var student = _students.FirstOrDefault(s => s.Id == studentId);
            if (course != null && student != null && !course.EnrolledStudents.Contains(student))
            {
                course.EnrolledStudents.Add(student);
                return true;
            }
            return false;
        }

        // Получить все курсы преподавателя
        public List<Course> GetCoursesByTeacher(int teacherId)
        {
            return _courses.Where(c => c.AssignedTeacher?.Id == teacherId).ToList();
        }

        // Получить всех студентов на курсе
        public List<Student> GetStudentsInCourse(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            return course?.EnrolledStudents ?? new List<Student>();
        }

        // Получить все курсы
        public List<Course> GetAllCourses()
        {
            return _courses;
        }

        // Вспомогательные методы для тестов
        public List<Student> GetStudents() => _students;
        public List<Teacher> GetTeachers() => _teachers;
    }

    // Главная программа
    class Program
    {
        static CourseManager manager = new CourseManager();

        static void Main()
        {
            SeedData(); // пример данных
            while (true)
            {
                Console.WriteLine("\n--- Система управления курсами ---");
                Console.WriteLine("1. Добавить курс");
                Console.WriteLine("2. Удалить курс");
                Console.WriteLine("3. Назначить преподавателя на курс");
                Console.WriteLine("4. Записать студента на курс");
                Console.WriteLine("5. Показать все курсы");
                Console.WriteLine("6. Показать курсы преподавателя");
                Console.WriteLine("7. Показать студентов на курсе");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddCourse(); break;
                    case "2": RemoveCourse(); break;
                    case "3": AssignTeacher(); break;
                    case "4": EnrollStudent(); break;
                    case "5": ShowAllCourses(); break;
                    case "6": ShowCoursesByTeacher(); break;
                    case "7": ShowStudentsInCourse(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор"); break;
                }
            }
        }

        static void SeedData()
        {
            // Добавить преподавателей
            manager.AddTeacher("Алиса");
            manager.AddTeacher("Боб");
            manager.AddTeacher("Марина");
            manager.AddTeacher("Иван");

            // Добавить студентов
            manager.AddStudent("Джон");
            manager.AddStudent("Мария");
            manager.AddStudent("Кира");
            manager.AddStudent("Лиза");

            // Добавить курсы
            manager.AddCourse("Математика", "online", "Zoom");
            manager.AddCourse("Физика", "offline", "Аудитория 101");
            manager.AddCourse("Программирование", "online", "Microsoft Teams");

            // Назначить преподавателей на курсы
            manager.AssignTeacher(1, 1); // Алиса на Математика 
            manager.AssignTeacher(2, 2); // Боб на Физика 
            manager.AssignTeacher(3, 1); // Алиса на Программирование 

            // Записать студентов на курсы
            manager.EnrollStudent(1, 1); // Джон на Математика 
            manager.EnrollStudent(1, 2); // Мария на Математика 
            manager.EnrollStudent(2, 1); // Джон на Физика 
            manager.EnrollStudent(3, 2); // Мария на Программирование 
        }

        static void AddCourse()
        {
            Console.Write("Название курса: ");
            string? name = Console.ReadLine();  // Sửa thành string? để tránh warning
            Console.WriteLine("Выберите тип курса: 1. Онлайн 2. Офлайн");
            Console.Write("Введите номер: ");
            string? typeInput = Console.ReadLine();  // Đã là string?, giữ nguyên
            string type = typeInput == "1" ? "online" : typeInput == "2" ? "offline" : "";
            if (string.IsNullOrEmpty(type)) { Console.WriteLine("Неверный выбор типа!"); return; }
            Console.Write(type == "online" ? "Платформа: " : "Место проведения: ");
            string? param = Console.ReadLine();  // Đã là string?, giữ nguyên
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(param))
            {
                Console.WriteLine("Название курса и платформа/место не могут быть пустыми!");
                return;
            }
            if (manager.AddCourse(name, type, param))
            {
                Console.WriteLine("Курс добавлен успешно!");
                // Автоматически показать информацию о добавленном курсе
                var newCourse = manager.GetAllCourses().Last(); // Получить последний добавленный курс
                newCourse.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Ошибка добавления курса!");
            }
        }

        static void RemoveCourse()
        {
            Console.Write("Введите Id курса для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.RemoveCourse(id))
                    Console.WriteLine("Курс удален.");
                else
                    Console.WriteLine("Курс не найден.");
            }
        }

        static void AssignTeacher()
        {
            Console.Write("Введите Id курса: ");
            int.TryParse(Console.ReadLine(), out int courseId);
            Console.WriteLine("Преподаватели:");
            foreach (var t in manager.GetTeachers()) Console.WriteLine($"{t.Id}: {t.Name}");
            Console.Write("Введите Id преподавателя: ");
            int.TryParse(Console.ReadLine(), out int teacherId);
            if (!manager.AssignTeacher(courseId, teacherId))
                Console.WriteLine("Ошибка назначения преподавателя.");
            else
                Console.WriteLine("Преподаватель назначен.");
        }

        static void EnrollStudent()
        {
            Console.Write("Введите Id курса: ");
            int.TryParse(Console.ReadLine(), out int courseId);
            Console.WriteLine("Студенты:");
            foreach (var s in manager.GetStudents()) Console.WriteLine($"{s.Id}: {s.Name}");
            Console.Write("Введите Id студента: ");
            int.TryParse(Console.ReadLine(), out int studentId);
            if (!manager.EnrollStudent(courseId, studentId))
                Console.WriteLine("Ошибка записи студента.");
            else
                Console.WriteLine("Студент записан на курс.");
        }

        static void ShowAllCourses()
        {
            foreach (var c in manager.GetAllCourses()) c.DisplayInfo();
        }

        static void ShowCoursesByTeacher()
        {
            Console.WriteLine("Преподаватели:");
            foreach (var t in manager.GetTeachers()) Console.WriteLine($"{t.Id}: {t.Name}");
            Console.Write("Введите Id преподавателя: ");
            int.TryParse(Console.ReadLine(), out int teacherId);
            var courses = manager.GetCoursesByTeacher(teacherId);
            if (courses.Count == 0) Console.WriteLine("Нет назначенных курсов.");
            else foreach (var c in courses) c.DisplayInfo();
        }

        static void ShowStudentsInCourse()
        {
            Console.Write("Введите Id курса: ");
            int.TryParse(Console.ReadLine(), out int courseId);
            var students = manager.GetStudentsInCourse(courseId);
            if (students.Count == 0) Console.WriteLine("Нет студентов на курсе.");
            else
            {
                Console.WriteLine("Студенты на курсе:");
                foreach (var s in students) Console.WriteLine($"{s.Id}: {s.Name}");
            }
        }
    }
}