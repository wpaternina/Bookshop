using System;
using System.Net;

namespace Api.ErrorHandling
{
    public class Error : Exception
    {
        public HttpStatusCode Code;
        public object Errores;

        public Error(HttpStatusCode code, object errores = null) 
        {
            Code = code;
            Errores = errores;
        }
    }
}
