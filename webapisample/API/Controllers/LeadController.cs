using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private static readonly LeadRequest[] CapDeLeads = new LeadRequest[]
        {
            new LeadRequest{ Nome = "jr",Sobrenome="Silva", Email="campostay@gmail.com", DataNascimento="02/05/1991"},

        };

        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger) 
        {
            _logger = logger;
        }

        [HttpGet(Name = "LeadController")]
        public IEnumerable<Lead> Get() 
        {
            return Enumerable.Range(1, 5).Select(index => new Lead {
                CapDeLead = CapDeLeads[Random.Shared.Next(CapDeLeads.Length)]
            })
            .ToArray();
        }



    }
     

        }
   