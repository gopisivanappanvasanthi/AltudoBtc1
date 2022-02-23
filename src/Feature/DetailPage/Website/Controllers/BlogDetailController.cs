using AltudoBtc1.Feature.DetailPage.Models;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Feature.DetailPage.Controllers
{
    public class BlogDetailController : Controller
    {
        // GET: BlogDetail
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;

            BlogDetail blogDetail = new BlogDetail()
            {
                DetailedBlog = new HtmlString(FieldRenderer.Render(contextItem, "DetailedBlog"))
            };

            return View("/Views/Altudo/DetailPage/DetailedBlog.cshtml", blogDetail);
        }
    }
}