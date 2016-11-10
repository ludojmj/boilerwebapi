using System.Collections.Generic;
using System.Threading.Tasks;
using BoilerWebApi.Models;

namespace BoilerWebApi.Repository
{
    public interface IProductRepo
    {
        IList<Product> GetProductsFromRepo(int input);
        Task<IList<Product>> GetProductsFromRepoAsync(int input);
    }
}
