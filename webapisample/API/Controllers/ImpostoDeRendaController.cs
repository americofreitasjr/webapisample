using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        //A resposta do Post é o valor de imposto a ser recolhido.
        //Pode ser uma mensagem com o valor parecido com o que você já fez até aqui
        // Valor do imposto(Imagem)

        //Base de cálculo é a renda Tributável
        //Na primeira versão, você precisa retornar o valor do imposto com base nesta tabela
        public ActionResult<DeclaracaoIR> Post(DeclaracaoIR req) {

            var aliquota = ClassificarAliquota(req.RendaTributavel);
            var ImpostoRenda = CalcularImposto(req.RendaTributavel, aliquota);
           
            string mensagem = DefinirMensagemResposta(req, ImpostoRenda);

            return Ok(mensagem);
        }

        private string DefinirMensagemResposta(DeclaracaoIR req, double ImpostoRenda) {
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

            string mensagem = $"Olá {tratamento} {req.Nome}, portador do CPF: {req.CPF}. Seu imposto de renda a ser pago é de {ImpostoRenda} reais." ;

            return mensagem;
        }
        //1 - Função que retorne a alíquota baseada no salário
        //2 - Função pra calcular o imposto pago que recebe alíquota e renda tributavel de parametro
        //Imposto apagar = RendaTributavel * (aliquota / 100)


        private double CalcularImposto(double rendaTributavel, double aliquota) {
            return (rendaTributavel * (aliquota/ 100));
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

    } 
}

  



