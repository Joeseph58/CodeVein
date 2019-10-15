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
    public class WeaponController : Controller
    {
        [Authorize]
        
            // GET: Note
            public ActionResult Index()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new WeaponService(userId);
                var model = service.GetWeapons();

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
            public ActionResult Create(WeaponsCreate model)
            {
                if (!ModelState.IsValid) return View(model);

                var service = CreateWeaponService();

                if (service.CreateWeapon(model))
                {
                    TempData["SaveResult"] = "Your Weapon was created.";

                    return RedirectToAction("Index");
                };

                ModelState.AddModelError("", "Weapon could not be created.");

                return View(model);
            }

            public ActionResult Details(int id)
            {
                var svc = CreateWeaponService();
                var model = svc.GetWeaponById(id);

                return View(model);
            }


            public ActionResult Edit(int id)
            {
                var service = CreateWeaponService();
                var detail = service.GetWeaponById(id);
                var model =
                    new WeaponsEdit
                    {
                        WeaponId = detail.WeaponId,
                        WeaponType = detail.WeaponType,
                        WeaponStats = detail.WeaponStats
                    };
                return View(model);
            }

        [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, WeaponsEdit model)
            {
                if (!ModelState.IsValid) return View(model);

                if (model.WeaponId != id)
                {
                    ModelState.AddModelError("", "Id Mismatch");
                    return View(model);
                }

                var service = CreateWeaponService();

                if (service.UpdateWeapon(model))
                {
                    TempData["SaveResult"] = "Your Weapon was updated.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Your Weapon could not be updated.");
                return View(model);
            }

            [ActionName("Delete")]
            public ActionResult Delete(int id)
            {
                var svc = CreateWeaponService();
                var model = svc.GetWeaponById(id);

                return View(model);
            }

            [HttpPost]
            [ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeletePost(int id)
            {
                var service = CreateWeaponService();

                service.DeleteWeapon(id);

                TempData["SaveResult"] = "Your weapon was deleted";

                return RedirectToAction("Index");
            }

            private WeaponService CreateWeaponService()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new WeaponService(userId);
                return service;
            }


       
    }
}