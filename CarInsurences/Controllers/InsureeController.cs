using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurences.Models;

namespace CarInsurences.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMaker,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            
            insuree.Quote = 50;

            
            if (ModelState.IsValid)

            {

                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - insuree.DateOfBirth.Year;

                if (insuree.DateOfBirth > currentDate.AddYears(-age))
                    age--;

                if (age <= 18)
                {
                    insuree.Quote += 100;
                }
                else if (age >=19 && age <= 25)
                {
                    insuree.Quote += 50;

                }
                
                else if (age >= 26)
                {
                    insuree.Quote += 25;
                }
                else if (insuree.CarYear < 2000)
                {
                    insuree.Quote += 25;
                }
                else if (insuree.CarYear > 2015)
                {
                    insuree.Quote += 25;
                }
                else if (insuree.CarMaker == "Porsche")
                {
                    insuree.Quote += 25;
                    if (insuree.CarMaker == "Porsche" && insuree.CarModel == "911 Carrera")
                    {
                        insuree.Quote += 25;
                    }
                }
                else if (insuree .SpeedingTickets >= 1)
                {
                    insuree.Quote += (insuree.SpeedingTickets * 10);
                }
                else if (insuree.DUI !=false )
                {
                    
                     insuree.Quote += (insuree.Quote * 25) / 100;
                }
                else if (insuree .CoverageType !=false)
                {
                    insuree.Quote += (insuree.Quote * 50) / 100;
                }
                
               









               

            db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMaker,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {

                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - insuree.DateOfBirth.Year;

                if (insuree.DateOfBirth > currentDate.AddYears(-age))
                    age--;

                if (age <= 18)
                {
                    insuree.Quote += 100;
                }
                else if (age >= 19 && age <= 25)
                {
                    insuree.Quote += 50;

                }

                else if (age >= 26)
                {
                    insuree.Quote += 25;
                }
                else if (insuree.CarYear < 2000)
                {
                    insuree.Quote += 25;
                }
                else if (insuree.CarYear > 2015)
                {
                    insuree.Quote += 25;
                }
                else if (insuree.CarMaker == "Porsche")
                {
                    insuree.Quote += 25;
                    if (insuree.CarMaker == "Porsche" && insuree.CarModel == "911 Carrera")
                    {
                        insuree.Quote += 25;
                    }
                }
                else if (insuree.SpeedingTickets >= 1)
                {
                    insuree.Quote += (insuree.SpeedingTickets * 10);
                }
                else if (insuree.DUI != false)
                {

                    insuree.Quote += (insuree.Quote * 25) / 100;
                }
                else if (insuree.CoverageType != false)
                {
                    insuree.Quote += (insuree.Quote * 50) / 100;
                }






                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
