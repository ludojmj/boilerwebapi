using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BoilerWebApi.Models;
using BoilerWebApi.Repository;

namespace BoilerWebApi.Controllers
{
    /// <summary>
    /// Test bugg in app if input.Id == "1"
    /// Normal operation if input.Id != "1"
    /// </summary>
    public class OtherProductController : ApiController
    {
        private readonly IOtherProductRepo _repo;

        public OtherProductController()
        {
            _repo = new OtherProductRepo();
        }

        public OtherProductController(IOtherProductRepo repo)
        {
            _repo = repo;
        }

        public IHttpActionResult Post(Product input)
        {
            IList<Product> result = _repo.GetOtherProductsFromRepo(input);
            return Ok(result);
        }

        [Route("api/otherproduct/async")]
        public async Task<IHttpActionResult> PostAsync(Product input)
        {
            IList<Product> result = await _repo.GetOtherProductsFromRepoAsync(input).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
