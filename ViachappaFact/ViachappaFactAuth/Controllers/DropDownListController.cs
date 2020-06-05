using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViachappaFactAuth.Controllers
{
    [Authorize]
    public class DropDownListController : Controller
    {
        DB.AAModel db = new DB.AAModel();
        
        public string Departments(string key)
        {
            if(string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.Departments.Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
            else return JsonConvert.SerializeObject(db.Departments.Where(x => x.Name.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
        }

        public string Employees(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.Employees.Take(30).ToList().Select(x => new { id = x.Id, text = $@"{x.LastName} {x.FirstName} {x.SecondName}" }).ToArray());
            else return JsonConvert.SerializeObject(db.Employees.Where(x => x.LastName.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = $@"{x.LastName} {x.FirstName} {x.SecondName}" }).ToArray());
        }

        public string PlanAccountTypes(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.PlanAccountTypes.ToList().Select(x => new { id = x.Id, text = $@"{x.Number} {x.Name}" }).ToArray());
            else return JsonConvert.SerializeObject(db.PlanAccountTypes.Where(x => x.Name.Contains(key) || x.Number.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = $@"{x.Number} {x.Name}" }).ToArray());
        }
        public string ProductProcessingOperations(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.ProductProcessingOperations.Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
            else return JsonConvert.SerializeObject(db.ProductProcessingOperations.Where(x => x.Name.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
        }

        public string Partners(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.Partners.Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
            else return JsonConvert.SerializeObject(db.Partners.Where(x => x.Name.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
        }

        public string Organisations(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.Organisations.Take(30).ToList().Select(x => new { id = x.Id.ToString(), text = x.Name }).ToArray());
            else return JsonConvert.SerializeObject(db.Organisations.Where(x => x.Name.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id.ToString(), text = x.Name }).ToArray());
        }

        public string Materials(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.Materials.Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
            else return JsonConvert.SerializeObject(db.Materials.Where(x => x.Name.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
        }

        public string VATRates(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(db.VATRates.Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
            else return JsonConvert.SerializeObject(db.VATRates.Where(x => x.Name.Contains(key)).Take(30).ToList().Select(x => new { id = x.Id, text = x.Name }).ToArray());
        }

        public ActionResult ContractsForPartner(string id)
        {
            var p = id;
            var g = Guid.Parse(id);
            return View(db.Contracts.Where(x => x.Partner_Id == g && !x.isDeleted).ToList());
        }

        public double GetMaterialRest(Guid id)
        {
            try
            {
                var count = db.WarehouseItems.Where(x => x.Material_Id == id).Sum(x => x.Count);
                return count > 0 ? count : 0;
            }
            catch
            {
                return 0;
            }
        }

    }
}