using MortgageCalculator;
using MortgageCalculatorWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MortgageCalculatorWeb.Controllers
{
    public class MortgageController : Controller
    {
        // GET: Mortgage
        public ActionResult Index()
        {
            MortgagePersister persister = GetPersister();

            return View(persister.Mortgages);
        }

        private MortgagePersister GetPersister()
        {
            return new MortgagePersister(Server.MapPath("~/App_Data/prospects.txt"));
        }

        // GET: Mortgage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mortgage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                decimal.TryParse(collection["TotalLoan"],NumberStyles.Number, CultureInfo.InvariantCulture, out var totalLoan);
                decimal.TryParse(collection["Interest"], NumberStyles.Number, CultureInfo.InvariantCulture, out var interest);
                decimal.TryParse(collection["Years"], NumberStyles.Number, CultureInfo.InvariantCulture, out var years);
                CustomerMortgage mortgage = new CustomerMortgage(collection["Name"], totalLoan, interest, years);

                var persister = GetPersister();
                persister.Mortgages.Add(mortgage);
                persister.Persist();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mortgage/Edit/5
        public ActionResult Edit(int id)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var mortgage = GetPersister().Mortgages[id];
            ViewBag.Id = id;
            return View(mortgage);
        }

        // POST: Mortgage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var persister = GetPersister();

                decimal.TryParse(collection["TotalLoan"], NumberStyles.Number, CultureInfo.InvariantCulture, out var totalLoan);
                decimal.TryParse(collection["Interest"], NumberStyles.Number, CultureInfo.InvariantCulture, out var interest);
                decimal.TryParse(collection["Years"], NumberStyles.Number, CultureInfo.InvariantCulture, out var years);
                CustomerMortgage mortgage = new CustomerMortgage(collection["Name"], totalLoan, interest, years);

                persister.Mortgages[id] = mortgage;
                persister.Persist();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mortgage/Delete/5
        public ActionResult Delete(int id)
        {
            var persister = GetPersister();
            persister.Mortgages.RemoveAt(id);
            persister.Persist();

            return RedirectToAction("index");
        }
    }
}
