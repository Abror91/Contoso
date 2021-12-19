using Contose.DAL.Core.IReposotories;
using Contoso.Models.DTOs;
using Contoso.Models.Entities;
using Contoso.Services.Services;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.Tests
{
    [TestFixture]
    public class StudentServiceTests
    {
        private StudentService _studentService;
        private Mock<IStudentRepository> studentRepMock;
        private Student stud1 = new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com" };
        private Student stud2 = new Student { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@doe.com" };

        private StudentDTO studDto1 = new StudentDTO { Id = 1, Fullname = "John Doe", Email = "john@doe.com" };
        private StudentDTO studDto2 = new StudentDTO { Id = 2, Fullname = "Jane Doe", Email = "jane@doe.com" };

        [SetUp]
        protected void InitialSetup()
        {
            studentRepMock = new Mock<IStudentRepository>();
            _studentService = new StudentService(studentRepMock.Object);
        }

        [Test]
        public async Task GetStudents_InvokeMethod_CheckIfCalled()
        {
            await _studentService.GetSetudents();

            studentRepMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public async Task GetStudents_InvokeMethod_ReturnStudOneAndTwo()
        {
            var entities = new List<Student> { stud1, stud2 };
            var expectedResult = new List<StudentDTO>() { studDto1, studDto2 };

            studentRepMock.Setup(s => s.GetAll()).ReturnsAsync(entities);

            var result = await _studentService.GetSetudents();

            CollectionAssert.AreEqual(expectedResult, result, new StudentComparer());
        }

        [Test]
        public async Task GetById_InputStudOne_CompareResult()
        {
            studentRepMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(stud1);

            var result = await _studentService.GetById(1);

            CollectionAssert.AreEqual(new List<StudentDTO> { studDto1 }, new List<StudentDTO> { result }, new StudentComparer());
        }

        private class StudentComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var stud1 = (StudentDTO)x;
                var stud2 = (StudentDTO)y;

                if (stud1.Id != stud2.Id)
                {
                    return 1;
                }
                if (stud1.Fullname != stud2.Fullname)
                {
                    return 1;
                }
                if (stud1.Email != stud2.Email)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
