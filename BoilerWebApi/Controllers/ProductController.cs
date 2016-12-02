using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BoilerWebApi.Logic;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;

namespace BoilerWebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductRepo _repo;
        private readonly IProductLogic _repo2;

        public ProductController()
        {
            _repo = new ProductRepo();
            _repo2 = new ProductLogic(_repo);
        }

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        // Test OK
        [Route("api/product/async")]
        public async Task<IHttpActionResult> GetAsync(int input)
        {
            IList<Product> result = await _repo.GetProductsFromRepoAsync(input).ConfigureAwait(false);
            return Ok(result);
        }

        // Test BusinessException
        public IHttpActionResult Get(int input)
        {
            IList<Product> result = _repo.GetProductsFromRepo(input);
            return Ok(result);
        }

        // Test <B>ug in application
        public async Task<IHttpActionResult> Post(Product input)
        {
            IList<Product> result = await _repo2.GetProductsFromLogicAsync(input).ConfigureAwait(false);
            return Ok(result);
        }

    }
}
