using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private static readonly Lead[] CapDeLeads = new Lead[]
        {
            new Lead{ Nome = "jr",Sobrenome="Silva", Email="campostay@gmail.com", DataNascimento=new DateTime(1991, 05, 02),},  
           
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
        public ActionResult<Lead> Post(Lead req) {
            //Se for M � Sr, se for F Sra
            
            if (req.Genero == "M") {
                string mensagem = $"Ol� Sr {req.Nome} {req.Sobrenome} Obrigado por se cadastrar. Voc� receber� uma confirma��o pelo e-mail {req.Email}";
                return Ok(mensagem);
            } else if (req.Genero == "F") {
                string mensagem = $"Ol� Sra {req.Nome} {req.Sobrenome} Obrigado por se cadastrar. Voc� receber� uma confirma��o pelo e-mail {req.Email}";
                return Ok(mensagem);
             }

             return Ok(); 

        }


    }
     

 }
   