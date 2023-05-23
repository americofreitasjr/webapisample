using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;

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
            double idade = CalcularIdade(req.DataNascimento);

            // Se idade menor que 18 devolver mensagem
            if (idade < 18) {
                return Ok($"Você possui ({idade} anos) e essa idade é abaixo da permitida para se cadastrar.");
            }

            string diaSemana = TraduzirDiaSemana(req.DataNascimento.DayOfWeek.ToString());

            //Imc = cálculo base altura e peso;
            var imc = CalcularIMC(req.Peso, req.Altura);
            //Classificacaoimc = ClassificarIMC base imc;
            var classificacaoIMC = ClassificarIMC(imc);
            //Mensagem = mensagematual com classificacaoimc;
            var classificacaoRenda = ClassificarRendaFamiliar(req.RendaFamiliar);
            string mensagem = DefinirMensagemResposta(req, diaSemana, idade, classificacaoIMC, classificacaoRenda);

            return Ok(mensagem);
        }

        private double CalcularIdade(DateTime dataNascimento) {
            return Math.Abs(Math.Round((dataNascimento - DateTime.Now).TotalDays / 365.25d, 0));

        }

        private string TraduzirDiaSemana(string dayOfWeek) {
            string diaSemana = "";

            switch (dayOfWeek) {
                case "Sunday":
                    diaSemana = "Domingo";
                    break;
                case "Monday":
                    diaSemana = "Segunda-Feira";
                    break;

                case "Tuesday":
                    diaSemana = "Terça-Feira";
                    break;

                case "Wednesday":
                    diaSemana = "Quarta-Feira";
                    break;

                case "Thursday":
                    diaSemana = "Quinta-Feira";
                    break;

                case "Friday":
                    diaSemana = "Sexta-Feira";
                    break;

                case "Saturday":
                    diaSemana = "Sábado";
                    break;
            }

            return diaSemana;
        }


        private string DefinirMensagemResposta(Lead req, string diaSemana, double idade, string classificacaoIMC, string classificacaoRenda) {
            //Se for M é Sr, se for F Sra
            string tratamento;

            switch (req.Genero) {
                case "M":
                    tratamento = "Sr";
                    break;
                case "F":
                    tratamento = "Sra";
                    break;
                default:
                    tratamento = "";
                    break;
            }

            string mensagem = $"Olá {tratamento} {req.Nome} {req.Sobrenome} ({idade} anos) {diaSemana} Obrigado por se cadastrar. " +
         $"Você receberá uma confirmação pelo e-mail {req.Email}. Classificação IMC: {classificacaoIMC}. sua renda mensal é {classificacaoRenda}";

            return mensagem;
        }

        private double CalcularIMC(double peso, double altura) {
            return ((peso / (altura * altura)));
        }
        //- string mensagem desnecessário. return “OBESO”; - FEITO.
        //- if(){} tem chaves - FEITO.
        //- você está errando em calcular o IMC duas vezes - FEITO.
        //- o parâmetro de ClassificarIMC é o imc(double) - FEITO.

        private string ClassificarIMC(double imc) {
            //Se imc < 18.5 retorna “MAGREZA”
            //Senão Se imc >= 18.5 e imc <=24.9 retorna “NORMAL”
            //Senão Se imc >= 25.0 e imc <= 29.9 retorna “SOBREPESO”
            //Senão Se imc >= 30.0 e imc <= 39.9 retorna “OBESIDADE”
            //Senão Se imc > 40.0 retorna “OBESIDADE GRAVE”
            if (imc < 18.5) {
                return "Magreza";
            } else if (imc >= 18.5 && imc <= 24.9) {
                return "Normal";
            } else if (imc >= 25.0 && imc <= 29.9) {
                return "Sobrepeso";
            } else if (imc >= 30.0 && imc <= 39.9) {
                return "Obesidade";
            } else if (imc >= 40.0) {
                return "Obesidade Grave";
            }
            return "";

        }
        //classificar a renda e retorna na mensagem
        //    De R$ 0 até R$ 999,00
        //    Entre R$ 1000,00 e R$ 2499,00
        //    Entre R$ 2500,00 e R$ 5999,00
        //    Maior que R$ 6000,00

        private string ClassificarRendaFamiliar(double rendaFamiliar) {
            if (rendaFamiliar <= 999.00) {
                return "de R$ 0 até R$ 999,00";
            } else if (rendaFamiliar >= 1000.00 && rendaFamiliar <= 2499.00) {
                return "entre R$ 1000,00 e R$ 2499,00";
            } else if (rendaFamiliar >= 2500.00 && rendaFamiliar <= 5999.00) {
                return "entre R$ 2500,00 e R$ 5999,00";
            } else if (rendaFamiliar >= 6000.00) {
                return "maior que R$ 6000,00";
            }
            return "";
        }

    }


}
