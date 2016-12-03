using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;

namespace BoilerWebApi.Controllers
{
    /// <summary>
    /// Test our user message when id == 1
    /// Test normal operation when id != 1
    /// </summary>
    public class ProductController : ApiController
    {
        private readonly IProductRepo _repo;

        public ProductController()
        {
            _repo = new ProductRepo();
        }

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        public IHttpActionResult Get(int id)
        {
            IList<Product> result = _repo.GetProductsFromRepo(id);
            return Ok(result);
        }

        [Route("api/product/async")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            IList<Product> result = await _repo.GetProductsFromRepoAsync(id).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
