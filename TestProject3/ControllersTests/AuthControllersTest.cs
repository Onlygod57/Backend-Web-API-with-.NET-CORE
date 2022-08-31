using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace TestProject3.ControllersTests
{
    [TestClass]
    public class AuthControllersTest
    {
        //Projede Business bölümü test edildiği için bu bölümde Businessları Mock ile tanımlayarak projenin test aşamasındaki süre kaybının önlenmesini istedim.
        Mock<IAuthService> _mockAuthService;
        Mock<ITokenHelper> _mockTokenHelper;

        List<User> _dbUsers;
        List<OperationClaim>? _dbClaims;
        List<UserOperationClaim> _dbUserClaims;

        [TestInitialize]
        public void Start()
        {
            _mockAuthService = new Mock<IAuthService>();
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

            _mockAuthService.Setup(th => th.CreateAccessToken(It.IsAny<User>())).Returns(new IResult<AccessToken>(Messages.AccessTokenCreated));
        }

        [TestMethod]
        public void AuthShouldBeAbleToBeSuccessfullyCreateRegisterAndReturn200StatusCode()
        {
            //Arranges
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

            _mockAuthService.Setup(a => a.UserExists(It.IsAny<string>())).Returns(new SuccessResult(userForRegisterDto.Email));
            _mockAuthService.Setup(u => u.Register(It.IsAny<UserForRegisterDto>(), It.IsAny<string>())).Returns(new IResult<User>(user, Messages.UserRegistered));

            var controller = new AuthController(_mockAuthService.Object);

            //Act
            var result = (OkObjectResult)controller.Register(userForRegisterDto);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AuthShouldBeAbleToBeSuccessfullyCreateLoginAndReturn200StatusCode()
        {
            //Arranges
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
            UserForLoginDto userForLoginDto = new UserForLoginDto
            {
                Email = email,
                Password = passwd,
                
            };

            var controller = new AuthController(_mockAuthService.Object);
            _mockAuthService.Setup(a=> a.Login(It.IsAny<UserForLoginDto>())).Returns(new IResult<User>(Messages.SuccessfulLogin));

            //Act
            var result = (OkObjectResult)controller.Login(userForLoginDto);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }
    }
}
