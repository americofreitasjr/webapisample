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

            double idade = CalcularIdade(req);
            string diaSemana = "";
            diaSemana = TraduzirDiaSemana(req, diaSemana);

            Console.WriteLine(idade);
            // Se idade menor que 18 devolver mensagem
            if (idade < 18) {
                string mensagem = $"Você possui ({idade} anos) e essa idade é abaixo da permitida para se cadastrar.";
                return Ok(mensagem);
            }

            if (req.Genero == "M") {
                string mensagem = $"Olá Sr {req.Nome} {req.Sobrenome} ({idade} anos) {diaSemana} Obrigado por se cadastrar. Você receberá uma confirmação pelo e-mail {req.Email}";
                return Ok(mensagem);

            } else if (req.Genero == "F") {
                string mensagem = $"Olá Sra {req.Nome} {req.Sobrenome} ({idade} anos) {diaSemana} Obrigado por se cadastrar. Você receberá uma confirmação pelo e-mail {req.Email}";
                return Ok(mensagem);
            }
            return Ok();

            static double CalcularIdade(Lead req) {
                return Math.Abs(Math.Round((req.DataNascimento - DateTime.Now).TotalDays / 365.25d, 0));

            }

            static string TraduzirDiaSemana(Lead req, string diaSemana) {
                switch (req.DataNascimento.DayOfWeek.ToString()) {
                    case "Sunday":
                        diaSemana = "Domingo";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;

                    case "Monday":
                        diaSemana = "Segunda-Feira";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;

                    case "Tuesday":
                        diaSemana = "Terça-Feira";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;

                    case "Wednesday":
                        diaSemana = "Quarta-Feira";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;

                    case "Thursday":
                        diaSemana = "Quinta-Feira";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;

                    case "Friday":
                        diaSemana = "Sexta-Feira";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;

                    case "Saturday":
                        diaSemana = "Sábado";
                        Console.WriteLine($"Measured value is {diaSemana}");
                        break;


                }

                return diaSemana;
            }
        }


    }


}
