using AltudoBtc1.Feature.DetailPage.Models;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Feature.DetailPage.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;
            SiteDetails siteDetails = new SiteDetails
            {
                Title = new HtmlString(FieldRenderer.Render(contextItem, "Title")),
                Description = new HtmlString(FieldRenderer.Render(contextItem, "Description")),
                CardImage = new HtmlString(FieldRenderer.Render(contextItem, "CardImage")),
            };
            return View("/Views/Altudo/DetailPage/SiteDetails.cshtml", siteDetails);
        }
    }
}