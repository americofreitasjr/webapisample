using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ImpostoRendaController : ControllerBase {
        private static readonly DeclaracaoIR[] DeclaracaoIR = new DeclaracaoIR[]
        {
            new DeclaracaoIR{ Nome = "jr",CPF="10020030040", NumeroDependentes="10", RendaIsenta= "5000.00", Patrimonio = "5000.00"},

        };

        private readonly ILogger<ImpostoRendaController> _logger;

        public ImpostoRendaController(ILogger<ImpostoRendaController> logger) {
            _logger = logger;
        }

        [HttpGet(Name = "ImpostoDeRendaController")]
        public IEnumerable<DeclaracaoIR> Get() {
            return (IEnumerable<DeclaracaoIR>)DeclaracaoIR.ToList();
        }
        [HttpPost]
        //A resposta do Post � o valor de imposto a ser recolhido.
        //Pode ser uma mensagem com o valor parecido com o que voc� j� fez at� aqui
        // Valor do imposto

        //Base de c�lculo � a renda Tribut�vel
        //Na primeira vers�o, voc� precisa retornar o valor do imposto com base nesta tabela
        public ActionResult<DeclaracaoIR> Post(DeclaracaoIR req) {
            var limiteDeducao = DefinirLimiteDeducao(req.RendaTributavel);
            var aliquota = ClassificarAliquota(req.RendaTributavel);
            var calculoImposto = CalcularImposto(req.RendaTributavel, aliquota);
            var deducaoPermitida = DefinirDeducaoPermitida(limiteDeducao);
            var impostoPagar = CalcularImpostoPagar(calculoImposto, deducaoPermitida);
            

            string mensagem = DefinirMensagemResposta(req, impostoPagar);

            return Ok(mensagem);
        }

        private string DefinirMensagemResposta(DeclaracaoIR req, double impostoPagar) {



            string mensagem = $"Ol� {req.Nome}, portador do CPF: {req.CPF}. Seu imposto de renda a ser pago � de {impostoPagar} reais.";

            return mensagem;
        }



        //1 - Fun��o que retorne a al�quota baseada no sal�rio
        //2 - Fun��o pra calcular o imposto pago que recebe al�quota e renda tributavel de parametro
        //Imposto apagar = RendaTributavel * (aliquota / 100)


        private double CalcularImposto(double rendaTributavel, double aliquota) {
            return (rendaTributavel * (aliquota / 100));
        }
        private double ClassificarAliquota(double rendaTributavel) {
            double aliquota = 00;

            if (rendaTributavel <= 2112.00) {
                aliquota = 00;
            } else if (rendaTributavel >= 2112.01 && rendaTributavel <= 2826.65) {
                aliquota = 7.50;
            } else if (rendaTributavel >= 2826.66 && rendaTributavel <= 3751.05) {
                aliquota = 15.00;
            } else if (rendaTributavel >= 3751.06 && rendaTributavel <= 4664.68) {
                aliquota = 22.50;
            } else if (rendaTributavel >= 4664.69) {
                aliquota = 27.50;
            }

            return aliquota;
        }

        private double DefinirLimiteDeducao(double rendaTributavel) {
            double deducao = 00;

            if (rendaTributavel >= 2112.01 && rendaTributavel <= 2826.65) {
                deducao = 158.40;
            } else if (rendaTributavel >= 2826.66 && rendaTributavel <= 3751.05) {
                deducao = 370.40;
            } else if (rendaTributavel >= 3751.06 && rendaTributavel <= 4664.68) {
                deducao = 651.73;
            } else if (rendaTributavel >= 4664.69) {
                deducao = 884.96;
            }
            return deducao;

        }

        //Dedu��o permitida = dedu��o, desde que a dedu��o seja menor que o limiteDeducao, sen�o dedu��o permitida = limite dedu��o
        //O imposto a pagar passar� a ser, impostocalculado-deducaopermitida

        private double DefinirDeducaoPermitida(double deducao) {
            double limiteDeducao = 00;
            double deducaoPermitida = 00;
            if (deducao < limiteDeducao) {
                deducaoPermitida = deducao;
            } else { deducaoPermitida =  limiteDeducao; }

            return limiteDeducao;
        }

        private double CalcularImpostoPagar(double deducaoPermitida, double calculoImposto) {
            return (calculoImposto - deducaoPermitida);
        }


    }
}





