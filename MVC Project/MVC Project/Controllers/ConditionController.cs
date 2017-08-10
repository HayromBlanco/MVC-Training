using MVC_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class ConditionController : Controller
    {
        // GET: PlantType
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllConditions()
        {
            return Json(ParcelData.ConditionList, JsonRequestBehavior.AllowGet);
        }
    }

    
}     


















//// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}