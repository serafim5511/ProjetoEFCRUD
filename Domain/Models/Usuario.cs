using System;

namespace Domain.Models
{
    public class Usuario : Base 
    {
        public string Nome { get; set; }
        public DateTime DtNasc { get; set; }
    }
}
