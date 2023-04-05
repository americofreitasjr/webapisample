namespace API
{
    public class CEPLogradouro
    {
        //https://viacep.com.br/ws/22710255/json/
        /*
          
         {
            "cep": "22710-255", FEITO.
            "logradouro": "Rua Mapendi", FEITO.
            "complemento": "", FEITO.
            "bairro": "Taquara",FEITO.
            "localidade": "Rio de Janeiro",FEITO.
            "uf": "RJ", FEITO.
            "ibge": "3304557", FEITO.
            "gia": "", FEITO.
            "ddd": "21", FEITO 
            "siafi": "6001" FEITO.
        }
          
         */

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string IBGE { get; set; } 
        public string Complemento { get; set; } 
        public string GIA { get; set; } 
        public string SIAFI { get; set; }
    }
}