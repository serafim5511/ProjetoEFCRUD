using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Helpers
{
    public class Pagination
    {
        [DefaultValue(1)]
        public int Page { get; set; }
        [DefaultValue(20)]
        public int PageSize { get; set; }
        public void ValidatePage()
        {
            if(Page < 0)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Página no formato inválido");
            }

            if(Page == 0)
            {
                Page = 1;
            }
            Page = Page-1;
        }
    }
}
