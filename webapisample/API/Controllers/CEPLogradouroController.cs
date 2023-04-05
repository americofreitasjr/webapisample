using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CEPLogradouroController : ControllerBase
    {
        private static readonly string[] CEPs = new[]
        {
            "22710255", "22790710", "21760350"
        };

        private readonly ILogger<CEPLogradouroController> _logger;

        public CEPLogradouroController(ILogger<CEPLogradouroController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "CEPLogradouro")] 
        public IEnumerable<CEPLogradouro> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new CEPLogradouro
            {
                CEP = CEPs[Random.Shared.Next(CEPs.Length)]
            })
            .ToArray();
        }
    }
}