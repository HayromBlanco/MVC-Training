using MVC_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class PlantTypeController : Controller
    {
        // GET: PlantType
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]

        public JsonResult GetAllPlantTypes()
        {
            try
            {
                return Json(PlantData.PlantTypeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }//End GetAllPlantTypes

        [HttpGet]
        public JsonResult GetTypePlantById(int? plantTypeId)
        {
            try
            {
                if (plantTypeId != null)
                {
                    var GetPlantType = PlantData.PlantTypeList.Where(P => P.Id == plantTypeId).FirstOrDefault();
                    return Json(GetPlantType, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetTypePlantById

    }
}