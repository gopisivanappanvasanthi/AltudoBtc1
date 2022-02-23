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
    public class FeaturedCommentsController : Controller
    {
        // GET: FeaturedComments
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
                                .Where(x => ApplyCommentsBusinessLogic(x.PostedDate))
                                .OrderByDescending(x => x.PostedDate);

            return View("/Views/Altudo/DetailPage/ReviewList.cshtml", reviewLists);
        }

        private DateTime GetDateTimeFromPostedDate(Item item, string fieldName)
        {
            DateField dateField = item.Fields[fieldName];
            return dateField.DateTime;
        }

        private bool ApplyCommentsBusinessLogic(DateTime postedDate)
        {
            var homeItemForSite = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var CommentsSettingItem = homeItemForSite.Axes.GetDescendants()
                                        .FirstOrDefault(x => x.TemplateName == "TripCommentsSettings");

            //var settingsItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{B30F9094-5EAD-4D5D-AB3D-B128A560BF33}"));

            DateField startDate = CommentsSettingItem.Fields["StartDate"];
            DateField endDate = CommentsSettingItem.Fields["EndDate"];
            return ((postedDate > startDate.DateTime) && (postedDate < endDate.DateTime));
        }
    }
}