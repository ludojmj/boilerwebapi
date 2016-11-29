using System.Collections.Generic;
using System.Threading.Tasks;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;

namespace BoilerWebApi.Logic
{
    // Pas utilisé
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepo _repo;

        public ProductLogic(IProductRepo repo)
        {
            _repo = repo;

        }
        public IList<Product> GetProductsFromLogic(Product input)
        {
            return _repo.GetProductsFromRepo(int.Parse(input.Id));
        }

        public async Task<IList<Product>> GetProductsFromLogicAsync(Product input)
        {
            // Test bug in app
            int a = 0;
            int b = 10;
            var c = b / a;

            await Task.Delay(2000).ConfigureAwait(false);
            return await _repo.GetProductsFromRepoAsync(int.Parse(input.Id)).ConfigureAwait(false);
        }
    }
}