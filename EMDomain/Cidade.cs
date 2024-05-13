using EM.Domain.Interface;

namespace EM.Domain
{
    public  class Cidade : IEntidade
    {
        public string NomeCidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public int ID_Cidade { get; set; }
      
       
    }
}
