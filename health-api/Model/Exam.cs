using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace health_api.Model
{
    public class Exam
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]

        public string Description { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public Guid ColaboratorId { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
