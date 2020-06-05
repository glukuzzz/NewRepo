
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System.Net.Http;
using Newtonsoft.Json;

namespace ViachappaFactAuth.Controllers
{

    [Authorize]
    public class FactoryController : Controller
    {
        DB.AAModel db = new DB.AAModel();
        // GET: Factory
        public ActionResult Index() => View();
        public ActionResult ListCurriencies() => View("ListCurriencies", db.Currencies.ToList());
        public ActionResult DepartmentList() => View("DepartmentList", db.Departments.ToList());
        public ActionResult ListEmployees() => View("ListEmployees", db.Employees.ToList());
        [HttpGet]
        public ActionResult EmployeeCreate()
        {
            // ViewBag.Departments = db.Departments.ToDictionary(x => x.Id, x => x.Name);
            return View("EmployeeCreate", new DB.Employee { });
        }
        [HttpPost]
        public ActionResult EmployeeCreate(DB.Employee emp)
        {
            // ViewBag.Departments = db.Departments.ToDictionary(x => x.Id, x => x.Name);
            //try
            //{
            emp.Id = db.Employees.Max(x => x.Id) + 1;

            db.Employees.Add(emp);
            db.SaveChanges();
            //}
            //catch { }
            return RedirectToAction("ListEmployees");
        }
        public ActionResult EmployeeDelete(int id)
        {
            db.Employees.Remove(db.Employees.First(x => x.Id == id));
            db.SaveChanges();
            return RedirectToAction("ListEmployees");
        }

        public ActionResult ListFixedAssets() => View("ListFixedAssets", db.FixedAssets.ToList());
        public ActionResult ListIntangibleAssets() => View("ListIntangibleAssets", db.IntangibleAssets.ToList());
        public ActionResult PartnerList() => View("PartnerList", db.Partners.ToList());
        [HttpGet]
        public ActionResult PartnerCreate(Guid? id)
        {
            ViewBag.Edit = id.HasValue;
            if (id.HasValue)
            {
                ViewBag.BankAccounts = new List<(Guid, string, string, string)>();
            }

            if (!id.HasValue) return View("PartnerCreate", new DB.Partner { });
            else
            {
                ViewBag.BankAccounts = db.PartnerBankAccounts.Where(x => x.Partner_Id == id).ToList().Select(x => (x.Id, x.BankName, x.PaymentAccount, x.Currency.Name)).ToList();
                return View("PartnerCreate", db.Partners.FirstOrDefault(x => x.Id == id));
            }
        }
        [HttpPost]
        public ActionResult PartnerCreate(DB.Partner partner)
        {

            var part = db.Partners.FirstOrDefault(x => x.Id == partner.Id);
            ViewBag.Edit = part != null;

            if (part == null)
            {
                partner.Id = Guid.NewGuid();
                db.Partners.Add(partner);
                db.SaveChanges();
                //db.Contracts.Add(new DB.Contract
                //{
                //    Id = Guid.NewGuid(),
                //    Abstract = "Основной договор",
                //    ContractDate = DateTime.Now,
                //    ContractNumber = $@"000-{partner.Name}",
                //    DateStart = DateTime.Today,
                //    Partner_Id = partner.Id
                //});
                //db.SaveChanges();

                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        var t = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        foreach (var ve in eve.ValidationErrors)
                //        {
                //            var p = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                //                ve.PropertyName, ve.ErrorMessage);
                //        }
                //    }
                //    throw;
                //}
            }
            else
            {
                part.INN = partner.INN;
                part.KPP = partner.KPP;
                part.Name = partner.Name;
                part.OGRN = partner.OGRN;
                part.OKPO = partner.OKPO;
                part.OKATO = partner.OKATO;
                part.LegalAddress = partner.LegalAddress;
                part.ActualAddress = partner.LegalAddress;
                part.Director = partner.Director;
                part.AccountantGeneral = partner.AccountantGeneral;
                part.PhoneNumber = partner.PhoneNumber;
                part.Fax = partner.Fax;
                part.Email = partner.Email;
                part.ShortName = partner.ShortName;
                db.SaveChanges();
            }

            return RedirectToAction("PartnerList");
        }



        [HttpGet]
        public ActionResult PartnerBankAccountCreate(Guid id, Guid? acc_id)
        {
            ViewBag.Edit = acc_id.HasValue;
            if (!acc_id.HasValue) return View("PartnerBankAccountCreate", new DB.PartnerBankAccount { Partner_Id = id });
            else return View("PartnerBankAccountCreate", db.PartnerBankAccounts.FirstOrDefault(x => x.Partner_Id == id && x.Id == acc_id));
        }
        [HttpPost]
        public ActionResult PartnerBankAccountCreate(DB.PartnerBankAccount acc)
        {

            var guid = Guid.Parse(acc.Id.ToString());
            var part = db.PartnerBankAccounts.FirstOrDefault(x => x.Id == guid);
            ViewBag.Edit = part != null;
            if (part == null)
            {
                acc.Id = Guid.NewGuid();

                try
                {
                    db.PartnerBankAccounts.Add(acc);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var t = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var p = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            else
            {
                part.Currency_Id = acc.Currency_Id;
                part.BIK = acc.BIK;
                part.BankName = acc.BankName;
                part.CorrespondentAccount = acc.CorrespondentAccount;
                part.PaymentAccount = acc.PaymentAccount;
                db.SaveChanges();
            }

            return RedirectToAction("PartnerCreate", new { id = acc.Partner_Id });
        }




        public ActionResult PartnerDelete(Guid id)
        {
            db.Partners.Remove(db.Partners.First(x => x.Id == id));
            db.SaveChanges();
            return RedirectToAction("ListPartners");
        }



        public ActionResult GetOpers()
        {
            var t = db.Departments.Join(db.ProductProcessingOperations, x => x.Id, x => x.Department_Id, (dep, oper) => new { dep, oper }).ToList()
                .Select(x => (x.dep.Id, x.dep.Name, x.oper.Id, x.oper.Name)).ToList();
            return View("GetOpers", t);
        }

        public FileResult TestPDF()
        {
            Document document = new Document();
            document.AddSection();
            Paragraph paragraph = document.LastSection.AddParagraph("Table Overview", "Heading1");
            // Create an empty page



            document.LastSection.AddParagraph("Simple Tables", "Heading2");

            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromCentimeter(2));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(5));

            Row row = table.AddRow();
            row.Shading.Color = Colors.PaleGoldenrod;
            Cell cell = row.Cells[0];
            cell.AddParagraph("Itemus");
            cell = row.Cells[1];
            cell.AddParagraph("Descriptum");

            row = table.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("1");
            cell = row.Cells[1];
            cell.AddParagraph("ggggg");

            row = table.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("2");
            cell = row.Cells[1];
            cell.AddParagraph("ffff");

            table.SetEdge(0, 0, 2, 3, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

            document.LastSection.Add(table);


            MemoryStream stream = new MemoryStream();
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(stream);
            return File(stream.ToArray(), "application/pdf", "test.pdf");
        }

        public ActionResult MovingAssetDocumentList() => View(db.MovingAssetDocuments.ToList());


        [HttpGet]
        public ActionResult MovingAssetDocumentCreate(Guid? id)
        {
            if (!id.HasValue)
            {
                return View(new DB.MovingAssetDocument { Id = Guid.NewGuid() });
            }
            else
            {
                return View(db.MovingAssetDocuments.First(x => x.Id == id));
            }
        }

        [HttpPost]
        public ActionResult MovingAssetDocumentCreate(DB.MovingAssetDocument doc)
        {
            var guid = Guid.NewGuid();
            db.MovingAssetDocuments.Add(new DB.MovingAssetDocument
            {
                DocDate = doc.DocDate,
                Name = doc.Name,
                OperDate = DateTime.Now,
                DocNumber = db.MovingAssetDocuments.Count() + 1,
                Id = guid,
                Organisation_Id = doc.Organisation_Id
            });
            db.SaveChanges();
            return RedirectToAction("MovingAssetDocument", new { id = guid });
        }
        [HttpGet]
        public ActionResult MovingAssetDocument(Guid id) => View(db.MovingAssetDocuments.First(x => x.Id == id));

        [HttpPost]
        public ActionResult MovingAssetDocument()
        {
            var doc_id = Guid.Parse(Request.Form["Document_Id"]);

            var p = Request.Form.AllKeys;

            var pos = new DB.MovingAssetDocumentPosition();
            pos.Id = Guid.NewGuid();
            pos.Document_Id = doc_id;
            pos.Cost = Convert.ToDecimal(Request.Form["Cost"]);
            pos.Content = Request.Form["Content"];
            pos.SubcontoArray = new DB.SubcontoArray
            {
                Id = Guid.NewGuid(),
                PlanAccountType_Id = Convert.ToInt32(Request.Form["SubcontoArray_Id"])
            };

            pos.SubcontoArray1 = new DB.SubcontoArray
            {
                Id = Guid.NewGuid(),
                PlanAccountType_Id = Convert.ToInt32(Request.Form["SubcontoArray_Id_To"])
            };


            try
            {
                db.MovingAssetDocumentPositions.Add(pos);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var list = new List<string>();
                foreach (var i in ex.EntityValidationErrors)
                    foreach (var j in i.ValidationErrors)
                        list.Add($@"{j.ErrorMessage} {j.PropertyName}");
                throw ex;
            }

            return RedirectToAction("MovingAssetDocument", new { id = doc_id });
        }


        public ActionResult MovingAssetDocumentPositionCreate() => View();


        public ActionResult SubcontoArrayBuilder(string type, int pa)
        {
            ViewBag.Type = type;
            ViewBag.Val = pa;
            return View(db.PlanAccountTypes.First(x => x.Id == pa).SubcontoTypes.ToList());
        }


        public ActionResult MovingAssetDocumentPositionList() => View();

        //[HttpPost]
        //public ActionResult MovingAssetDocument(DB.MovingAssetDocument doc)
        //{
        //    var p = new Dictionary<string, long>();
        //    db.MovingAssetDocuments.Add(doc);
        //    db.SaveChanges();
        //    if (Request.Files.Count > 0)
        //    {
        //        foreach (string f in Request.Files)
        //        {
        //            var file = Request.Files[f];
        //            p.Add(file.FileName, file.ContentLength);

        //            //byte[] fileData = null;
        //            //using (var binaryReader = new BinaryReader(file.InputStream))
        //            //{
        //            //    fileData = binaryReader.ReadBytes(file.ContentLength);
        //            //}
        //        }
        //    }
        //    ViewBag.Test = p;
        //    return RedirectToAction("MovingAssetDocumentsList");
        //}

        //[HttpPost]
        //public ActionResult MovingAssetDocumentCreate(DB.MovingAssetDocument doc)
        //{
        //    doc.Id = Guid.NewGuid();
        //    doc.OperDate = DateTime.Now;
        //    db.MovingAssetDocuments.Add(doc);
        //    db.SaveChanges();
        //    return RedirectToAction("MovingAssetDocumentsList");
        //}

        //public FileResult MovingAssetDocumentsCsv()
        //{
        //    byte[] dataAsBytes = db.MovingAssetDocuments.ToList().Select(x => $@"{x.Cost};{x.Department.Name};{x.Department1.Name};{x.DocNumber};{x.OperDate};{x.Partner.Name};{x.Id}").SelectMany(s => Encoding.GetEncoding(1251).GetBytes(s + Environment.NewLine)).ToArray();
        //    var stream = new MemoryStream(dataAsBytes);
        //    return File(stream, "text/csv", $"MovingAssetDocuments.csv");
        //}



        public ActionResult ProductProcessingOperationList()
        {
            return View("ProductProcessingOperationList", db.ProductProcessingOperations.ToList());
        }
        [HttpGet]
        public ActionResult ProductProcessingOperationCreate(int? id)
        {
            ViewBag.Departments = db.Departments.ToDictionary(x => x.Id, x => x.Name);
            if (id.HasValue) return View("ProductProcessingOperationCreate", db.ProductProcessingOperations.FirstOrDefault(x => x.Id == id));
            else return View("ProductProcessingOperationCreate", new DB.ProductProcessingOperation());
        }
        [HttpPost]
        public ActionResult ProductProcessingOperationCreate(DB.ProductProcessingOperation doc)
        {
            var olddoc = db.ProductProcessingOperations.FirstOrDefault(x => x.Id == doc.Id);
            if (olddoc == null)
            {
                // ViewBag.Departments = db.Departments.ToDictionary(x => x.Id, x => x.Name);
                doc.Id = db.ProductProcessingOperations.Max(x => x.Id) + 1;
                db.ProductProcessingOperations.Add(doc);
                db.SaveChanges();
            }
            else
            {
                olddoc.Name = doc.Name;
                olddoc.Department_Id = doc.Department_Id;
                db.SaveChanges();
            }

            return RedirectToAction("ProductProcessingOperationList");
        }

        public ActionResult WarehouseList()
        {
            return View("WarehouseList", db.Warehouses.ToList());


        }

        [HttpGet]
        public ActionResult WarehouseCreate()
        {
            return View("WarehouseCreate", new DB.Warehouse());
        }
        [HttpPost]
        public ActionResult WarehouseCreate(DB.Warehouse doc)
        {
            var id = 0;
            var w_Id = db.Warehouses.Where(x => x.Department_Id == doc.Department_Id).Select(x => x.id);
            if (w_Id.Count() == 0) id = 1;
            else
            {
                id = (w_Id.Max() % 100) + 1;
            }
            doc.id = doc.Department_Id * 100 + id;
            db.Warehouses.Add(doc);
            db.SaveChanges();
            return RedirectToAction("WarehouseList");
        }

        public ActionResult WarehouseStatus()
        {
          var t =  db.WarehouseItems.GroupBy(x => new
            {
                Articul = x.Material.Articul,
                Name = x.Material.Name,
                Measure = x.Material.UnitName,
                x.Warehouse_Id,
            }).Select(x => new Models.WarehouseStatusItem
            {
                Articul = x.Key.Articul,
                Name = x.Key.Name,
                Measure = x.Key.Measure,
                Warehouse_Id = x.Key.Warehouse_Id,
                ItemCount = x.Sum(y => y.Count)
            }).Where(x => x.ItemCount !=0).ToList();

            return View(t);
        }

       

        public ActionResult MaterialList() => View(db.Materials.ToList());


        [HttpGet]
        public ActionResult MaterialCreate()
        {
            //ViewBag.Departments = db.Departments.ToDictionary(x => x.Id, x => x.Name);
            //ViewBag.Employees = db.Employees.ToDictionary(x => x.Id, x => $@"{x.LastName} {x.FirstName} {x.SecondName}");
            return View("MaterialCreate", new DB.Material());
        }
        [HttpPost]
        public ActionResult MaterialCreate(DB.Material doc)
        {
            doc.MaterialType_Id = 1;
            //ViewBag.Employees = db.Employees.ToDictionary(x => x.Id, x => $@"{x.LastName} {x.FirstName} {x.SecondName}");
            //ViewBag.Departments = db.Departments.ToDictionary(x => x.Id, x => x.Name);
            doc.Id = Guid.NewGuid();
            db.Materials.Add(doc);

            db.SaveChanges();
            return RedirectToAction("MaterialList");
        }

        public ActionResult StaffingUnitList()
        {
            return View(db.StaffingUnits.Where(x => x.DateStart <= DateTime.Today && x.DateEnd >= DateTime.Today).ToList());
        }


        [HttpGet]
        public ActionResult StaffingUnitCreate()
        {
            return View(new DB.Material());
        }
        [HttpPost]
        public ActionResult StaffingUnitCreate(DB.Material doc)
        {


            doc.Id = Guid.NewGuid();
            db.Materials.Add(doc);
            db.SaveChanges();
            return RedirectToAction("StaffingUnitList");
        }

        public ActionResult EmployeeToStuffingUnit(Guid id)
        {

            return View();
        }

        public ActionResult PlanAccountTypeList() => View(db.PlanAccountTypes.ToList());

        public ActionResult PlanAccountTypeCreate(int? id)
        {
            if (Request.Form.Count == 0)
            {
                if (!id.HasValue)
                {
                    ViewBag.PlanSubcontoTypes = db.SubcontoTypes.ToList().Select(x => (x.Id, $@"{x.Code} {x.Name}", "")).ToList();
                    return View(new DB.PlanAccountType());

                }
                else
                {
                    var selected = db.PlanAccountTypes.First(x => x.Id == id).SubcontoTypes.Select(x => x.Id);
                    ViewBag.PlanSubcontoTypes = db.SubcontoTypes.ToList().Select(x => (x.Id, $@"{x.Code} {x.Name}", selected.Contains(x.Id) ? "selected" : "")).ToList();
                    return View(db.PlanAccountTypes.First(x => x.Id == (int)id));
                }
            }
            else
            {
                var i = Convert.ToInt32(Request.Form["Id"]);
                if (i == 0)
                {
                    var pa = new DB.PlanAccountType();
                    pa.Number = Request.Form["Number"];
                    pa.Name = Request.Form["Name"];
                    var p1 = new int[] { };
                    try
                    {
                        p1 = Request.Form["PlanSubcontoTypes"].Split(',').Select(x => int.Parse(x)).ToArray();
                    }
                    catch { }
                    pa.SubcontoTypes = db.SubcontoTypes.Where(y => p1.Contains(y.Id)).ToList();
                    db.PlanAccountTypes.Add(pa);
                    db.SaveChanges();
                }
                else
                {
                    db.Database.ExecuteSqlCommand($"delete from PlanAccountsSubcontosTypes where PlanAccountType_Id = {i}");
                    var pa = db.PlanAccountTypes.First(x => x.Id == i);
                    pa.Number = Request.Form["Number"];
                    pa.Name = Request.Form["Name"];
                    var p1 = new int[] { };
                    try
                    {
                        p1 = Request.Form["PlanSubcontoTypes"].Split(',').Select(x => int.Parse(x)).ToArray();
                    }
                    catch { }
                    pa.SubcontoTypes = db.SubcontoTypes.Where(y => p1.Contains(y.Id)).ToList();
                    db.SaveChanges();
                }
                return RedirectToAction("PlanAccountTypeList");
            }
        }

        public ActionResult SubcontoTypeList() => View(db.SubcontoTypes.ToList());

        public ActionResult CostItemList() => View(db.CostItems.ToList());

        public ActionResult CostItemCreate(Guid? id, int? delete)
        {
            if (id.HasValue && delete.HasValue)
            {
                db.Database.ExecuteSqlCommand($@"delete from CostItems where Id = '{id}'");
                return RedirectToAction("CostItemList");
            }
            if (Request.Form.Count == 0)
            {
                ViewBag.CostGroups = db.CostGroups.ToDictionary(x => x.Id, x => x.Name);
                ViewBag.CostTypes = db.CostTypes.ToDictionary(x => x.Id, x => x.Name);
                ViewBag.CostNatures = db.CostNatures.ToDictionary(x => x.Id, x => x.Name);
                ViewBag.ExpensesTypes = db.ExpensesTypes.ToDictionary(x => x.Id, x => x.Name);

                if (!id.HasValue) return View(new DB.CostItem { Id = Guid.NewGuid() });
                else return View(db.CostItems.First(x => x.Id == id));
            }
            else
            {
                var guid = Guid.Parse(Request.Form["Id"]);
                var t = db.CostItems.FirstOrDefault(x => x.Id == guid);
                if (t == null)
                {
                    db.CostItems.Add(new DB.CostItem
                    {
                        Id = guid,
                        CostGroup_Id = Convert.ToInt32(Request.Form["CostGroup_Id"]),
                        Name = Request.Form["Name"],
                        CostType_Id = Convert.ToInt32(Request.Form["CostType_Id"]),
                        CostNature_Id = Convert.ToInt32(Request.Form["CostNature_Id"]),
                        ExpensesType_Id = Convert.ToInt32(Request.Form["ExpensesType_Id"]),
                    });
                }
                else
                {
                    t.CostGroup_Id = Convert.ToInt32(Request.Form["CostGroup_Id"]);
                    t.Name = Request.Form["Name"];
                    t.CostType_Id = Convert.ToInt32(Request.Form["CostType_Id"]);
                    t.CostNature_Id = Convert.ToInt32(Request.Form["CostNature_Id"]);
                    t.ExpensesType_Id = Convert.ToInt32(Request.Form["ExpensesType_Id"]);
                }
                db.SaveChanges();
                return RedirectToAction("CostItemList");
            }
        }
        public ActionResult CostGroupList() => View(db.CostGroups.ToList());
        public ActionResult CostGroupCreate(int? id, int? delete)
        {
            if (id.HasValue && delete.HasValue)
            {
                db.Database.ExecuteSqlCommand($@"delete from CostGroups where Id = {id}");
                return RedirectToAction("CostGroupList");
            }


            if (Request.Form.Count == 0)
            {
                if (!id.HasValue) return View(new DB.CostGroup { Id = 0 });
                else return View(db.CostGroups.First(x => x.Id == id));
            }
            else
            {
                var guid = Convert.ToInt32(Request.Form["Id"]);
                var t = db.CostGroups.FirstOrDefault(x => x.Id == guid);
                if (t == null)
                {
                    db.CostGroups.Add(new DB.CostGroup
                    {
                        Name = Request.Form["Name"]
                    });
                }
                else
                {
                    t.Name = Request.Form["Name"];
                }
                db.SaveChanges();
                return RedirectToAction("CostItemCreate");
            }
        }


        public ActionResult AssetsLiabilitiesTypesList() => View(db.AssetsLiabilitiesTypes.ToList());

        public ActionResult RDList() => View(db.ResearchDevelopmentTypes.ToList());

        public ActionResult OrganisationList() => View(db.Organisations.ToList());

        public ActionResult OrganisationCreate(int? id, int? delete)
        {
            if (id.HasValue && delete.HasValue)
            {
                db.Database.ExecuteSqlCommand($@"update  Organisations set IsDeleted = 1 where Id = '{id}'");
                return RedirectToAction("OrganisationList");
            }
            if (Request.Form.Count == 0)
            {


                if (!id.HasValue) return View(new DB.Organisation { Id = 0 });
                else return View(db.Organisations.First(x => x.Id == id));
            }
            else
            {
                var guid = Convert.ToInt32(Request.Form["Id"]);
                var t = db.Organisations.FirstOrDefault(x => x.Id == guid);
                if (t == null)
                {
                    db.Organisations.Add(new DB.Organisation
                    {
                        Name = Request.Form["Name"],
                        FullName = Request.Form["FullName"],
                        ShortName = Request.Form["ShortName"],
                        IFNS = Convert.ToInt32(Request.Form["IFNS"]),
                        OKATO = Convert.ToInt64(Request.Form["OKATO"]),
                        IFNS_Name = Request.Form["IFNS_Name"],
                        IFNS_RegDate = Convert.ToDateTime(string.IsNullOrEmpty(Request.Form["IFNS_RegDate"]) ? "1900-10-10" : Request.Form["IFNS_RegDate"]),
                        INN = Convert.ToInt64(Request.Form["INN"]),
                        IFNS_RegName = Request.Form["IFNS_RegName"],
                        KPP = Convert.ToInt64(Request.Form["KPP"]),
                        OKPO = Convert.ToInt64(string.IsNullOrEmpty(Request.Form["OKPO"]) ? "0" : Request.Form["OKPO"]),
                        OGRN = Convert.ToInt64(Request.Form["OGRN"]),
                        IsDeleted = false

                    });
                }
                else
                {
                    t.Name = Request.Form["Name"];
                    t.FullName = Request.Form["FullName"];
                    t.ShortName = Request.Form["ShortName"];
                    t.IFNS = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["IFNS"]) ? "0" : Request.Form["IFNS"]);
                    t.OKATO = Convert.ToInt64(string.IsNullOrEmpty(Request.Form["OKATO"]) ? "0" : Request.Form["OKATO"]);
                    t.IFNS_Name = Request.Form["IFNS_Name"];
                    t.IFNS_RegDate = Convert.ToDateTime(string.IsNullOrEmpty(Request.Form["IFNS_RegDate"]) ? "1900-10-10" : Request.Form["IFNS_RegDate"]);
                    t.INN = Convert.ToInt64(Request.Form["INN"]);
                    t.IFNS_RegName = Request.Form["IFNS_RegName"];
                    t.KPP = Convert.ToInt64(Request.Form["KPP"]);
                    t.OKPO = Convert.ToInt64(string.IsNullOrEmpty(Request.Form["OKPO"]) ? "0" : Request.Form["OKPO"]);
                    t.OGRN = Convert.ToInt64(Request.Form["OGRN"]);
                    t.IsDeleted = false;
                }
                db.SaveChanges();
                return RedirectToAction("OrganisationList");
            }
        }

        public ActionResult OutgoingOrders() => View(db.Orders.Where(x => x.OrderType_Id == 2).OrderBy(x => x.DocDate).ThenBy(x => x.Id).ToList());

        [HttpGet]
        public ActionResult OutgoingOrdersCreate(Guid? id)
        {
            var start_year = new DateTime(DateTime.Today.Year, 1, 1);
            if (id.HasValue) return View(db.Orders.FirstOrDefault(x => x.Id == id));
            else return View(new DB.Order { Id = Guid.NewGuid(), OperDate = DateTime.Now
                , DocNumber = db.Orders.Where(x => x.DocDate >= start_year && x.OrderType_Id == 2).Max(x => x.DocNumber) + 1, DocDate = DateTime.Today,
               OrderType_Id = 2
            });
        }
        [HttpPost]
        public ActionResult OutgoingOrdersCreate(DB.Order order)
        {
            var o = db.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (o == null) db.Orders.Add(order);
            else
            {
                o.OperDate = order.OperDate;
                o.Partner_Id = order.Partner_Id;
                o.Organisation_Id = order.Organisation_Id;
                o.DocNumber = order.DocNumber;
                o.DocDate = order.DocDate;
                o.Contract_Id = order.Contract_Id;
                o.OrderType_Id = order.OrderType_Id;

            }
            db.SaveChanges();
            return RedirectToAction("OutgoingOrders");
        }
        public ActionResult OutgoingOrder(Guid id, int? edit)
        {
            ViewBag.Edit = edit.HasValue;
            if (Request.Form.Count > 0)
            {
                var t = db.Orders.FirstOrDefault(x => x.Id == id);
                t.Organisation_Id = Convert.ToInt32(Request.Form["Organisation_Id"]);
                t.Partner_Id = Guid.Parse(Request.Form["Partner_Id"]);
                t.Contract_Id = Guid.Parse(Request.Form["Contract_Id"]);
                db.SaveChanges();
            }
            return View(db.Orders.First(x => x.Id == id));
        }
        [HttpGet]
        public ActionResult OutgoingOrderPositionCreate() => View(new DB.OrderPosition());
        [HttpPost]
        public ActionResult OutgoingOrderPositionCreate(DB.OrderPosition pos)
        {
            pos.Id = Guid.NewGuid();
            db.OrderPositions.Add(pos);
            db.SaveChanges();
            return RedirectToAction("OutgoingOrder", new { Id = pos.Order_Id });
        }

        public ActionResult IncomingOrders() => View(db.Orders.Where(x => x.OrderType_Id == 1).OrderBy(x => x.DocDate).ThenBy(x => x.Id).ToList());

        [HttpGet]
        public ActionResult IncomingOrdersCreate(Guid? id)
        {
            ViewBag.OrderTypes = db.OrderTypes.ToDictionary(x => x.Id, x => x.Name);
            var start_year = new DateTime(DateTime.Today.Year, 1, 1);
            if (id.HasValue) return View(db.Orders.FirstOrDefault(x => x.Id == id));
            else return View(new DB.Order
            {
                Id = Guid.NewGuid(),
                OperDate = DateTime.Now,
                OrderType_Id =1,
                DocDate = DateTime.Today
            });
        }

        [HttpPost]
        public ActionResult IncomingOrdersCreate(DB.Order order)
        {
            var o = db.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (o == null) {
                if (order.OrderType_Id %2 ==0)
                {
                    var start_year = new DateTime(DateTime.Today.Year, 1, 1);
                    order.DocNumber = (db.Orders.Where(x => x.DocDate >= start_year && x.OrderType_Id == order.OrderType_Id).Count()>0 ? db.Orders.Where(x => x.DocDate >= start_year && x.OrderType_Id == order.OrderType_Id).Select(x=>x.DocNumber).ToList().Select(x => int.Parse(x)).Max() + 1 : 1).ToString();
                    order.DocDate = DateTime.Today;
                    order.isDeleted = false;
                    order.isPaid = false;
                }
                db.Orders.Add(order);
            }
            else
            {
                o.OperDate = order.OperDate;
                o.Partner_Id = order.Partner_Id;
                o.Organisation_Id = order.Organisation_Id;
                o.DocNumber = order.DocNumber;
                o.DocDate = order.DocDate;
                o.Contract_Id = order.Contract_Id;
                o.OrderType_Id = order.OrderType_Id;
            }

           
            db.SaveChanges();
            return RedirectToAction("IncomingOrder", new { id = order.Id });
        }
        public ActionResult IncomingOrder(Guid id, int? edit)
        {
            ViewBag.Edit = edit.HasValue;
            ViewBag.Warehouses = db.Warehouses.ToDictionary(x => x.id, x => x.Name);
            if (Request.Form.Count > 0)
            {
                var t = db.Orders.FirstOrDefault(x => x.Id == id);
                t.Organisation_Id = Convert.ToInt32(Request.Form["Organisation_Id"]);
                t.Partner_Id = Guid.Parse(Request.Form["Partner_Id"]);
                t.Contract_Id = Guid.Parse(Request.Form["Contract_Id"]);
                db.SaveChanges();
            }
            return View(db.Orders.First(x => x.Id == id));
        }
       
        public ActionResult IncomingOrderPositionCreate() 
        {

            if (Request.Form.Count > 0)
            {
                var id_pos = Guid.Parse(Request.Form["Id"]);
                var id_order = Guid.Parse(Request.Form["Order_Id"]);
                var pos = db.OrderPositions.FirstOrDefault(x => x.Id == id_pos && x.Order_Id == id_order);
                if (pos == null)
                {
                    pos = new DB.OrderPosition();
                    pos.Id = Guid.NewGuid();
                    pos.Order_Id = Guid.Parse(Request.Form["Order_Id"]);
                    pos.Count = Convert.ToDouble(Request.Form["Count"].Replace(".", ","));
                    pos.Price = Convert.ToDecimal(Request.Form["Price"].Replace(".", ","));
                    pos.Material_Id = Guid.Parse(Request.Form["Material_Id"]);
                    pos.PlanAccountType_NDS_Id = Convert.ToInt32(Request.Form["PlanAccountType_NDS_Id"]);
                    pos.PlanAccountType_Id = Convert.ToInt32(Request.Form["PlanAccountType_Id"]);
                    db.OrderPositions.Add(pos);
                    int vatrate_id = Convert.ToInt32(Request.Form["VATRate_Id"]);
                    var vr1 = db.VATRates.First(x => x.Id == vatrate_id);
                    if (vr1.Parent.HasValue)
                    {
                        var vr = vr1.VATRate1;
                        pos.Price = pos.Price / ((decimal)vr.Price + 1);
                        pos.VATRate_Id = vr.Id;
                    }
                    else
                    {
                        pos.VATRate_Id = vr1.Id;
                    }
                    db.SaveChanges();
                    return RedirectToAction("IncomingOrder", new { Id = pos.Order_Id });
                }
                else
                {
                    pos.Count = Convert.ToDouble(Request.Form["Count"].Replace(".", ","));
                    pos.Price = Convert.ToDecimal(Request.Form["Price"].Replace(".", ","));
                    pos.Material_Id = Guid.Parse(Request.Form["Material_Id"]);
                    pos.PlanAccountType_NDS_Id = Convert.ToInt32(Request.Form["PlanAccountType_NDS_Id"]);
                    pos.PlanAccountType_Id = Convert.ToInt32(Request.Form["PlanAccountType_Id"]);
                    db.OrderPositions.Add(pos);
                    int vatrate_id = Convert.ToInt32(Request.Form["VATRate_Id"]);
                    var vr1 = db.VATRates.First(x => x.Id == vatrate_id);
                    if (vr1.Parent.HasValue)
                    {
                        var vr = vr1.VATRate1;
                        pos.Price = pos.Price / ((decimal)vr.Price + 1);
                        pos.VATRate_Id = vr.Id;
                    }
                    else
                    {
                        pos.VATRate_Id = vr1.Id;
                    }
                    db.SaveChanges();
                    return RedirectToAction("IncomingOrder", new { Id = pos.Order_Id });
                }
            }
            else
            {
                return View(new DB.OrderPosition());
            }
            
        }


        public ActionResult DeleteIncomingOrderPosition(Guid id)
        {
            var p = db.OrderPositions.First(x => x.Id == id);
            db.Database.ExecuteSqlCommand($"delete from OrderPositions where Id ='{p.Id}' ");
            return RedirectToAction("IncomingOrder", new { id = p.Order_Id });
        }
        public FileResult IncomingOrderPDF()
        {
            var workStream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(workStream))
            {
                pdfWriter.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument("<h1>ТОРГ12</h1><p>ыпаываыва</p>", pdfWriter)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }

        public ActionResult VATRates() => View(db.VATRates.ToList());

        public ActionResult ContractList() => View(db.Contracts.Where(x=>!x.isDeleted).ToList());

        public ActionResult ContractCreate()
        {
            if (Request.Form.Count > 0)
            {
                var guid = Guid.NewGuid();
                var p = new DB.Contract { Id = Guid.NewGuid() };
                p.Partner_Id = Guid.Parse(Request.Form["Partner_Id"]);
                p.Abstract = Request.Form["Abstract"];
                p.ContractDate = Convert.ToDateTime(Request.Form["ContractDate"]);
                p.DateStart = Convert.ToDateTime(Request.Form["DateStart"]);
                p.ContractNumber = Request.Form["ContractNumber"];
                p.UseUpd = Request.Form["UseUpd"].Contains("true");
                foreach (string f in Request.Files)
                {
                    var file = Request.Files[f];
                    using (var binaryReader = new BinaryReader(file.InputStream)) p.FileData = binaryReader.ReadBytes(file.ContentLength);
                    p.FileName = file.FileName;
                }
                db.Contracts.Add(p);
                db.SaveChanges();
                return RedirectToAction("ContractList");
            }
            else return View(new DB.Contract());
        }
        public ActionResult ContractSpecCreate()
        {
            if (Request.Form.Count > 0)
            {
                var p = new DB.ContractSpec { Id = Guid.NewGuid() };
                p.Date = Convert.ToDateTime(Request.Form["Date"]);
                p.Abstract = Request.Form["Abstract"];
                p.Contract_Id = Guid.Parse(Request.Form["Id"]);
                p.Price = Convert.ToDecimal(Request.Form["Price"].Replace(".",","));
                p.DocNumber = Request.Form["DocNumber"];
                foreach (string f in Request.Files)
                {
                    var file = Request.Files[f];
                    using (var binaryReader = new BinaryReader(file.InputStream)) p.FileData = binaryReader.ReadBytes(file.ContentLength);
                    p.FileName = file.FileName;
                }
                db.ContractSpecs.Add(p);
                db.SaveChanges();
                return RedirectToAction("Contract", new { id = p.Contract_Id });
            }
            else return View(new DB.Contract());
        }
        public ActionResult ContractAddCreate()
        {
            if (Request.Form.Count > 0)
            {
                var p = new DB.ContractAdd { Id = Guid.NewGuid() };
                p.Date = Convert.ToDateTime(Request.Form["Date"]);
                p.Abstract = Request.Form["Abstract"];
                p.Contract_Id = Guid.Parse(Request.Form["Id"]);
                p.DocNumber = Request.Form["DocNumber"];
                foreach (string f in Request.Files)
                {
                    var file = Request.Files[f];
                    using (var binaryReader = new BinaryReader(file.InputStream)) p.FileData = binaryReader.ReadBytes(file.ContentLength);
                    p.FileName = file.FileName;
                }
                db.ContractAdds.Add(p);
                db.SaveChanges();
                return RedirectToAction("Contract", new { id = p.Contract_Id });
            }
            else return View(new DB.Contract());
        }
        public FileResult DownloadContractDoc(Guid id)
        {
            var doc = db.Contracts.First(x => x.Id == id);
            if (doc.FileName.Contains(".pdf")) return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Pdf);
            else return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Octet, doc.FileName);
        }
        public FileResult DownloadContractAdd(Guid id)
        {
            var doc = db.ContractAdds.First(x => x.Id == id);
            if (doc.FileName.Contains(".pdf")) return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Pdf);
            else return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Octet, doc.FileName);
        }
        public FileResult DownloadContractSpec(Guid id)
        {
            var doc = db.ContractSpecs.First(x => x.Id == id);
            if (doc.FileName.Contains(".pdf")) return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Pdf);
            else return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Octet, doc.FileName);
        }

        public ActionResult Contract(Guid id)
        {
            return View(db.Contracts.First(x => x.Id == id));
        }
       
        public ActionResult ContractDelete(Guid id)
        {
            db.Contracts.First(x => x.Id == id).isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("ContractList");
        }


        Models.BankModel.RootObject BankInfoData(string bik)
        {
            Models.BankModel.RootObject bankinfo = null;
            var dadata_key = "blablabla";
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/bank"))
            {
                request.Headers.Add("Authorization", $@"Token {dadata_key}");
                request.Content = new StringContent(JsonConvert.SerializeObject(new { query = bik }), Encoding.UTF8, "application/json");
                var result = client.SendAsync(request).Result;
                bankinfo = (Models.BankModel.RootObject)JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result, typeof(Models.BankModel.RootObject));
            }
            return bankinfo;
        }

        Models.PartnerModel.RootObject ParnterInfoData(long inn)
        {
            Models.PartnerModel.RootObject partner_info = null;
            var dadata_key = "blablabla";
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/party"))
            {
                request.Headers.Add("Authorization", $@"Token {dadata_key}");
                request.Content = new StringContent(JsonConvert.SerializeObject(new { query = inn.ToString() }), Encoding.UTF8, "application/json");
                var result = client.SendAsync(request).Result;
                partner_info = (Models.PartnerModel.RootObject)JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result, typeof(Models.PartnerModel.RootObject));
            }
            return partner_info;
        }  
        

        public string PartnerInfo(long id)
        {
            if (id.ToString().Length < 8) throw new Exception();
            var b = ParnterInfoData(id).suggestions.First().data;
            return JsonConvert.SerializeObject(new
            {
                kpp = b.kpp,
                name = b.name.full_with_opf,
                shortName = b.name.short_with_opf,
                ogrn = b.ogrn,
                okpo = b.okpo,
                director = b.management.name,
                fact_adr = b.address.unrestricted_value,
                phonenumber = b.phones

            });
            //return View(ParnterInfoData(id).suggestions.First().data);
        }

        public ActionResult Orders() => View(db.Orders.Where(x=>!x.isDeleted).ToList());

        public ActionResult OrderDelete(Guid id)
        {
            db.Orders.First(x => x.Id == id).isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        public ActionResult OrderTransferDocs() => View(db.OrderTransferDocs.ToList());


        public ActionResult OrderInvoices() => View(db.OrderInvoices.ToList());

        

        public ActionResult OrderActs() => View(db.OrderActs.ToList());

        [HttpPost]
        public ActionResult OrderTransferDocCreate(Guid id)
        {
            var guid = Guid.NewGuid();
            var p = new DB.OrderTransferDoc { Id = Guid.NewGuid() };
          
            p.DocNumber = Request.Form["DocNumber"];
            p.DocDate = Convert.ToDateTime(Request.Form["DocDate"]);
            p.Warehouse_Id = Convert.ToInt32(Request.Form["Warehouse_Id"]);
            p.User_Id = db.AspNetUsers.First(x => x.UserName == User.Identity.Name).Id;
            p.OperDate = DateTime.Now;
            p.isPost = false;
            foreach (string f in Request.Files)
            {
                var file = Request.Files[f];
                using (var binaryReader = new BinaryReader(file.InputStream)) p.FileData = binaryReader.ReadBytes(file.ContentLength);
                p.FileName = file.FileName;
            }
            db.Orders.First(x => x.Id == id).OrderTransferDoc = p;
            db.SaveChanges();
            return RedirectToAction("IncomingOrder", new { id }); 
        }

        public FileResult DownloadOrderTransferDocFile(Guid id)
        {
            var doc = db.OrderTransferDocs.First(x => x.Id == id);
            if (doc.FileName.Contains(".pdf")) return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Pdf);
            else return File(doc.FileData, System.Net.Mime.MediaTypeNames.Application.Octet, doc.FileName);
        }

        public ActionResult OrderTransferDocPost(Guid id)
        {
            var order = db.Orders.First(x => x.OrderTransferDoc_Id == id);
            var i = order.OrderType_Id % 2 == 0 ?  -1: 1;
            foreach (var pos in order.OrderPositions)
            {
                db.WarehouseItems.Add(new DB.WarehouseItem
                {
                    Count = pos.Count * i,
                    Id = Guid.NewGuid(),
                    Material_Id = pos.Material_Id,
                    VATRate_Id = (int)pos.VATRate_Id,
                    Price = pos.Price,
                    OrderTransferDoc_Id = id,
                    OrderInvoiceDate = DateTime.Today,
                    Warehouse_Id = order.OrderTransferDoc.Warehouse_Id

                });
                db.SaveChanges();
            }
            db.OrderTransferDocs.FirstOrDefault(x => x.Id == id).isPost = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}