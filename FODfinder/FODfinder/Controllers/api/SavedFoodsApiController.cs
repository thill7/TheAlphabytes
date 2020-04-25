using FODfinder.Models;
using FODfinder.Models.Api;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FODfinder.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/savedfoods")]
    public class SavedFoodsApiController : ApiController
    {
        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] SavedFoodRequest request)
        {
            using (FFDBContext db = new FFDBContext())
            {
                if (User.Identity.GetUserId() != null)
                {
                    SavedFood savedFood = new SavedFood(request.UsdaFoodID, request.ListID, request.BrandOwner, request.Upc, request.Description);
                    try
                    {
                        db.SavedFoods.Add(savedFood);
                        db.SaveChanges();
                        var jsonData = new JsonResponse
                        {
                            success = true,
                            message = "Food has been saved.",
                            redirect = false
                        };

                        return Ok(jsonData);
                    }
                    catch
                    {
                        var jsonData2 = new JsonResponse
                        {
                            success = false,
                            message = "Food has already been saved.",
                            redirect = false
                        };

                        return Ok(jsonData2);
                    }
                }

                var jsonData3 = new JsonResponse
                {
                    success = false,
                    message = "User not logged in.",
                    redirect = true
                };

                return Ok(jsonData3);
            }
        }
    }
}
