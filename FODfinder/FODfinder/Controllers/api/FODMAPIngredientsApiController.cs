using FODfinder.Models;
using FODfinder.Models.Api;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FODfinder.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/fodmapingredients")]
    public class FODMAPIngredientsApiController : ApiController
    {
        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] LabelIngredientRequest req)
        {
            var ingredientName = req.IngredientName;
            var assignLabel = req.AssignLabel;
            using (FFDBContext db = new FFDBContext())
            {
                string userID = User.Identity.GetUserId();
                if (userID != null)
                {
                    int ingredientID = db.LabelledIngredients.Where(s => s.Name == ingredientName).Select(s => s.ID).FirstOrDefault();
                    if (ingredientID == 0)
                    {
                        LabelledIngredient labelIngredient = new LabelledIngredient { Name=ingredientName };
                        try
                        {
                            db.LabelledIngredients.Add(labelIngredient);
                            db.SaveChanges();
                            ingredientID = labelIngredient.ID;
                        }
                        catch
                        {
                            var jsonData_failed_ingredient_add = new JsonResponse
                            {
                                success = false,
                                message = "Ingredient not added to database",
                                redirect = false
                            };

                            
                            return Ok(jsonData_failed_ingredient_add);
                        }
                    }
                    if (db.UserIngredients.Where(s => s.LabelledIngredientID == ingredientID && s.userID == userID).Count() > 0)
                    {
                        var existingRecord = db.UserIngredients.FirstOrDefault(s => s.LabelledIngredientID == ingredientID && s.userID == userID);
                        existingRecord.Label = assignLabel;
                        db.SaveChanges();
                        var jsonData_edit_record = new JsonResponse
                        {
                            success = true,
                            message = "Label has been changed in the record",
                            redirect = false
                        };

                        return Ok(jsonData_edit_record);
                    }
                    else
                    {
                        UserIngredient userIng = new UserIngredient(userID, assignLabel, ingredientID);
                        try
                        {
                            db.UserIngredients.Add(userIng);
                            db.SaveChanges();
                            var jsonData_success = new JsonResponse
                            {
                                success = true,
                                message = "Label has been saved.",
                                redirect = false
                            };

                            return Ok(jsonData_success);
                        }
                        catch(Exception e)
                        {
                            var jsonData_fail = new JsonResponse
                            {
                                success = false,
                                message = $"Something went wrong: {e.InnerException.InnerException.Message}",
                                redirect = false
                            };

                            return Ok(jsonData_fail);
                        }
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
