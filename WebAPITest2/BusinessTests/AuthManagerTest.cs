using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Moq;

namespace WebAPITest2.BusinessTests
{
    [TestClass]
    public class AuthManagerTest
    {
        Mock<IUserDal> _mockUserDal;
        Mock<IUserService> _mockUserService;
        //IUserService _userService;

        Mock<ITokenHelper> _mockTokenHelper;
        List<User> _dbUsers;
        List<OperationClaim> _dbClaims;
        List<UserOperationClaim> _dbUserClaims;

        [TestInitialize]
        public void Start()
        {
            _mockUserDal = new Mock<IUserDal>();
            _mockUserService = new Mock<IUserService>();
            _mockTokenHelper = new Mock<ITokenHelper>();

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
        }

        [TestMethod]
        public void AuthShouldBeAbleToBeSuccessfullyCreateAccessToken()
        {
            //Arranges
            IAuthService authService = new AuthManager(_mockTokenHelper.Object, _mockUserService.Object);
            User user = new User { Id = 1, Email = "ufukcetinkaya_10@hotmail.com", FirstName = "Ufuk", LastName = "ÇETİNKAYA", Status = true, PasswordHash = null, PasswordSalt = null };
            var claim = from operationClaim in _dbClaims
                        join userOperationClaim in _dbUserClaims
                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                        where userOperationClaim.UserId == user.Id
                        select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            //  _mockUserService.Setup(u => u.GetClaims(user)).Returns(claim.ToList());

            //Act
            var result = authService.CreateAccessToken(user);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AuthShouldBeAbleToBeSuccessfullyCreateRegister()
        {
            //Arranges
            IAuthService authService = new AuthManager(_mockTokenHelper.Object, _mockUserService.Object);
            IUserService userService = new UserManager(_mockUserDal.Object);

            string passwd = "123456";
            string email = "ufukcetinkaya-12@hotmail.com";
            string firstName = "Ufuk";
            string lastName = "ÇETİNKAYA";

            User user = new User
            {
                Id = 15,
                Email = passwd,
                LastName = lastName,
                FirstName = firstName,
            };

            UserForRegisterDto userForRegisterDto = new UserForRegisterDto
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = passwd
            };

            _mockUserService.Setup(u => u.Add(It.IsAny<User>())).Callback(
                (User target) =>
                {
                    _dbUsers.Add(target);
                });

            //Act
            var result = authService.Register(userForRegisterDto, passwd);

            //Assert
            Assert.AreEqual(result.Data.Email, _dbUsers[_dbUsers.Count - 1].Email);
        }

        [TestMethod]
        public void AuthShouldBeAbleToBeSuccessfullyLogin()
        {
            //Arranges
            IAuthService authService = new AuthManager(_mockTokenHelper.Object, _mockUserService.Object);

            string passwd = "123456";
            string email = "ufukcetinkaya-12@hotmail.com";
            string firstName = "Ufuk";
            string lastName = "ÇETİNKAYA";
            bool status = true;

            User user = new User
            {
                Id = 5,
                Email = email,
                LastName = lastName,
                FirstName = firstName,
                Status = status
            };

            UserForLoginDto userForLoginDto = new UserForLoginDto
            {
                Email = email,
                Password = passwd,
            };
            UserForRegisterDto userForRegisterDto = new UserForRegisterDto
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = passwd
            };

            _mockUserService.Setup(u => u.Add(It.IsAny<User>())).Callback(
                (User target) =>
                {
                    _dbUsers.Add(target);
                });

            //Act
            authService.Register(userForRegisterDto, passwd);
            _mockUserService.Setup(u => u.GetByEmail(userForLoginDto.Email)).Returns(_dbUsers.Find(o => o.Email == email));

            var result2 = authService.Login(userForLoginDto);

            //Assert
            Assert.AreEqual(result2.Data.Email, _dbUsers[_dbUsers.Count - 1].Email);
        }
        [TestMethod]
        public void AuthShouldBeAbleToBeSuccessfullyCreateUserExists()
        {
            //Arranges
            IAuthService authService = new AuthManager(_mockTokenHelper.Object, _mockUserService.Object);
            IUserService userService = new UserManager(_mockUserDal.Object);
            string email = "ufukcetinkaya-12@hotmail.com";
            _mockUserService.Setup(us => us.GetByEmail(email)).Returns(_dbUsers.Find(u => u.Email == email));

            var result = authService.UserExists(email);
            Assert.IsTrue(result.Success);

        }
    }
}
