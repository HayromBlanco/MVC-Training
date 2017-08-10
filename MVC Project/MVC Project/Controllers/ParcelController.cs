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
    public class ParcelController : Controller
    {
        // GET: Parcel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetParcel(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    var GetParcel = ParcelData.ParcelList.Where(P => P.Id == parcelId).FirstOrDefault();
                    return Json(GetParcel, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End Get Parcel


        [HttpGet]
        public JsonResult GetAllParcelByFarmId(int? farmId)
        {
            try
            {
                if (farmId != null)
                {
                    var GetParcel = ParcelData.ParcelList.Where(P => P.IdFarm == farmId);
                    return Json(GetParcel, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End  GetAllParcelByFarmId


        [HttpPost]
        public JsonResult AddParcel([Bind(Include = "Size,Description,IdFarm,Observations,ConditionIds")] Parcel ObjParcel)
        {
            try
            {
                if (!string.IsNullOrEmpty(ObjParcel.Description) && !string.IsNullOrEmpty(ObjParcel.Size.ToString()) && ObjParcel.Observations.Count > 0)
                {
                    
                    var LastId = ParcelData.ParcelList.Last();
                    ObjParcel.Id = LastId.Id + 1;
                    ParcelData.ParcelList.Add(ObjParcel);
                    return Json(ObjParcel, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End of AddParcel

        [HttpPut]
        public JsonResult UpdateParcel([Bind(Include = "Id,Size,Description,IdFarm,Observations,ConditionIds")] Parcel ObjParcel)
        {
            try
            {
                if (ObjParcel.Observations.Count > 0 && !string.IsNullOrEmpty(ObjParcel.Size.ToString()) && !string.IsNullOrEmpty(ObjParcel.Description))
                {
                    var UpdateParcel = ParcelData.ParcelList.Where(P => P.Id == ObjParcel.Id).FirstOrDefault();
                    UpdateParcel.Size = ObjParcel.Size;
                    UpdateParcel.Description = ObjParcel.Description;
                    UpdateParcel.Observations = ObjParcel.Observations;
                    UpdateParcel.ConditionIds = ObjParcel.ConditionIds;
                    return Json(UpdateParcel, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End UpdateParcel

        [HttpDelete]
        public JsonResult RemoveParcel(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    var DeleteParcel = ParcelData.ParcelList.Where(P => P.Id == parcelId).FirstOrDefault();
                    ParcelData.ParcelList.Remove(DeleteParcel);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }//End RemoveParcel



    }//End ParcelController
}