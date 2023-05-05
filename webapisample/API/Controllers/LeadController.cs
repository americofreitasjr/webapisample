using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase {
        private static readonly Lead[] CapDeLeads = new Lead[]
        {
            new Lead{ Nome = "jr",Sobrenome="Silva", Email="campostay@gmail.com", DataNascimento=new DateTime(1991, 05, 02),},

        };

        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger) {
            _logger = logger;
        }

        [HttpGet(Name = "LeadController")]
        public IEnumerable<Lead> Get() {
            return (IEnumerable<Lead>)CapDeLeads.ToList();

        }

        [HttpPost]
        public ActionResult<Lead> Post(Lead req) {
            //Se for M é Sr, se for F Sra

            double idade = Calcularidade(req);

            Console.WriteLine(idade);

            if (req.Genero == "M") {
                string mensagem = $"Olá Sr {req.Nome} {req.Sobrenome} ({idade} anos) Obrigado por se cadastrar. Você receberá uma confirmação pelo e-mail {req.Email}";
                return Ok(mensagem);
            } else if (req.Genero == "F") {
                string mensagem = $"Olá Sra {req.Nome} {req.Sobrenome} ({idade} anos) Obrigado por se cadastrar. Você receberá uma confirmação pelo e-mail {req.Email}";
                return Ok(mensagem);

            }

            return Ok();

            static double Calcularidade(Lead req) { 
                return Math.Abs(Math.Round((req.DataNascimento - DateTime.Now).TotalDays / 365.25d, 0));
            }
        }


    }


}
