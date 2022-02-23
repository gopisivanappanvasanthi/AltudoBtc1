using AltudoBtc1.Project.Altudo.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Project.Altudo.Controllers
{
    public class ArticleAuthorsController : Controller
    {
        // GET: ArticleAuthors
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;
            List<Author> authors = new List<Author>();
            MultilistField authoredby = contextItem.Fields["AuthoredBy"];

            authors = authoredby.GetItems()
                        .Select(x => new Author
                        {
                            Name = x.Fields["TraineeName"].Value,
                            Url = LinkManager.GetItemUrl(x)
                        }).ToList();

            return View("/Views/Altudo/ArticleAuthors.cshtml", authors);
        }
    }
}