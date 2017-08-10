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
    public class FarmController : Controller
    {
        // GET: Farm
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllFarms()
        {
            try
            {
                return Json(FarmData.FarmList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetAllFarms

        [HttpGet]
        public JsonResult GetFarmsById(int? farmId)
        {
            try
            {
                if (farmId != null)
                {
                    var GotFarmById = FarmData.FarmList.Where(f => f.Id == farmId).FirstOrDefault();
                    return Json(GotFarmById, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetFarmsById




        public JsonResult AddFarm([Bind(Include = "Name,Description")] Farm ObjFarm)
        {
            try
            {
                if (!string.IsNullOrEmpty(ObjFarm.Name) && !string.IsNullOrEmpty(ObjFarm.Description))
                {

                    var LastFarm = FarmData.FarmList.LastOrDefault();
                    if (LastFarm == null)
                    {
                        ObjFarm.Id = 1;
                        FarmData.FarmList.Add(ObjFarm);
                        return Json(ObjFarm, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ObjFarm.Id = LastFarm.Id + 1;
                        FarmData.FarmList.Add(ObjFarm);
                        return Json(ObjFarm, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End Add Farm

        [HttpDelete]
        public JsonResult RemoveFarm(int? farmId)
        {
            try
            {
                if (farmId != null)
                {
                    var Farm = FarmData.FarmList.Where(f => f.Id == farmId).FirstOrDefault();

                    var AllParcels = ParcelData.ParcelList.Where(Ap => Ap.IdFarm == farmId).ToList();

                    foreach (var Parcels in AllParcels)
                    {
                        var ParcelId = Parcels.Id;

                        var PlantList = PlantData.PlantList.Where(P => P.IdParcel == ParcelId).ToList();

                        foreach (var Plants in PlantList)
                        {
                            PlantData.PlantList.Remove(Plants);
                        }

                        ParcelData.ParcelList.Remove(Parcels);
                    }

                    FarmData.FarmList.Remove(Farm);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End RemoveFarm

        [HttpGet]
        //Check the name of the method
        public JsonResult getFarm(int? farmId)
        {
            try
            {
                if (farmId != null)
                {
                    var Farm = FarmData.FarmList.Where(s => s.Id == farmId).FirstOrDefault();
                    return Json(Farm, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End GetFarm

        [HttpPut]
        public JsonResult UpdateFarm([Bind(Include = "Id,Name,Description")] Farm ObjFarm)
        {
            try
            {
                if (ObjFarm.Id != null)
                {
                    var EditFarm = FarmData.FarmList.Where(gf => gf.Id == ObjFarm.Id).FirstOrDefault();
                    EditFarm.Name = ObjFarm.Name;
                    EditFarm.Description = ObjFarm.Description;
                    return Json(EditFarm, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End UpdateFarm


    }//End Farm Controller

}//End NameSpace


