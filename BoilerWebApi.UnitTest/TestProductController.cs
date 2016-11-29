using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;
using BoilerWebApi.SelfHost.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BoilerWebApi.UnitTest
{
    [TestClass]
    public class TestProductController
    {
        private Mock<IProductRepo> _mockService;
        private readonly IList<Product> _dataSource = new AppDb().AppTable;
        private readonly string _firstValue = new AppDb().AppTable.First().Id;

        [TestMethod]
        public void GetList_ShouldReturnAllProducts()
        {
            // Arrange
            _mockService = new Mock<IProductRepo>();
            _mockService.Setup(x => x.GetProductsFromRepo(It.IsAny<int>())).Returns(_dataSource);
            var controller = new ProductController(_mockService.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(int.Parse(_firstValue));
            var contentResult = actionResult as OkNegotiatedContentResult<IList<Product>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_firstValue, contentResult.Content[0].Id);
        }
    }
}
