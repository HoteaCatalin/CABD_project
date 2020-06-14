using Newtonsoft.Json;
using project.CABD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace project.CABD.Controllers
{
    public class HomeController : Controller
    {
        cabdEntities db = new cabdEntities();
        static int globalId;

        public ActionResult Index()
        {
            var cars = db.Cars.ToList();
            return View(cars);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Car car)
        {
            car.DateCreated = DateTime.UtcNow;
            car.DateLastModified = DateTime.UtcNow;
            db.Cars.Add(car);
            db.SaveChanges();

            var caruta = db.Cars.FirstOrDefault(c => c.Marca == car.Marca && c.Model == car.Model && c.Pret == car.Pret);

            db.CarHistories.Add(new CarHistory
            {
                CarId = caruta.Id,
                Marca = car.Marca,
                Model = car.Model,
                An = car.An,
                Pret = car.Pret,
                DateCreated = car.DateCreated.Value,
                DateModified = car.DateLastModified.Value
            });

            db.SaveChanges();

            return View("Index", db.Cars.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(int id, Car carView)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            car.Marca = carView.Marca;
            car.Model = carView.Model;
            car.An = carView.An;
            car.Pret = carView.Pret;

            db.CarHistories.Add(new CarHistory
            {
                CarId = car.Id,
                Marca = car.Marca,
                Model = car.Model,
                An = car.An,
                Pret = car.Pret,
                DateCreated = car.DateCreated.Value,
                DateModified = DateTime.UtcNow
            });
            db.SaveChanges();

            return View("Index", db.Cars.ToList());
        }

        [HttpGet]
        public ActionResult ViewHistory(int id)
        {
            var carHistory = db.CarHistories.Where(c => c.CarId == id).ToList();
            return View(carHistory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            db.Cars.Remove(car);
            db.SaveChanges();
            return View("Index", db.Cars.ToList());
        }

        [HttpGet]
        public ActionResult Stats()
        {
            int id = 5;
            var carhistory = db.CarHistories.Where(c => c.CarId == id).OrderBy(c => c.Pret).ToList();

            var minPriceCar = carhistory.FirstOrDefault();

            var minPrice = minPriceCar.Pret;
            var minDate = minPriceCar.DateModified;

            var nextCar = carhistory.Where(c => c.DateModified > minDate && c.Pret != minPrice).OrderBy(c => c.DateModified).FirstOrDefault();

            var maxDate = DateTime.UtcNow;
            if (nextCar != null)
                maxDate = nextCar.DateModified;

            var dateRange = 0d;

            if (maxDate > minDate)
                dateRange = Math.Round((maxDate - minDate).TotalMinutes, 2);
            else
                dateRange = Math.Round((minDate - maxDate).TotalMinutes, 2);

            var variatii = carhistory.OrderBy(c => c.DateModified).Select(c => new KeyValuePair<int, DateTime>(c.Pret, c.DateModified)).ToList();

            return View(new CarStatistics
            {
                MinPret = minPrice,
                MinDurata = dateRange,
                Variatii = variatii
            }
            );
        }

        [HttpGet]
        public ActionResult Find(int id)
        {
            globalId = id;
            return View();
        }

        [HttpGet]
        public ActionResult Show(string date)
        {
            if (date == string.Empty)
                return View("Index", db.Cars.ToList());
            var datex = DateTime.Parse(date).Date;
            var x = db.CarHistories.ToList().FirstOrDefault(c => c.CarId == globalId && c.DateModified.Date == datex);

            return View(x);
        }
       
    }
}