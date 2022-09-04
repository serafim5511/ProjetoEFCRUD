using Domain.Enums;
using System;

namespace Domain.Models
{
    public class Usuario : Base 
    {
        public string Nome { get; set; }
        public DateTime DtNasc { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TipoUsuario? Tipo { get; set; }
    }
}
