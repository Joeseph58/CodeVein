using CodeVein.Data;
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
    public class BuildController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BuildService(userId);
            var model = service.GetBuild();

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
        public ActionResult Create(BuildCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBuildService();

            if (service.CreateBuild(model))
            {
                TempData["SaveResult"] = "Your Build was created.";

                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Build could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBuildService();
            var model = svc.GetBuildById(id);

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var service = CreateBuildService();
            var detail = service.GetBuildById(id);
            var model =
                new BuildEdit
                {
                    BuildId = detail.BuildId,
                    BuildName = detail.BuildName,
                    BuildStyle = detail.BuildStyle
                };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BuildEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BuildId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBuildService();

            if (service.UpdateBuild(model))
            {
                TempData["SaveResult"] = "Your Build was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Build could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBuildService();
            var model = svc.GetBuildById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBuildService();

            service.DeleteBuild(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private BuildService CreateBuildService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BuildService(userId);
            return service;
        }


        public ActionResult GetBuildStyle(string id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BuildService(userId);
            var model = service.GetBuildStyle( (BuildStyle)Enum.Parse(typeof(BuildStyle), id));

            return View(model);

        }
    }
}