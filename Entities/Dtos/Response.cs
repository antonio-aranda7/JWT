using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class Response
    {

        public string? Message { get; set; }
        public int Code { get; set; }

        public string? Token { get; set; }
    }
}
