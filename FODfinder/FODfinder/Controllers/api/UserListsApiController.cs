using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public IHttpActionResult getLists()
        {
            using (var db = new FFDBContext())
            {
                string userID = User.Identity.GetUserId();
                if (userID != null)
                {
                    List<UserList> userLists = db.UserLists.Where(x => x.userID == userID).Include(u => u.SavedFoods).ToList();
                    if (!userLists.Any())
                    {
                        var jsonNoLists = new UserListJsonResponse
                        {
                            success = false,
                            redirect = false,
                        };
                        return Ok(jsonNoLists);
                    }

                    var jsonLoggedIn = new UserListJsonResponse
                    {
                        success = true,
                        redirect = false,
                        lists = userLists
                    };

                    return Ok(jsonLoggedIn);
                }

                var jsonNotLoggedIn = new UserListJsonResponse
                {
                    success = false,
                    redirect = true
                };

                return Ok(jsonNotLoggedIn);
            }
        }
    }
}
