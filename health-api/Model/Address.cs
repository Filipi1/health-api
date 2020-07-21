using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace health_api.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public int CEP { get; set; }
        public string Complement { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UF { get; set; }
    }
}
