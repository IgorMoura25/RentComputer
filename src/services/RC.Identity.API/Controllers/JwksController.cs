using Microsoft.AspNetCore.Mvc;
using RC.Identity.API.Data.Repositories;
using RC.Identity.API.Models;
using RC.WebAPI.Core;

namespace RC.Identity.API.Controllers
{
    [ApiController]
    public class JwksController : MainController
    {
        private readonly ISecurityKeyRepository _securityKeyRepository;

        public JwksController(ISecurityKeyRepository securityKeyRepository)
        {
            _securityKeyRepository = securityKeyRepository;
        }

        [HttpGet]
        [Route("jwks")]
        public JsonWebKeySetViewModel ListKeys()
        {
            var keys = _securityKeyRepository.GetRecentKeys();

            return new JsonWebKeySetViewModel()
            {
                Keys = keys
            };
        }
    }
}
