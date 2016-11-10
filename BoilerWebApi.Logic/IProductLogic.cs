using System.Collections.Generic;
using System.Threading.Tasks;
using BoilerWebApi.Models;

namespace BoilerWebApi.Logic
{
    // Pas utilisé
    public interface IProductLogic
    {
        IList<Product> GetProductsFromLogic(Product input);
        Task<IList<Product>> GetProductsFromLogicAsync(Product input);
    }
}