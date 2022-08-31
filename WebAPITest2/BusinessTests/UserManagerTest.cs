using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Moq;

namespace WebAPITest2.BusinessTests
{
    [TestClass]
    public class UserManagerTest
    {
        Mock<IUserDal> _mockUserDal;
        List<User> _dbUsers;
        List<OperationClaim> _dbClaims;
        List<UserOperationClaim> _dbUserClaims;

        [TestInitialize]
        public void Start()
        {
            _mockUserDal = new Mock<IUserDal>();
            _dbUsers = new List<User>
            {
                new User { Id = 1, Email = "ufukcetinkaya_10@hotmail.com", FirstName = "Ufuk", LastName = "ÇETİNKAYA", Status= true, PasswordHash = null , PasswordSalt = null},
                new User { Id = 2, Email = "ufukcetinkaya_1881@gmail.com", FirstName = "Altay", LastName = "MERİÇ", Status= true, PasswordHash = null , PasswordSalt = null},
                new User { Id = 3, Email = "ufukcetinkaya_101@gmail.com", FirstName = "Cem", LastName = "MERİÇ", Status= true, PasswordHash = null , PasswordSalt = null},
                new User { Id = 4, Email = "ufukcetinkaya_8781@hotmail.com", FirstName = "Çetin", LastName = "KAYA", Status= true, PasswordHash = null , PasswordSalt = null},
            };

            _dbClaims = new List<OperationClaim>{
                new OperationClaim{Id = 1, Name = "Ufuk"},
                new OperationClaim {Id = 2, Name = "Altay"}
            };

            _dbUserClaims = new List<UserOperationClaim>
            {
                new UserOperationClaim{Id = 1, UserId= 1 , OperationClaimId = 1},
               new UserOperationClaim{Id = 2, UserId= 2 , OperationClaimId = 2 }
            };

            //Birden fazla fonksiyonda kullanıldığı için bu setup'ı gloablde tanımalamak istedim.
            _mockUserDal.Setup(m => m.GetList(It.IsAny<Expression<Func<User, bool>>>())).Returns(_dbUsers);
        }

        [TestMethod]
        public void UserShouldBeAbleToBeSuccessfullyAdded()
        {
            //Arrange
            IUserService userService = new UserManager(_mockUserDal.Object);
            User newProduct = new User { Id = 9, Email = "ufukcetinkaya_181@hotmail.com", FirstName = "Çetin", LastName = "KAYA", Status = true, PasswordHash = null, PasswordSalt = null };
            _mockUserDal.Setup(mr => mr.Add(It.IsAny<User>())).Callback(
                (User target) =>
                {
                    _dbUsers.Add(target);
                });

            //Act
            var actual = userService.GetList().Data.Count + 1;
            userService.Add(newProduct);
            var expected = userService.GetList().Data.Count;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void UserShouldBeAbleToBeSuccessfullyGetByMail()
        {
            //Arrange
            string email = "ufukcetinkaya_10@hotmail.com";
            //Act
            _mockUserDal.Setup(m => m.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns(_dbUsers.Single(x => x.Email == email));
            IUserService userService = new UserManager(_mockUserDal.Object);
            User users = userService.GetByEmail(email);
            var result = users;

            //Assert
            Assert.AreEqual(result.Email, email);
        }

        [TestMethod]
        public void UserShouldBeAbleToBeSuccessfullyGetClaims()
        {
            //Arrange
            User user = _dbUsers[0];
            var claim = from operationClaim in _dbClaims
                         join userOperationClaim in _dbUserClaims
                         on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

            _mockUserDal.Setup(m => m.GetClaims(user)).Returns(claim.ToList());
            IUserService userService = new UserManager(_mockUserDal.Object);

            //Act
            var result = userService.GetClaims(user);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UserShouldBeAbleToBeSuccessfullyGetById()
        {
            //Arrange
            var testId = 2;

            //Act
            _mockUserDal.Setup(m => m.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns(_dbUsers.Single(x => x.Id == testId));
            IUserService userService = new UserManager(_mockUserDal.Object);
            IDataResult<User> users = userService.GetById(testId);
            var result = users.Data;

            //Assert
            Assert.AreEqual(result.Id, testId);
        }

        [TestMethod]
        public void UserShouldBeAbleToBeSuccessfullyGetList()
        {
            //Arrange
            IUserService userService = new UserManager(_mockUserDal.Object);

            //Act
            IDataResult<List<User>> users = userService.GetList();

            //Assert
            Assert.AreEqual(4, users.Data.Count);
        }

        [TestMethod]
        public void UserShouldBeAbleToBeSuccessfullyUpdated()
        {
            //Arrange
            IUserService userService = new UserManager(_mockUserDal.Object);
            User actual = new User { Id = 2, Email = "samteDemir_1881@gmail.com", FirstName = "Samet", LastName = "Demir", Status = true, PasswordHash = null, PasswordSalt = null };

            _mockUserDal.Setup(m => m.Update(It.IsAny<User>())).Callback(
                (User target) =>
                {
                    var user = _dbUsers.Where(q => q.Id == target.Id).Single();
                    if (user == null)
                    {
                        throw new InvalidOperationException();
                    }
                    user.FirstName = target.FirstName;
                    user.LastName = target.LastName;
                    user.Status = target.Status;
                    user.PasswordSalt = target.PasswordSalt;
                    user.PasswordHash = target.PasswordHash;
                    user.Email = target.Email;
                });
            _mockUserDal.Setup(m => m.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns(_dbUsers.Single(x => x.Id == actual.Id));

            //Act
            userService.Update(actual);
            IDataResult<User> expect = userService.GetById(actual.Id);
            var result = expect.Data;

            //Assert
            Assert.AreEqual(result.Id, actual.Id);

        }

    }
}
