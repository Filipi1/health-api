using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace health_api.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo 'Nome' deve ser preenchido.")]
        [MinLength(3, ErrorMessage = "O Campo 'Nome' deve conter no mínimo 3 caracteres")]
        [MaxLength(30, ErrorMessage = "O Campo 'Nome' deve conter no máximo 30 caracteres")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        [Required(ErrorMessage = "O Campo 'Email' deve ser preenchido.")]
        [MinLength(4, ErrorMessage = "O Campo 'Email' deve conter no mínimo 3 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo 'Senha' deve ser preenchido.")]
        [MinLength(6, ErrorMessage = "O Campo 'Senha' deve conter no mínimo 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O Campo {0} deve ser preenchido.")]
        public string CPF { get; set; }

        public DateTime BornDate { get; set; }

        public string Cellphone { get; set; }

        public int Idade { get; set; }

        public string Sexo { get; set; }

        public List<Exam> Exams { get; set; }

        public int? addressId { get; set; }

        public Address address { get; set; }

        public bool Active { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
