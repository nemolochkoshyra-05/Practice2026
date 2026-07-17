using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using task02;

namespace task02tests
{
    [TestFixture]
    public class StudentServiceTests
    {
        private List<Student> _testStudents;
        private StudentService _service;

        [SetUp]
        public void Setup()
        {
            _testStudents = new List<Student>
            {
                new Student { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
                new Student { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
                new Student { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
            };
            _service = new StudentService(_testStudents);
        }

        [Test]
        public void GetStudentsByFaculty_ReturnsCorrectStudents()
        {
            var result = _service.GetStudentsByFaculty("ФИТ").ToList();
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(s => s.Faculty == "ФИТ"), Is.True);
        }

        [Test]
        public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
        {
            var result = _service.GetFacultyWithHighestAverageGrade();
            Assert.That(result, Is.EqualTo("Экономика"));
        }

        [Test]
        public void GetStudentsWithMinAverageGrade_ReturnsOnlyHighAchievers()
        {
            var result = _service.GetStudentsWithMinAverageGrade(4.5).ToList();
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetStudentsOrderedByName_ReturnsSortedList()
        {
            var result = _service.GetStudentsOrderedByName().ToList();
            Assert.That(result[0].Name, Is.EqualTo("Анна"));
            Assert.That(result[1].Name, Is.EqualTo("Иван"));
        }

        [Test]
        public void GroupStudentsByFaculty_ReturnsCorrectLookup()
        {
            var lookup = _service.GroupStudentsByFaculty();
            Assert.That(lookup["ФИТ"].Count(), Is.EqualTo(2));
            Assert.That(lookup["Экономика"].Count(), Is.EqualTo(1));
        }
    }
}