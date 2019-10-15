using CodeVein.Models;
using CodeVein.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeVein_Web_MVC.Controllers
{
    public class BloodCodeController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BloodCodeService(userId);
            var model = service.GetBloodCode();

            return View(model);
        }

        //Add method here VVVV
        //GET
        public ActionResult Create()
        {
            return View();
        }

        //Add code here vvvv
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodCodeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBloodCodeService();

            if (service.CreateBloodCode(model))
            {
                TempData["SaveResult"] = "Your Blood Code was created.";

                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Blood Code could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBloodCodeService();
            var model = svc.GetBloodCodeById(id);

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var service = CreateBloodCodeService();
            var detail = service.GetBloodCodeById(id);
            var model =
                new BloodCodeEdit
                {
                    BloodCodeId = detail.BloodCodeId,
                    BcName = detail.BcName,
                    BuildStyle = detail.BuildStyle
                };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BloodCodeEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BloodCodeId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBloodCodeService();

            if (service.UpdateBloodCode(model))
            {
                TempData["SaveResult"] = "Your Blood Code was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Blood Code could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBloodCodeService();
            var model = svc.GetBloodCodeById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBloodCodeService();

            service.DeleteBloodCode(id);

            TempData["SaveResult"] = "Your Blood Code was deleted";

            return RedirectToAction("Index");
        }

        private BloodCodeService CreateBloodCodeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BloodCodeService(userId);
            return service;
        }

    }
}