using System.Collections.Generic;
using System.Threading.Tasks;
using BoilerWebApi.Models;
using BoilerWebApi.Shared;

namespace BoilerWebApi.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly IList<Product> _dataSource = new AppDb().AppTable;

        public IList<Product> GetProductsFromRepo(int input)
        {
            if (input == 0)
            {
                throw new BusinessException("Human message for my app exception.");
            }
            var result = _dataSource;
            return result;
        }

        public async Task<IList<Product>> GetProductsFromRepoAsync(int input)
        {
            await Task.Delay(2000).ConfigureAwait(false);
            var result = await Task.FromResult(GetProductsFromRepo(input)).ConfigureAwait(false);
            return result;
        }
    }
}