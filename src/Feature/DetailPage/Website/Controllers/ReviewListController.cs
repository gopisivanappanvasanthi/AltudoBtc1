using AltudoBtc1.Feature.DetailPage.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Feature.DetailPage.Controllers
{
    public class ReviewListController : Controller
    {
        // GET: ReviewList
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;

            var reviewLists = contextItem.GetChildren()
                                .Where(x => x.TemplateName == "ReviewComment")
                                .Select(x => new Review
                                {
                                    Name = x.Fields["Name"].Value,
                                    EmailId = x.Fields["EmailId"].Value,
                                    Rating = x.Fields["Rating"].Value,
                                    ReviewComments = x.Fields["ReviewComments"].Value,
                                    PostedDate = GetDateTimeFromPostedDate(x, "PostedDate")
                                })
                                .ToList()
                                .OrderByDescending(x => x.PostedDate);

            return View("/Views/Altudo/DetailPage/ReviewList.cshtml", reviewLists);
        }

        private DateTime GetDateTimeFromPostedDate(Item item, string fieldName)
        {
            DateField dateField = item.Fields[fieldName];
            return dateField.DateTime;    
        }
    }
}