using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Models.Api
{
    public class UserListJsonResponse : JsonResponse
    {
        public List<UserList> lists { get; set; }
    }
}