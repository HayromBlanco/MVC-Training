using MVC_Project.Data;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class PlantController : Controller
    {
        // GET: Plant
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getPlant(int? plantId)
        {
            try
            {
                if (plantId != null)
                {
                    var GetPlant = PlantData.PlantList.Where(Gp => Gp.Id == plantId).FirstOrDefault();
                    return Json(GetPlant, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetPlant

        private string GetTypeName(int plantId)
        {
            return PlantData.PlantTypeList.Where(p => p.Id == plantId).FirstOrDefault().Name;
        }//End GetTypeName

        [HttpGet]
        public JsonResult GetParcelByTypeAndCount(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    var a = from p in PlantData.PlantList
                            where p.IdParcel == parcelId
                            group p by p.PlantType
                        into g
                            select g.ToList();
                    List<Object> Result = new List<object>();
                    foreach (var b in a)
                    {
                        Result.Add(new
                        {
                            count = b.Count,
                            type = GetTypeName(b.FirstOrDefault().PlantType)
                        });
                    }

                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetParcelByTypeAndCount

        [HttpGet]
        public JsonResult GetAllPlantsByParcelId(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    var GetPlants = PlantData.PlantList.Where(P => P.IdParcel == parcelId);
                    return Json(GetPlants, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetAllPlantsByParcelId

        [HttpDelete]
        public JsonResult RemovePlant(int? plantId)
        {
            try
            {
                if (plantId != null)
                {
                    var plant = PlantData.PlantList.Where(g => g.Id == plantId).FirstOrDefault();
                    PlantData.PlantList.Remove(plant);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End RemovePlant

        [HttpPost]
        public JsonResult AddPlant([Bind(Include = "Name,Description,PlantedDate,IdParcel,PlantType")] Plant ObjPlant)
        {
            try
            {
                if (!string.IsNullOrEmpty(ObjPlant.Name) && !string.IsNullOrEmpty(ObjPlant.Description))
                {
                    var LastPlant = PlantData.PlantList.LastOrDefault();

                    if (LastPlant == null)
                    {
                        ObjPlant.Id = 1;
                        PlantData.PlantList.Add(ObjPlant);
                        return Json(ObjPlant, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ObjPlant.Id = LastPlant.Id + 1;
                        PlantData.PlantList.Add(ObjPlant);
                        return Json(ObjPlant, JsonRequestBehavior.AllowGet);
                    }


                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End AddPlant

        [HttpPut]
        public JsonResult UpdatePlant([Bind(Include = "Id,Name,Description,PlantedDate,IdParcel,PlantType")] Plant ObjPlant)
        {
            try
            {
                if (ObjPlant.Id != null)//Can 0 be used in the condition?
                {
                    var GotPlant = PlantData.PlantList.Where(Gp => Gp.Id == ObjPlant.Id).FirstOrDefault();
                    GotPlant.Name = ObjPlant.Name;
                    GotPlant.Description = ObjPlant.Description;
                    GotPlant.PlantedDate = ObjPlant.PlantedDate;
                    GotPlant.IdParcel = ObjPlant.IdParcel;
                    GotPlant.PlantType = ObjPlant.PlantType;
                    return Json(GotPlant, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End UpdatePlant

    }//End PlantController
}