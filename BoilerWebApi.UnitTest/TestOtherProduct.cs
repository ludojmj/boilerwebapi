using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using BoilerWebApi.Controllers;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BoilerWebApi.UnitTest
{
    [TestClass]
    public class TestOtherProduct
    {
        private Mock<IOtherProductRepo> _mockService;
        private readonly IList<Product> _dataSource = new AppDb().AppTable;
        private readonly Product _firstValue = new AppDb().AppTable.First();
        private readonly Product _errorValue = new Product() { Id = "1", Lib = "Label1" };

        /// <summary>
        /// Test OtherProductController
        /// </summary>
        [TestMethod]
        public void OtherProductController_Post_ShouldReturnAllProducts()
        {
            // Arrange
            _mockService = new Mock<IOtherProductRepo>();
            _mockService.Setup(x => x.GetOtherProductsFromRepo(It.IsAny<Product>())).Returns(_dataSource);
            OtherProductController controller = new OtherProductController(_mockService.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(_firstValue);
            var contentResult = actionResult as OkNegotiatedContentResult<IList<Product>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_firstValue.Id, contentResult.Content[0].Id);
            Assert.AreEqual(_firstValue.Lib, contentResult.Content[0].Lib);
        }

        /// <summary>
        /// Test Repo OK
        /// </summary>
        [TestMethod]
        public void GetOtherProductsFromRepo_ShouldReturnAllProducts()
        {
            // arrange
            OtherProductRepo service = new OtherProductRepo();

            // act
            IList<Product> test = service.GetOtherProductsFromRepo(_firstValue);
            string result = test.First(x => x.Id == _firstValue.Id).Lib;
            string expected = _dataSource.First(x => x.Id == _firstValue.Id).Lib;

            // assert
            Assert.AreEqual(result, expected);
        }

        /// <summary>
        /// Test Repo KO
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Bug in application.")]
        public void GetOtherProductsFromRepo_ShouldReturnDivideByZeroException()
        {
            IOtherProductRepo test = new OtherProductRepo();
            IList<Product> result = test.GetOtherProductsFromRepo(_errorValue);
        }
    }
}
