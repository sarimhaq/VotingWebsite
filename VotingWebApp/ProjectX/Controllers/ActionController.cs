using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectX.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ProjectX.Controllers
{

    [Authorize]
    public class ActionController : Controller
    {
 
        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: Action
       
        
       public ActionResult Comments(string CompanyName)
        {
            var Comment = (from list in db.VotingDatas
                           where list.CompanyName == CompanyName
                           select list.Comment).ToList();
            ViewBag.Comments = Comment;
            ViewBag.Company = CompanyName;
            return View();
        }

        public ActionResult PostSummary()
        {
            var VoteList = db.VotingDatas.ToList();
            var CompanyName = db.CompanyNames.ToList();
            List<ResultSummary> ItemList = new List<ResultSummary>();
            foreach(var name in CompanyName)
            {
                
                var CompanyScore =  (from objects in VoteList
                             where objects.CompanyName == name.CompanyName
                             select objects).ToList();
                decimal TotalClarity = 0;
                decimal TotalContent = 0;
                decimal TotalDelivery = 0;
                decimal TotalScores = 0;
                if (CompanyScore.Count != 0)
                {
                    foreach (var x in CompanyScore)
                    {
                        TotalClarity += decimal.Parse(x.Clarity); 
                        TotalContent += decimal.Parse(x.Content);
                        TotalDelivery += decimal.Parse(x.Delivery);
                    }
                    int NumberTotal = CompanyScore.Count;



                    TotalDelivery = ((TotalDelivery) / (4 * NumberTotal)) * 100;
                    TotalContent = ((TotalContent) / (4 * NumberTotal)) * 100;
                    TotalClarity = ((TotalClarity) / (4 * NumberTotal)) * 100;

                    TotalDelivery = Math.Truncate(TotalDelivery * 1m);
                    TotalContent = Math.Truncate(TotalContent * 1m);
                    TotalClarity = Math.Truncate(TotalClarity * 1m);

                    TotalScores = ((TotalDelivery + TotalContent + TotalClarity) / 300) * 100;
                    TotalScores = Math.Truncate(TotalScores * 1m);
                    ItemList.Add(new ResultSummary() { CompanyName = name.CompanyName, ClarityScore = TotalClarity.ToString(), ContentScore = TotalContent.ToString(), DeliveryScore = TotalDelivery.ToString(), TotalScore = TotalScores.ToString(), IntTotal = (int)TotalScores });
                }
            }
            var ToPostList = ItemList.OrderByDescending(i => i.IntTotal);
            
            return View(ToPostList);
        }

       

        public List<String> PopulateCompanyName()
        {
            List<string> Names = new List<string>();

            Names = db.CompanyNames.Select(a => a.CompanyName).ToList();
            Names.Sort();
            return Names;
        }
        public ActionResult VotingData()
        {

            

            ViewBag.CompanyNames = PopulateCompanyName();


            return View();
        }


         [HttpPost]
        public ActionResult VotingData(VotingData VotingData, string CompanyName)
        {
             if(CompanyName=="ignore")
             {
                 ViewBag.CompanyNames = PopulateCompanyName();
                 return View();
             }
            string userid = User.Identity.GetUserId();
            VotingData.UserProfileId = userid;
            if (ModelState.IsValid) {

                var check = (from list in db.VotingDatas
                             where list.CompanyName == CompanyName && list.UserProfileId == userid
                             select list).FirstOrDefault();
              
                if (check == null)
                {
                    db.VotingDatas.Add(VotingData);
                }
                else
                {
                    check.Clarity = VotingData.Clarity;
                    check.Content = VotingData.Content;
                    check.Delivery = VotingData.Delivery;
                    check.Comment = VotingData.Comment;
                    
                }
                db.SaveChanges();
                //return PartialView("_RefreshedCompany");
                ViewBag.CompanyNames = PopulateCompanyName();
                return View("Confirmation");
            }

            ViewBag.CompanyNames = PopulateCompanyName();
            return View();
            //return View();
         
           
        }

        public ActionResult CreateCompany()
         {
             return View();
         }


         [HttpPost]

        public ActionResult CreateCompany(CompanyNames Company)
        {
            if (ModelState.IsValid)
            {
                db.CompanyNames.Add(Company);
                db.SaveChanges();
            }
            return View();
        }

         public ActionResult DeleteFirm()
         {
             var DeleteCompany = db.CompanyNames.ToList();
             var itemToRemove = DeleteCompany.Single(r => r.CompanyName == "Reno");
             DeleteCompany.Remove(itemToRemove);
             var itemToRemovee = DeleteCompany.Single(r => r.CompanyName == "RenoSeer3D");
             DeleteCompany.Remove(itemToRemovee);
             db.SaveChanges();
             return View();
         }
       
    }
}