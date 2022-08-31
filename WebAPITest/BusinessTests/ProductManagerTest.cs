using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;

namespace WebAPITest.BusinessTests
{
    public class ProductManagerTest
    {

        Mock<IProductDal> _mockProductDal;

        [Test]
        [Fact]
        public void ProductShouldBeAbleToBeSuccessfullyAdded()
        {
            //Arrange
            IProductService productService = new ProductManager()
            //Act

            //Assert
        }

    }
}
