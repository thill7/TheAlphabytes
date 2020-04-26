using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FODfinder.Models.Api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/userlists")]
    public class UserListsApiController : ApiController
    {
        [HttpGet]
        [Route("get")]
        public UserListJsonResponse getLists()
        {
            using (var db = new FFDBContext())
            {
                string userID = User.Identity.GetUserId();
                if (userID != null)
                {
                    List<UserList> userLists = db.UserLists.Where(x => x.userID == userID).ToList();
                    if (!userLists.Any())
                    {
                        var jsonNoLists = new UserListJsonResponse
                        {
                            success = false,
                            redirect = false,
                        };
                        return jsonNoLists;
                    }

                    var jsonLoggedIn = new UserListJsonResponse
                    {
                        success = true,
                        redirect = false,
                        lists = userLists
                    };

                    return jsonLoggedIn;
                }

                var jsonNotLoggedIn = new UserListJsonResponse
                {
                    success = false,
                    redirect = true
                };

                return jsonNotLoggedIn;
            }
        }
    }
}
