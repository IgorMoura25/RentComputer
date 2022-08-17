using Microsoft.AspNetCore.Mvc;
using RC.Identity.API.Data.Repositories;
using RC.Identity.API.Models;
using RC.WebAPI.Core;

namespace RC.Identity.API.Controllers
{
    [ApiController]
    public class JwksController : MainController
    {
        private readonly IRepository _repository;

        public JwksController(IRepository securityKeyRepository)
        {
            _repository = securityKeyRepository;
        }

        [HttpGet]
        [Route("jwks")]
        public JsonWebKeySetViewModel ListKeys()
        {
            var keys = _repository.GetRecentKeys();

            return new JsonWebKeySetViewModel()
            {
                Keys = keys
            };
        }
    }
}
