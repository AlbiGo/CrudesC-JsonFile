using CodingTaskSmartWork.Controllers;
using CodingTaskSmartWork.Interface;
using CodingTaskSmartWork.IService;
using CodingTaskSmartWork.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace CodingTaskUnitTesting
{
    public class PhoneBookTestUnit
    {
        private readonly IRepository<PhoneBook> _IRepository;
        private readonly IPhoneBookService _phoneBookService;
        private readonly PhoneBooksController _PhoneBooksController;
        private UnitTestService<PhoneBook> _UnitTestService ;
        public PhoneBookTestUnit() //Test Controller Constructor
        {
            _IRepository = new UnitTestService<PhoneBook>();
            _PhoneBooksController = new PhoneBooksController(_phoneBookService);
            _UnitTestService = new UnitTestService<PhoneBook>();
        }
        #region Test Get Method
        [Fact]
        public void GetAllRecords()
        {
            var okResult = _PhoneBooksController.getAllPhoneBooks();
            Assert.Equal(3, okResult.Count);

        }
        #endregion

        #region Test Get By Id Method
        [Fact]
        public void GetByIdNotFoundTestMethod()
        {
            // Act
            string randomGuid = new Guid("617e4975-1253-4ff8-8238-7acc87776e3a").ToString();
            Console.WriteLine(randomGuid);
            var notFoundResult = _PhoneBooksController.FindById(randomGuid).Result as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        public void GetByIdTestMethod()
        {
            // Act
            string randomGuid = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a").ToString();
            Console.WriteLine(randomGuid);
            var okResult = _PhoneBooksController.FindById(randomGuid).Result as OkObjectResult;

            // Assert
            Assert.IsType<PhoneBook>(okResult.Value);
            Assert.Equal(randomGuid, ((okResult.Value as PhoneBook).id).ToString());

            // Assert
        }
        #endregion

        #region Test Post Method

        [Fact]
        public void AddInvalidObjecy()
        {
            // Arrange
            var newPhoneBook = new PhoneBook()
            {
                FirstName = "UnitTest",
                LastName = "UnitTest",
                PhoneNumber = "999999"
            };
            _PhoneBooksController.ModelState.AddModelError("FirstName", "Required");
            _PhoneBooksController.ModelState.AddModelError("LastName", "Required");
            _PhoneBooksController.ModelState.AddModelError("PhoneNumber", "Required");
            _PhoneBooksController.ModelState.AddModelError("TypeID", "Required");
            // Act
            var badResponse = _PhoneBooksController.AddPhoneBook(newPhoneBook).Result as ObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void AddValidRecord()
        {
            // arrange
            var newPhoneBook = new PhoneBook()
            {
                FirstName = "UnitTest",
                LastName = "UnitTest",
                PhoneNumber = "999999",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };

            // Act
            var createdResponse = _PhoneBooksController.AddPhoneBook(newPhoneBook).Result as CreatedAtActionResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

        }


        [Fact]
        public void returnCreatedRecord()
        {
            // Arrange
            var newPhoneBook = new PhoneBook()
            {
                FirstName = "UnitTest",
                LastName = "UnitTest",
                PhoneNumber = "999999",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };

            // Act
            var createdResponse = _PhoneBooksController.AddPhoneBook(newPhoneBook).Result as CreatedAtActionResult;
            var createdRecord = createdResponse.Value as PhoneBook;

            // Assert
            Assert.IsType<PhoneBook>(createdRecord);
            Assert.Equal("UnitTest", createdRecord.FirstName);
        }
        #endregion

        #region Test Delete Method
        [Fact]
        public void DeleteNonEgzistingObject()
        {
            var newPhoneBook = new PhoneBook()
            {
                id = Guid.NewGuid(),
                FirstName = "UnitTest",
                LastName = "UnitTest",
                PhoneNumber = "999999",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };
            var notFoundResult = _PhoneBooksController.Delete(newPhoneBook).Result as NotFoundResult;
            Assert.IsType<NotFoundResult>(notFoundResult);

        }

        [Fact]
        public void DeleteEgzistingObject()
        {

            var newPhoneBook = new PhoneBook()
            {
                id = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a"),
                FirstName = "Mock1",
                LastName = "Mock1",
                PhoneNumber = "266681684",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };
            var okResult = _PhoneBooksController.Delete(newPhoneBook).Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);
        }
        public void returnDeletedObject()
        {

            var newPhoneBook = new PhoneBook()
            {
                id = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a"),
                FirstName = "Mock1",
                LastName = "Mock1",
                PhoneNumber = "266681684",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };
            var okResult = _PhoneBooksController.Delete(newPhoneBook).Result as OkObjectResult;
            var deletedRecord = okResult.Value as PhoneBook;

            Assert.IsType<PhoneBook>(deletedRecord);
            Assert.Equal("Mock1", deletedRecord.FirstName);
        }
        #endregion
        #region Test Put Method
        [Fact]
        public void UpdateNonEgzistingObject()
        {
            var newPhoneBook = new PhoneBook()
            {
                id = Guid.NewGuid(),
                FirstName = "UnitTest",
                LastName = "UnitTest",
                PhoneNumber = "999999",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };
            var notFoundResult = _PhoneBooksController.Delete(newPhoneBook).Result as NotFoundResult;
            Assert.IsType<NotFoundResult>(notFoundResult);

        }
        [Fact]
        public void UpdateEgzistingObject()
        {
            var newPhoneBook = new PhoneBook()
            {
                id = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a"),
                FirstName = "Mock7",
                LastName = "Mock7",
                PhoneNumber = "85882852",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };
            var okresult = _PhoneBooksController.Delete(newPhoneBook).Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(okresult);

        }
        [Fact]
        public void returnUpdatedObject()
        {
            var newPhoneBook = new PhoneBook()
            {
                id = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a"),
                FirstName = "Mock7",
                LastName = "Mock7",
                PhoneNumber = "85882852",
                TypeID = new Guid("617e4975-1253-4ff8-8238-7acc67db6e3a")
            };
            var okresult = _PhoneBooksController.Delete(newPhoneBook).Result as OkObjectResult;
            var updatedRecord = okresult.Value as PhoneBook;

            // Assert
            Assert.IsType<PhoneBook>(updatedRecord);
            Assert.Equal("Mock7", updatedRecord.FirstName);

        }
        #endregion
    }
}
