using System;
using System.Collections.Generic;
using System.Text;

namespace TestIntegration
{
    public class ApiRoutes
    {
        private static readonly string _baseUrl = "http://localhost:7186/";

        public static class ProductsServices
        {
            public static readonly string GetUsuarioAsync = string.Concat(_baseUrl, "");
        }
    }
}
