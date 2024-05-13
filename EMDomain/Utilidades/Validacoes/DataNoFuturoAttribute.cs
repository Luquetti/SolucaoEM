using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Domain.Utilidades.Validacoes
{
    public  class DataNoFuturoAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue > DateTime.Now)
                {
                    return new ValidationResult("A data de nascimento não pode ser no futuro.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
