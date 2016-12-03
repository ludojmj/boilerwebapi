using System.Collections.Generic;
using System.Threading.Tasks;
using BoilerWebApi.Models;

namespace BoilerWebApi.Repository
{
    public interface IOtherProductRepo
    {
        IList<Product> GetOtherProductsFromRepo(Product input);
        Task<IList<Product>> GetOtherProductsFromRepoAsync(Product input);
    }
}
