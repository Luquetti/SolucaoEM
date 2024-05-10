using EM.Domain.Enum;
using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations;
using EM.Domain.Utilidades;


namespace EM.Domain
{
    public class Aluno : IEntidade
    {

        public int? Matricula { get; set; }

        [Required(ErrorMessage = "preencha este campo")]
        [StringLength(100, ErrorMessage = "Nome Deve ter no máximo 100 caracteres!")]
        [MinLength(3, ErrorMessage = "Nome Deve ter no mínimo 3 caracteres!")]
        public string? Nome { get; set; }
        [CPFValidation]
        public string? CPF { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? Nascimento { get; set; }
        [Required(ErrorMessage = "O campo sexo é obrigatório.")]
        public Sexo? Sexo { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public Cidade? Cidade { get; set; }

            


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            if (obj == null || this.GetType() != obj.GetType()) return false;
            Aluno outro = (Aluno)obj;

            return Matricula == outro.Matricula &&
                Nome == outro.Nome &&
                CPF == outro.CPF &&
                Nascimento == outro.Nascimento &&
                Sexo == outro.Sexo;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Matricula, Nome, CPF, Nascimento, Sexo);
        }
    }
}
