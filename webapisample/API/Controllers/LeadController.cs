using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private static readonly Lead[] CapDeLeads = new Lead[]
        {
            new Lead{ Nome = "jr",Sobrenome="Silva", Email="campostay@gmail.com", DataNascimento="02/05/1991"},

        };

        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger) 
        {
            _logger = logger;
        }

        [HttpGet(Name = "LeadController")]
        public IEnumerable<Lead> Get() 
        {
            return (IEnumerable<Lead>)CapDeLeads.ToList();

        }
        [HttpPost]
        public ActionResult<Lead> Post(Lead req) 
        {
          var captado = CapDeLeads.Any(w => w.Nome == req.Nome && w.Sobrenome == req.Sobrenome 
          && w.Email == req.Email && w.DataNascimento == req.DataNascimento);

            var response = new Lead() {
                Nome = req.Nome,
                Captado = captado,
               
            };

            return Ok(response);
        }


    }
     

        }
   