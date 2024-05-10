using EM.Domain.Interface;

namespace EM.Domain
{
    public  class Cidade : IEntidade
    {
        public string? NomeCidade { get; set; }
        public string? UF { get; set; }
        public int? ID_Cidade { get; set; }
      
       
    }
}
