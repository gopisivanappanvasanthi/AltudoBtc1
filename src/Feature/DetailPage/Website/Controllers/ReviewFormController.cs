using AltudoBtc1.Feature.DetailPage.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Feature.DetailPage.Controllers
{
    public class ReviewFormController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Review review = new Review();
            return View("/Views/Altudo/DetailPage/ReviewForm.cshtml", review);
        }
        [HttpPost]
        public ActionResult Index(Review review)
        {
            var contextItem = Sitecore.Context.Item;

            //get my db to create item
            var masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            var webDb = Sitecore.Configuration.Factory.GetDatabase("web");
            //Get my parent item
            var parentItem = masterDb.GetItem(contextItem.ID);
            //get my template
            ID templateID = new ID("{208AA494-841A-4BEE-9527-4C4A415A0046}");
            TemplateItem commentTemplate = masterDb.GetTemplate(templateID);
            //create my item
            using (new SecurityDisabler())
            {
                Item newCommentItem = parentItem.Add(review.Name, commentTemplate);
                newCommentItem.Editing.BeginEdit();
                newCommentItem["Name"] = review.Name;
                newCommentItem["EmailId"] = review.EmailId;
                newCommentItem["Rating"] = review.Rating;
                newCommentItem["ReviewComments"] = review.ReviewComments;
                newCommentItem.Editing.EndEdit();

                //publishing the newly created item

                PublishOptions publishOptions = new PublishOptions
                                                        (masterDb, 
                                                        webDb, 
                                                        PublishMode.SingleItem, 
                                                        Sitecore.Context.Language, 
                                                        DateTime.Now);

                Publisher publisher = new Publisher(publishOptions);
                publisher.Options.RootItem = newCommentItem;
                publisher.Options.Deep = true;
                publisher.Options.Mode = PublishMode.SingleItem;
                publisher.Publish();

            };

            return View("/Views/Altudo/DetailPage/ThankYou.cshtml");
        }
    }
}