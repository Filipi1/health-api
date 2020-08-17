using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace health_api.Model
{
    public class Auth
    {
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string CPF { get; set; }
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Password { get; set; }
    }
}
