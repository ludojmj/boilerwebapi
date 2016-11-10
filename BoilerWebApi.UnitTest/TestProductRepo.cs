using System.Collections.Generic;
using System.Linq;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;
using BoilerWebApi.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoilerWebApi.UnitTest
{
    [TestClass]
    public class TestProductRepo
    {
        private readonly IList<Product> _dataSource = new AppDb().AppTable;
        private readonly string _firstValue = new AppDb().AppTable.First().Id;
        private readonly int _errorValue = 0;

        [TestMethod]
        public void GetProductsFromRepo_ShouldReturnAllProducts()
        {
            // arrange
            var service = new ProductRepo();

            // act
            var test = service.GetProductsFromRepo(int.Parse(_firstValue));
            var result = test.First(x => x.Id == _firstValue).Lib;
            var expected = _dataSource.First(x => x.Id == _firstValue).Lib;
            
            // assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Human message for my app exception.")]
        public void GetProductsFromRepo_ShouldReturnBusinessException()
        {
            IProductRepo test = new ProductRepo();
            var result = test.GetProductsFromRepo(_errorValue);
        }
    }
}
