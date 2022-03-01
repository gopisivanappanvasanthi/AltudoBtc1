using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltudoBtc1.Feature.Common.Resolvers
{
    public class MultilistResolver : RenderingContentsResolver
    {
        private List<Item> items = new List<Item>();
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Assert.ArgumentNotNull(rendering, nameof(rendering));
            Assert.ArgumentNotNull(renderingConfig, nameof(renderingConfig));

            Item ds = GetContextItem(rendering, renderingConfig);

            var multilistitemfieldid = new ID("");

            MultilistField multilistField = ds.Fields[multilistitemfieldid];

            JObject jobject = new JObject
            {
                ["multilistitems"] = (JToken)new JArray()
            };
            jobject["multilistitems"] = ProcessItems(multilistField.GetItems(), rendering, renderingConfig);

            return jobject;
        }
    }
}