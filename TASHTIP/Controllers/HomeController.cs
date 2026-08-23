using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Diagnostics;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;
using TASHTIP.Models;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using TASHTIP.EF.ViewModel.Production;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Reporting;
using System.Data;

namespace TASHTIP.Controllers
{

    //[Authorize]
    public class HomeController : Controller
    {
         
        private readonly GeneralDBContext DB;
        private readonly IWebHostEnvironment webHostEnvironment;
        [Obsolete]
        private readonly IHostingEnvironment he;

        [Obsolete]
        public HomeController(  GeneralDBContext _generalDBContext ,
                                     IWebHostEnvironment _webHostEnvironment ,
                                     IHostingEnvironment _he)
        {    
            this.DB = _generalDBContext; 
            this.webHostEnvironment = _webHostEnvironment;
            this.he = _he;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Home()
        {
            var result = DB.BussinessGallary.DefaultIfEmpty().ToList();
            var Count_New =  DB.PurchaseRequest.Where(c=>c.Status == "New").Distinct().Count();

            HttpContext.Session.SetInt32("Badge_Number_Of_PR_New", Count_New);
            if (HttpContext.Session.GetInt32("Badge_Number_Of_PR_New") != null)
            {
                ViewBag.Notification = HttpContext.Session.GetInt32("Badge_Number_Of_PR_New"); 
            }
            ViewBag.ServicesType = new SelectList(DB.ServicesType, "Services", "Services");
            if (result != null)
            {
                return View(result);
            }

            
            return View(); 
             
             
        }

        public IActionResult GetCity(string model)
        {

            List<string> result = DB.BussinessGallary.Where(c => c.ServicesName == model).Select(m=>m.City).ToList();

            return Json(result);
        }

        public IActionResult GetEngineer(string model,string city)
        {

            List<string> result = DB.BussinessGallary.Where(c => c.ServicesName == model && c.City == city).Select(m => m.Engineer).ToList();

            return Json(result);
        }

        public IActionResult GetVendor(string model, string city,string engineer)
        {

            List<string> result = DB.BussinessGallary.Where(c => c.ServicesName == model && c.City == city && c.Engineer == engineer).Select(m => m.Vendor).ToList();

            return Json(result);
        }

        public IActionResult GetPrice(string model, string city, string engineer,string vendor)
        {

            List<decimal> result = DB.BussinessGallary.Where(c => c.ServicesName == model && c.City == city && c.Engineer == engineer && c.Vendor == vendor).Select(m => m.Price).ToList();

            return Json(result);
        }

        [HttpGet]
        public IActionResult AllPurchaseRequest()
        {
            //var AllPR = DB.PurchaseRequest.Where (c=>c.Status == "New").ToList();
            var AllPR = (from PR in DB.PurchaseRequest
                         join BG in DB.BussinessGallary
                         on PR.BussinessGallaryID equals BG.ID
                         where (PR.Status == "New")
                         select new ProjectFinishVM()
                         {
                            PurchaseRequest = new List<PurchaseRequest>()
                                                            {
                                                               new   PurchaseRequest()
                                                                {
                                                                  ID_PR =  PR.ID_PR,
                                                                  CutomerName = PR.CutomerName,
                                                                  RequestDate = PR.RequestDate
                                                                }
                                                            }.ToList(),
                              BussinessGallary = new List<BussinessGallary>()
                                                            {
                                                               new   BussinessGallary()
                                                                {
                                                                   City = BG.City,
                                                                   Price = BG.Price,
                                                                   ServicesName = BG.ServicesName
                                                                }
                                                            }.ToList()
                         }).Distinct().ToList();
            if (AllPR != null)
            {
                return View(AllPR);
            }
            return View ();
        }

        [HttpGet]
        public IActionResult DetailsProperty(int? id)
        {
            if (id != null)
            {
                var result = DB.BussinessGallary.Where(c=>c.ID == id).FirstOrDefault();
                if (result != null)
                {
                    return View(result);
                }
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Search(string model, string city, string engineer, string vendor )
        {

            int? id = DB.BussinessGallary.Where(c => c.ServicesName == model && c.City == city && c.Engineer == engineer && c.Vendor == vendor).Select(m => m.ID).FirstOrDefault();
            if (id != null)
            {
                return Json(id);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddProject()
        {
           await InitComponent();
            return View();
        }

        public async Task<Component> InitComponent()
        {
            ViewBag.City = new SelectList(DB.City, "CityName", "CityName");
            ViewBag.ServicesType = new SelectList(DB.ServicesType, "Services", "Services");
            ViewBag.Engineer = new SelectList(DB.SupervisorEnginners, "EngineersName", "EngineersName");
            ViewBag.Section = new SelectList(DB.FilterGallary, "FilterName", "Section");
            return new Component();
        }


        [Obsolete]
        [HttpPost] 
        public async Task<IActionResult> AddProject(string ProjectDate, string  ServicesType, string City ,
            string Engineer , string Vendor, decimal Price , string DetailsUnit ,string DetailsInteriorDesgin ,
            string DetailsQualityFinishing, IFormFile Image, string Section)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = DB.Database.BeginTransaction()) //--Startind transactions
                {

                    try
                    {

                        string uniqueFileName = null;
                        var FileExt1 = Path.GetExtension(Image.FileName);
                        string[] SupportType1 = new string[] { ".jpg", ".png", ".jpeg", ".pdf", ".xls", ".xlsx" };

                        if (SupportType1.Contains(FileExt1))
                        {
                            string UploadFolder = Path.Combine(he.WebRootPath, "ImageFinshProject/Image/");
                            uniqueFileName = Image.FileName.Trim();
                            string FilePath = Path.Combine(UploadFolder, uniqueFileName);
                            await Image.CopyToAsync(new FileStream(FilePath, FileMode.Create));

                        }
                        else
                        {
                            ViewBag.Message = "This Extension File Not Support in System";
                        }

                        // INSERT INTO GRN MAIN
                        var Bussiness = new BussinessGallary ();
                        Bussiness.BussinessDate = ProjectDate.Trim();
                        Bussiness.ServicesName   = ServicesType;
                        Bussiness.City = City; 
                        Bussiness.Engineer = Engineer; 
                        Bussiness.Vendor = !string.IsNullOrEmpty(Vendor) ? Vendor.Trim() : null;
                        Bussiness.Price = Price;
                        Bussiness.DetailsUnit  = !string.IsNullOrEmpty(DetailsUnit) ? DetailsUnit.Trim() : null;
                        Bussiness.InteriorDesign = !string.IsNullOrEmpty(DetailsInteriorDesgin) ? DetailsInteriorDesgin.Trim() : null;
                        Bussiness.FinishingQuality  = !string.IsNullOrEmpty(DetailsQualityFinishing) ? DetailsQualityFinishing.Trim() : null;
                        Bussiness.LinkVideo  = "https://www.youtube.com/embed/FicdWhMgadQ?si=UYh0_FjLPevtkbai";
                        Bussiness.ProfileImage = uniqueFileName.Trim();
                        Bussiness.Filter = !string.IsNullOrEmpty(Section) ? Section.Trim() : null;
                        await DB.BussinessGallary.AddAsync(Bussiness);
                        await DB.SaveChangesAsync();
                         

                        transaction.Commit();
                        return Json(new { success = "Success" });
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback(); 
                        return Json(new { error = "Error" });
                    }

                }

            } 
            return Json(new { error = "Error" });
        }

        [HttpGet]
        public async Task< IActionResult>  PurchaseRequest(int? id)
        {
            if (id != null)
            {
                var result =   DB.BussinessGallary.Where(c => c.ID == id).FirstOrDefault();
                if (result != null)
                {
                    return View(result);
                }

                return View();
            }
            return View();
        }


 

        [HttpPost]
        public async Task<IActionResult> PurchaseRequest(string? RequestDate, int? BussinessGallaryID, string? CustomerName,
            string? Address, string? Job, int? Mobile, int? Age, string? Email, string? Engineer,
            string? Rating, string? DeliveryTerms, string? Notes)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = DB.Database.BeginTransaction()) //--Startind transactions
                {

                    try
                    {
                        var PurchaseRequest = new PurchaseRequest();
                        
                        PurchaseRequest.RequestDate = !string.IsNullOrEmpty(RequestDate) ? RequestDate.Trim() : null;
                        PurchaseRequest.BussinessGallaryID = BussinessGallaryID;
                        PurchaseRequest.CutomerName = !string.IsNullOrEmpty(CustomerName ) ? CustomerName .Trim() : null; 
                        PurchaseRequest.Address  = !string.IsNullOrEmpty(Address ) ? Address .Trim() : null; 
                        PurchaseRequest.Job = !string.IsNullOrEmpty(Job ) ? Job .Trim() : null; 
                        PurchaseRequest.Mobile = Mobile;
                        PurchaseRequest.Age  =  Age ;
                        PurchaseRequest.Email   = !string.IsNullOrEmpty(Email ) ? Email .Trim() : null;
                        PurchaseRequest.Engineer  = !string.IsNullOrEmpty(Engineer ) ? Engineer .Trim() : null;
                        PurchaseRequest.Rating  =  string.Concat(!string.IsNullOrEmpty(Rating ) ? Rating .Trim() : null, " Star");
                        PurchaseRequest.PayTerms  = !string.IsNullOrEmpty(DeliveryTerms ) ? DeliveryTerms .Trim() : null;
                        PurchaseRequest.Notes = !string.IsNullOrEmpty(Notes ) ? Notes .Trim() : null;
                        PurchaseRequest.Status = "New";
                        await DB.PurchaseRequest.AddAsync(PurchaseRequest);
                        await DB.SaveChangesAsync();


                        transaction.Commit();
                        return Json(new { success = "Success" });
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return Json(new { error = "Error" });
                    }

                }

            }
            return Json(new { error = "Error" });
        }

        [HttpGet]
        public IActionResult DetialsPurchaseRequest(int? id)
        {
            //var AllPR = DB.PurchaseRequest.Where (c=>c.Status == "New").ToList();
            var PR_ID = (from PR in DB.PurchaseRequest
                         join BG in DB.BussinessGallary
                         on PR.BussinessGallaryID equals BG.ID
                         where (PR.ID_PR == id)
                         select new DetailsProjectFinishVM()
                         {
                             PurchaseRequest = new PurchaseRequest()
                             {
                                 ID_PR = PR.ID_PR,
                                 CutomerName = PR.CutomerName,
                                 RequestDate = PR.RequestDate,
                                 Address = PR.Address,
                                 Job = PR.Job,
                                 Mobile = PR.Mobile,
                                 Age = PR.Age,
                                 Email = PR.Email,
                                 Engineer = PR.Engineer,
                                 Rating = PR.Rating,
                                 PayTerms = PR.PayTerms,
                                 Notes = PR.Notes
                             },
                             BussinessGallary = new BussinessGallary()
                             {
                                 City = BG.City,
                                 Price = BG.Price,
                                 ServicesName = BG.ServicesName,
                                 Engineer = BG.Engineer,
                                 Vendor = BG.Vendor,
                                 BussinessDate = BG.BussinessDate,
                                 DetailsUnit = BG.DetailsUnit,
                                 InteriorDesign = BG.InteriorDesign,
                                 FinishingQuality = BG.FinishingQuality,
                                 ProfileImage = BG.ProfileImage,
                                 Filter = BG.Filter
                             }
                         }).Distinct().FirstOrDefault();
            if (PR_ID != null)
            {
                return View(PR_ID);
            }
            return View();
        }

        #region Method Print
        public async Task<IActionResult> PrintPR(int? id)
        {
            //============================== Action Report 
            var dt = new DataTable();
            dt = await GetPOInfo(id);
            string mimetype = "";
            int extension = 1;
            var path = $"{this.webHostEnvironment.WebRootPath}\\Reports\\DetailsPurchaseRequest_Report.rdlc";
            Dictionary<string, string> Parameters = new Dictionary<string, string>();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DetailsPurchaseRequest_DataSet", dt);

            var result = localReport.Execute(RenderType.Pdf, extension, Parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }
        [HttpGet]
        public async Task<DataTable> GetPOInfo(int? id)
        {
            var dt = new DataTable();

            dt.Columns.Add("RequestDate");
            dt.Columns.Add("CutomerName");
            dt.Columns.Add("Address");
            dt.Columns.Add("Job");
            dt.Columns.Add("Email");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("PayTerms");
            dt.Columns.Add("Engineer");
            dt.Columns.Add("Rating");
            dt.Columns.Add("ServicesName");
            dt.Columns.Add("City");
            dt.Columns.Add("Vendor");
            dt.Columns.Add("Price");

            var DataResult = (from PR in DB.PurchaseRequest
                              join BG in DB.BussinessGallary
                              on PR.BussinessGallaryID equals BG.ID
                              where (PR.ID_PR == id)
                              select new DetailsProjectFinishVM()
                              {
                                  PurchaseRequest = new PurchaseRequest()
                                  {
                                      CutomerName = PR.CutomerName,
                                      RequestDate = PR.RequestDate,
                                      Address = PR.Address,
                                      Job = PR.Job,
                                      Mobile = PR.Mobile,
                                      Email = PR.Email,
                                      Engineer = PR.Engineer,
                                      Rating = PR.Rating,
                                      PayTerms = PR.PayTerms,
                                  },
                                  BussinessGallary = new BussinessGallary()
                                  {
                                      City = BG.City,
                                      Price = BG.Price,
                                      ServicesName = BG.ServicesName,
                                      Vendor = BG.Vendor,
                                  }
                              });

            //============================= Actio Save In DataBase

            DataRow row;
            foreach (var item in DataResult)
            {
               
                row = dt.NewRow();
                row["RequestDate"] = !string.IsNullOrEmpty(item.PurchaseRequest.RequestDate) ? item.PurchaseRequest.RequestDate : "";
                row["CutomerName"] = !string.IsNullOrEmpty(item.PurchaseRequest.CutomerName) ? item.PurchaseRequest.CutomerName : "";
                row["Address"] = !string.IsNullOrEmpty(item.PurchaseRequest.Address) ? item.PurchaseRequest.Address : ""; 
                row["Job"] = !string.IsNullOrEmpty(item.PurchaseRequest.Job) ? item.PurchaseRequest.Job : "";
                row["Email"] = !string.IsNullOrEmpty(item.PurchaseRequest.Email) ? item.PurchaseRequest.Email : ""; 
                row["Mobile"] = (item.PurchaseRequest.Mobile) ;
                row["PayTerms"] = !string.IsNullOrEmpty(item.PurchaseRequest.PayTerms) ? item.PurchaseRequest.PayTerms : ""; 

               

                row["Engineer"] = !string.IsNullOrEmpty(item.PurchaseRequest.Engineer) ? item.PurchaseRequest.Engineer : ""; 
                row["Rating"] = !string.IsNullOrEmpty(item.PurchaseRequest.Rating) ? item.PurchaseRequest.Rating : "";
                row["ServicesName"] = !string.IsNullOrEmpty(item.BussinessGallary.ServicesName) ? item.BussinessGallary.ServicesName : "";
                row["City"] = !string.IsNullOrEmpty(item.BussinessGallary.City) ? item.BussinessGallary.City : "";
                row["Vendor"] = !string.IsNullOrEmpty(item.BussinessGallary.Vendor) ? item.BussinessGallary.Vendor : "";
                row["Price"] = item.BussinessGallary.Price;


                dt.Rows.Add(row);
            }



            return dt;

        }
        #endregion

    }
}