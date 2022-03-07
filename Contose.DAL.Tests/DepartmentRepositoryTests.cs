using Contose.DAL.Core.Reposotories;
using Contoso.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contose.DAL.Tests
{
    [TestFixture]
    public class DepartmentRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> options;
        private Department dep1 = new Department { Id = 1, Name = "IT" };
        private Department dep2 = new Department { Id = 2, Name = "Sales" };

        [SetUp]
        protected void InitialSetup()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase<ApplicationDbContext>(databaseName: "temp_db").Options;
        }

        [Test]
        public async Task EditDepartment_InputDep2_CompareEditedItem()
        {
            var expectedResult = new Department { Id = 2, Name = "Marketing" };
            using(var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new DepartmentRepository(context);
                await repository.Add(dep2);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var entity = await context.Departments.FirstOrDefaultAsync();
                entity.Name = expectedResult.Name;
                var repository = new DepartmentRepository(context);
                repository.Edit(entity);
                await repository.SaveChanges();
            }

            using(var context = new ApplicationDbContext(options))
            {
                var result = await context.Departments.FirstOrDefaultAsync();

                Assert.AreEqual(expectedResult.Name, result.Name);
            }
        }

        [Test]
        public async Task DeleteDepartment_InputDepartmentOne_CheckIfDeleted()
        {
            using(var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var respository = new DepartmentRepository(context);
                await respository.Add(dep1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var entity = await context.Departments.FirstOrDefaultAsync();
                var repository = new DepartmentRepository(context);
                repository.Delete(entity);
                await repository.SaveChanges();
            }

            using(var context = new ApplicationDbContext(options))
            {
                var result = await context.Departments.FirstOrDefaultAsync();

                Assert.IsNull(result);
            }
        }

        [Test]
        [Order(1)]
        public async Task AddDepartment_InputDep1_CompareSavedEntity()
        {
            using(var context = new ApplicationDbContext(options))
            {
                var repository = new DepartmentRepository(context);
                await repository.Add(dep1);
            }

            using(var context = new ApplicationDbContext(options))
            {
                var result = await context.Departments.FirstOrDefaultAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(dep1.Id, result.Id);
                Assert.AreEqual(dep1.Name, result.Name);
            }
        }

        [Test]
        [Order(2)]
        public async Task GetAllDepartments_DepOneAndTwo_CheckBothBookingFromDB()
        {
            //Arrange
            var expectedResult = new List<Department> { dep1, dep2 };

            using(var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new DepartmentRepository(context);
                await repository.Add(dep1);
                await repository.Add(dep2);
            }

            //Act
            List<Department> result;
            using(var context = new ApplicationDbContext(options))
            {
                var repository = new DepartmentRepository(context);
                var items = await repository.GetAll();
                result = items.ToList();
            }

            //Assert
            CollectionAssert.AreEqual(expectedResult, result, new DepartmentComparer());
        }

        private class DepartmentComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var dep1 = (Department)x;
                var dep2 = (Department)y;

                if(dep1.Id != dep2.Id)
                {
                    return 1;
                }    
                if(dep1.Name != dep2.Name)
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
