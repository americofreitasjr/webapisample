using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class Lead
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DataTime Data { get; set; }


    public class LeadController : ControllerBase
    {
        private static readonly LeadRequest[] Leads = new LeadRequest[]
        {
            new LeadRequest{ Nome="jr",Sobrenome="Silva", Email="campostay@gmail.com", DataNascimento"02/05/1991"},
           
        };

        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
        }

        [HttpPost] 
        public ActionResult<LeadResponse> Post(LeadRequest req)
        {
            var logado = Leads.Any(w => w.Nome == req.Nome && w.Sobrenome == req.Sobrenome && w.Email == req.Email && w.DataNascimento == req.Datanascimento);

            var response = new LeadResponse() {
                Nome = req.,
                Logado = logado,
                Data = DateTime.Now,
            };

            return Ok(response);
        }
    }
}