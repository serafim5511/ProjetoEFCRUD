using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null, bool sucesso = true)
        {
            Sucesso = sucesso;
            Mensagem = message;
            Data = data;
        }
        public T Data { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}
