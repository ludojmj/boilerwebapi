using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;
using BoilerWebApi.Controllers;
using BoilerWebApi.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BoilerWebApi.UnitTest
{
    [TestClass]
    public class TestProduct
    {
        private Mock<IProductRepo> _mockService;
        private readonly IList<Product> _dataSource = new AppDb().AppTable;
        private readonly string _firstValue = new AppDb().AppTable.First().Id;
        private readonly int _errorValue = 1;

        /// <summary>
        /// Test ProductController
        /// </summary>
        [TestMethod]
        public void ProductController_Get_ShouldReturnAllProducts()
        {
            // Arrange
            _mockService = new Mock<IProductRepo>();
            _mockService.Setup(x => x.GetProductsFromRepo(It.IsAny<int>())).Returns(_dataSource);
            ProductController controller = new ProductController(_mockService.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(int.Parse(_firstValue));
            var contentResult = actionResult as OkNegotiatedContentResult<IList<Product>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_firstValue, contentResult.Content[0].Id);
        }

        /// <summary>
        /// Test Repo OK
        /// </summary>
        [TestMethod]
        public void GetProductsFromRepo_ShouldReturnAllProducts()
        {
            // arrange
            IProductRepo service = new ProductRepo();

            // act
            IList<Product> test = service.GetProductsFromRepo(int.Parse(_firstValue));
            string result = test.First(x => x.Id == _firstValue).Lib;
            string expected = _dataSource.First(x => x.Id == _firstValue).Lib;

            // assert
            Assert.AreEqual(result, expected);
        }

        /// <summary>
        /// Test Repo KO
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Human message for my business error.")]
        public void GetProductsFromRepo_ShouldReturnBusinessException()
        {
            IProductRepo test = new ProductRepo();
            test.GetProductsFromRepo(_errorValue);
        }
    }
}
