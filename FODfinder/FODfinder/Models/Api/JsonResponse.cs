using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Api
{
    public class JsonResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public bool redirect { get; set; }
    }
}