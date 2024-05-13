using EM.Domain.Enum;
using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations;
using EM.Domain.Utilidades;


namespace EM.Domain
{
    public class Aluno : IEntidade
    {

        public int Matricula { get; set; }// o uso de interrogaçao permite que o a variavel matricula seja nula,

        [Required(ErrorMessage = "preencha este campo")]
        [StringLength(100, ErrorMessage = "Nome Deve ter no máximo 100 caracteres!")]
        [MinLength(3, ErrorMessage = "Nome Deve ter no mínimo 3 caracteres!")]
        public string Nome { get; set; } = string.Empty;
        [CPFValidation]
        public string? CPF { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime Nascimento { get; set; }
        [Required(ErrorMessage = "O campo sexo é obrigatório.")]
        public Sexo Sexo { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public Cidade Cidade { get; set; } = new Cidade();




		public override bool Equals(object? obj)// refatorado, apliquei a interrogaçao,e o erro cs 8765 saiu 
		{
			if (obj is Aluno outro)
			{
				return Matricula == outro.Matricula &&
					   Nome == outro.Nome &&
					   CPF == outro.CPF &&
					   Nascimento == outro.Nascimento &&
					   Sexo == outro.Sexo;
			}

			return false;
		}


		public override int GetHashCode()
        {
            return HashCode.Combine(Matricula, Nome, CPF, Nascimento, Sexo);
        }
    }
}
