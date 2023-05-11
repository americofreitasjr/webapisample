using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

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
            string DiaSemana = "";
                switch (req.DataNascimento.DayOfWeek.ToString()) { 
                case "Sunday":
                    DiaSemana = "Domingo";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;

                case "Monday":
                    DiaSemana = "Segunda-Feira";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;

                case "Tuesday":
                    DiaSemana = "Terça-Feira";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;

                case "Wednesday":
                    DiaSemana = "Quarta-Feira";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;

                case "Thursday":
                    DiaSemana = "Quinta-Feira";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;

                case "Friday":
                    DiaSemana = "Sexta-Feira";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;

                case "Saturday":
                    DiaSemana = "Sábado";
                    Console.WriteLine($"Measured value is {DiaSemana}");
                    break;


            }

            Console.WriteLine(idade);
            // Se idade menor que 18 devolver mensagem
            if (idade < 18 ) {
                string mensagem = $"Você possui ({idade} anos) e essa idade é abaixo da permitida para se cadastrar.";
                return Ok(mensagem);
            } 
            
            if (req.Genero == "M") {
                string mensagem = $"Olá Sr {req.Nome} {req.Sobrenome} ({idade} anos) {DiaSemana} Obrigado por se cadastrar. Você receberá uma confirmação pelo e-mail {req.Email}";
                return Ok(mensagem);

            } else if (req.Genero == "F") {
                string mensagem = $"Olá Sra {req.Nome} {req.Sobrenome} ({idade} anos) {DiaSemana} Obrigado por se cadastrar. Você receberá uma confirmação pelo e-mail {req.Email}";
                return Ok(mensagem);
            }
            return Ok();

            static double Calcularidade(Lead req) { 
                return Math.Abs(Math.Round((req.DataNascimento - DateTime.Now).TotalDays / 365.25d, 0));

            }
        }


    }


}
