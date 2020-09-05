using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace health_api.Model
{
    public class Nomes
    {
        [Key]
        public int id {get; set;}
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string nome {get; set;}
        public string desc { get; set; }
        public string url { get; set; }
        public bool isVideo { get; set; }
    }
}
