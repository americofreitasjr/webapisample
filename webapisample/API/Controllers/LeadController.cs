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
            double idade = CalcularIdade(req);

            // Se idade menor que 18 devolver mensagem
            if (idade < 18) {
                return Ok($"Voc� possui ({idade} anos) e essa idade � abaixo da permitida para se cadastrar.");
            }

            string diaSemana = TraduzirDiaSemana(req);

            string mensagem = DefinirMensagemResposta(req, diaSemana, idade);

            return Ok(mensagem);
        }

        private double CalcularIdade(Lead req) {
            return Math.Abs(Math.Round((req.DataNascimento - DateTime.Now).TotalDays / 365.25d, 0));

        }

        private string TraduzirDiaSemana(Lead req) {
            string diaSemana = "";

            switch (req.DataNascimento.DayOfWeek.ToString()) {
                case "Sunday":
                    diaSemana = "Domingo";
                    break;
                case "Monday":
                    diaSemana = "Segunda-Feira";
                    break;

                case "Tuesday":
                    diaSemana = "Ter�a-Feira";
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
                    diaSemana = "S�bado";
                    break;
            }

            return diaSemana;
        }


        private string DefinirMensagemResposta(Lead req, string diaSemana, double idade) {
            //Se for M � Sr, se for F Sra
            if (req.Genero == "M") {
                string mensagem = $"Ol� Sr {req.Nome} {req.Sobrenome} ({idade} anos) {diaSemana} Obrigado por se cadastrar. Voc� receber� uma confirma��o pelo e-mail {req.Email}";
                return mensagem;

            } else if (req.Genero == "F") {
                string mensagem = $"Ol� Sra {req.Nome} {req.Sobrenome} ({idade} anos) {diaSemana} Obrigado por se cadastrar. Voc� receber� uma confirma��o pelo e-mail {req.Email}";
                return mensagem;
            }
            return "";
        }

        private double CalcularIMC(double peso, double altura) {
            return ((peso / (altura * altura)));
        }
        //- string mensagem desnecess�rio. return �OBESO�; - FEITO.
        //- if(){} tem chaves - FEITO.
        //- voc� est� errando em calcular o IMC duas vezes - FEITO.
        //- o par�metro de ClassificarIMC � o imc(double) - FEITO.

        private string ClassificarIMC(double peso, double altura) {
            //Se imc < 18.5 retorna �MAGREZA�
            //Sen�o Se imc >= 18.5 e imc <=24.9 retorna �NORMAL�
            //Sen�o Se imc >= 25.0 e imc <= 29.9 retorna �SOBREPESO�
            //Sen�o Se imc >= 30.0 e imc <= 39.9 retorna �OBESIDADE�
            //Sen�o Se imc > 40.0 retorna �OBESIDADE GRAVE�
            double IMC = (peso / (altura * altura));
            if (IMC < 18.5) {
                return "Magreza";
            } else if (IMC >= 18.5 &&  IMC <= 24.9) {
                return "Normal";
            } else if (IMC >= 25.0 && IMC <= 29.9) {
                return "Sobrepeso";
            } else if (IMC >= 30.0 && IMC <= 39.9) {
                return "Obesidade";
            } else if (IMC >= 40.0) {
                return "Obesidade Grave";
            }
            return "";

        }
        private string DefinirMensagemRespostaIMC (Lead req, double IMC) {
            if ( IMC >= 18.5 && IMC <= 24.9) {
                string mensagem = "Sua classifica��o de IMC � Magreza e seu grau de obesidade � 0";
                return mensagem;
            } else if (IMC >= 25.0 && IMC <= 29.9) {
                string mensagem = "Sua classifica��o de IMC � Normal e seu grau de obesidade � 0";
                return mensagem;
            } else if (IMC >= 25.0 && IMC <= 29.9) {
                string mensagem = "Sua classifica��o de IMC � Sobrepeso e seu grau de obesidade � 1";
                return mensagem;
            } else if (IMC >= 30.0 && IMC <= 39.9) {
                string mensagem = "Sua classifica��o de IMC � Obesidade e seu grau de obesidade � 2";
                return mensagem;
            } else if (IMC >= 40.0) {
                string mensagem = "Sua classifica��o de IMC � Obesidade Grave e seu grau de obesidade � 3";
                return mensagem;
            }
            return "";
        }

    }


}
