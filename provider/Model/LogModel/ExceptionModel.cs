using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace provider.Model.Log
{
    public class ExceptionModel
    {
        public string Location { get; set;  }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
    }
}
