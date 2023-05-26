using System;

namespace API
{
    public class DeclaracaoIR
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string NumeroDependentes { get; set; }
        public string DespesasSaude { get; set; } 
        public string DespesasEducacao { get; set; }
        public double RendaTributavel { get; set; } 
        public string RendaIsenta{ get; set; }
        public string Patrimonio { get; set; }
        public string Genero { get; set; }
        
    }
}