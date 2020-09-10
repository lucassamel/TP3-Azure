using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP3_Azure.Models
{
    public class Amigos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Aniversario { get; set; }
        public string Telefone { get; set; }
    }
}
