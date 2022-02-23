using AltudoBtc1.Feature.Common.Models;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltudoBtc1.Feature.Common.Controllers
{
    public class BreadcrumbController : Controller
    {
        // GET: Breadcrumb
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;

            var homeItemPath = Sitecore.Context.Site.StartPath;

            var homeItem = Sitecore.Context.Database.GetItem(homeItemPath);

            var breadcrumbList = contextItem.Axes.GetAncestors()
                                    .Where(x => x.Axes.IsDescendantOf(homeItem))
                                    .Select(x => new BreadcrumbNav
                                    {
                                        NavTitle = x.Name,
                                        NavUrl = LinkManager.GetItemUrl(x, new ItemUrlBuilderOptions { LowercaseUrls = true }),
                                    })
                                    .ToList()
                                    .Concat(new List<BreadcrumbNav>() 
                                        { 
                                            new BreadcrumbNav 
                                            { 
                                                NavTitle = contextItem.Name, 
                                                NavUrl = LinkManager.GetItemUrl(contextItem, new ItemUrlBuilderOptions { LowercaseUrls = true }) 
                                            } 
                                        }
                                    );

            return View("/Views/Altudo/Common/Breadcrumb.cshtml", breadcrumbList);
        }
    } 
}