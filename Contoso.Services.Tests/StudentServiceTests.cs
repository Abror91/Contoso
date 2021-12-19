using Contose.DAL.Core.IReposotories;
using Contoso.Models.DTOs;
using Contoso.Models.Entities;
using Contoso.Models.ViewModels;
using Contoso.Services.Services;
using Moq;
using NUnit.Framework;
using System;
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
        private Student stud1 = new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com", DepartmentId = 1 };
        private Student stud2 = new Student { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@doe.com", DepartmentId = 1 };

        private StudentDTO studDto1 = new StudentDTO { Id = 1, Fullname = "John Doe", Email = "john@doe.com" };
        private StudentDTO studDto2 = new StudentDTO { Id = 2, Fullname = "Jane Doe", Email = "jane@doe.com" };

        private AddStudentViewModel addModel = new AddStudentViewModel { FirstName = "Simon", LastName = "Smith", Email = "simon@smith.com", DepartmentId = 1 };

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
        [Order(1)]
        public async Task GetStudents_InvokeMethod_ReturnStudOneAndTwo()
        {
            var entities = new List<Student> { stud1, stud2 };
            var expectedResult = new List<StudentDTO>() { studDto1, studDto2 };

            studentRepMock.Setup(s => s.GetAll()).ReturnsAsync(entities);

            var result = await _studentService.GetSetudents();

            CollectionAssert.AreEqual(expectedResult, result, new StudentComparer());
        }

        [Test]
        [Order(2)]
        public async Task GetById_InputStudOne_CompareResult()
        {
            studentRepMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(stud1);

            var result = await _studentService.GetById(1);

            CollectionAssert.AreEqual(new List<StudentDTO> { studDto1 }, new List<StudentDTO> { result }, new StudentComparer());
        }
        
        [Test]
        [Order(3)]
        public void AddStudent_InputNull_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _studentService.Add(null));

            Assert.AreEqual("Value cannot be null. (Parameter 'model')", exception.Message);
        }

        [Test]
        [Order(4)]
        public async Task AddStudent_InputNewAddModel_ReturnsSuccess()
        {
            Student savedItem = null;
            studentRepMock.Invocations.Clear();
            studentRepMock.Setup(s => s.Add(It.IsAny<Student>())).Callback((Student student) =>
            {
                savedItem = student;
            });

            await _studentService.Add(addModel);

            studentRepMock.Verify(s => s.Add(It.IsAny<Student>()), Times.Once);

            Assert.AreEqual(addModel.FirstName, savedItem.FirstName);
            Assert.AreEqual(addModel.LastName, savedItem.LastName);
            Assert.AreEqual(addModel.Email, savedItem.Email);
            Assert.AreEqual(addModel.DepartmentId, savedItem.DepartmentId);
        }

        [Test]
        [Order(5)]
        public async Task DeleteStudent_InputId_VerifyRepDeleteSaveChangesMethodsCalled()
        {
            await _studentService.Delete(1);

            studentRepMock.Verify(s => s.Delete(It.IsAny<int>()), Times.Once);
            studentRepMock.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        [Order(6)]
        public void EditStudent_InputNull_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _studentService.Edit(null));

            Assert.AreEqual("Value cannot be null. (Parameter 'model')", exception.Message);
        }

        [Test]
        [Order(7)]
        public void EditStudent_InputNonExistingStudentId_ThrowsException()
        {
            Student returnItem = null;
            studentRepMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(returnItem);

            var exception = Assert.ThrowsAsync<Exception>(() => _studentService.Edit(new EditStudentViewModel { Id = 1 }));
            Assert.AreEqual("Student not found!", exception.Message);
        }

        [Test]
        [Order(8)]
        public async Task EditStudent_InputValidStudentData_ReturnsSuccess()
        {
            EditStudentViewModel model = new EditStudentViewModel
            {
                Id = stud1.Id,
                FirstName = stud1.FirstName + "1",
                LastName = stud1.LastName + "1",
                Email = stud1.Email + "1",
                DepartmentId = stud1.DepartmentId + 1,
            };

            Student result = null;
            studentRepMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(stud1);
            studentRepMock.Setup(s => s.Edit(It.IsAny<Student>())).Callback((Student student) =>
            {
                result = student;
            });

            await _studentService.Edit(model);

            Assert.AreEqual(model.FirstName, result.FirstName);
            Assert.AreEqual(model.LastName, result.LastName);
            Assert.AreEqual(model.Email, result.Email);
            Assert.AreEqual(model.DepartmentId, result.DepartmentId);
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
