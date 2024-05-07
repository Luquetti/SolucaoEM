using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Domain.Utilidades;
public class CPFValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        string cpf = value.ToString();
        // Use o método CPFValidado para validar o CPF
        if (!ValidaCPF.CPFValida(cpf))
        {
            return new ValidationResult("CPF inválido.");
        }
        return ValidationResult.Success;
    }
}
