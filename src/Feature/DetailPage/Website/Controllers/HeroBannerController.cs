using AltudoBtc1.Feature.DetailPage.Models;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Feature.DetailPage.Controllers
{
    public class HeroBannerController : Controller
    {
        // GET: HeroBanner
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;

            HeroBanner heroBanner = new HeroBanner
            {
                HeroImage = new HtmlString(FieldRenderer.Render(contextItem, "HeroImage"))
            };

            return View("/Views/Altudo/DetailPage/HeroBanner.cshtml",heroBanner);
        }
    }
}