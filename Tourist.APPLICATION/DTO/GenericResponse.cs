using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO
{
    public class GenericResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T?  Response { get; set; }
    }
}
