using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp01.Models
{
    public class AlunoDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public string data { get; set; }
        public int ra { get; set; }
    }
}