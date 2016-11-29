using System.Collections.Generic;
using System.Threading.Tasks;
using BoilerWebApi.Logic;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;
using BoilerWebApi.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BoilerWebApi.UnitTest
{
    [TestClass]
    public class TestProductLogic
    {
        private Mock<IProductRepo> _mockService;
        private readonly IList<Product> _dataSource = new AppDb().AppTable;
        private readonly int _errorValue = 0;

        [TestMethod]
        public void GetProductsFromLogic_ShouldReturnAllProducts()
        {
            // arrange
            _mockService = new Mock<IProductRepo>();
            _mockService.Setup(x => x.GetProductsFromRepo(It.IsAny<int>())).Returns(_dataSource);
            var service = new ProductLogic(_mockService.Object);

            // act
            var input = new Product {Id = "1"};
            var result = service.GetProductsFromLogic(input);

            // assert
            Assert.AreEqual(result, _dataSource);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Human message for my app exception.")]
        public void GetProductsFromLogic_ShouldReturnBusinessException()
        {
            IProductRepo test = new ProductRepo();
            var result = test.GetProductsFromRepo(_errorValue);
        }


        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException), "Bug in application.")]
        public async Task GetProductsFromLogicAsync_ShouldReturnDivideByZeroException()
        {
            _mockService = new Mock<IProductRepo>();
            _mockService.Setup(x => x.GetProductsFromRepo(It.IsAny<int>())).Returns(_dataSource);
            var service = new ProductLogic(_mockService.Object);

            // act
            var input = new Product { Id = "1" };
            var result = await service.GetProductsFromLogicAsync(input);
        }
    }
}
