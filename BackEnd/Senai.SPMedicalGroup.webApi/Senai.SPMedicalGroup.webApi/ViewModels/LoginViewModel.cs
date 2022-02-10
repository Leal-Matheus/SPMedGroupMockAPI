using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Email!")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "Informe a senha!")]
        public string SenhaUsuario { get; set; }
    }
}
