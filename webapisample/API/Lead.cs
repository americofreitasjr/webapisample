using System;

namespace API
{
    public class Lead
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Data { get; set; }
        public string DataNascimento { get; set; }
        public bool Captado { get; set; }

    }
}