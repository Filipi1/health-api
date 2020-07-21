using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace health_api.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Titulo da categoria é obrigatório")]
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}
