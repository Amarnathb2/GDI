using CMS.Core;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.GDI;
using CMS.Helpers;

using GDI.Controller;
using GDI.Models;

using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using PIMS;

using System.Text.Json;

[assembly: RegisterPageRoute(PageMenu.CLASS_NAME, typeof(SearchResultsController), Path = "/Search")]
namespace GDI.Controller
{
    public class SearchResultsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPageDataContextRetriever pageDataContextRetriever;
        private readonly IPageDataContextInitializer pageDataContextInitializer;
        private readonly IPageRetriever pageRetriever;

        public SearchResultsController(IPageDataContextInitializer pageDataContextInitializer, IPageRetriever pageRetriever)
        {
            this.pageDataContextInitializer = pageDataContextInitializer;
            this.pageRetriever = pageRetriever;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            try
            {
                string SearchResults = string.Empty;
                string SearchFacets = string.Empty;
                string queryString = "";
                var page = pageRetriever.Retrieve<PageMenu>(query => query
                        .Path("/Search", PathTypeEnum.Single)).FirstOrDefault();
                if (page != null)
                {
                    pageDataContextInitializer.Initialize(page);
                }
                if (Request.Query["s"] != string.Empty && Request.Query["Name"] != string.Empty)
                {
                    queryString = Request.Query["s"];
                }
                var queryValue = queryString;
                var filterText = Request.Query["f"];
                if (!string.IsNullOrEmpty(queryValue))
                {
                    string currentURL = RequestContext.CurrentURL;
                    string searchText = queryValue;
                    string filter = string.Empty;
                    if (!(string.IsNullOrEmpty(filterText) || (filterText == "All")))
                    {
                        filter = filterText;
                    }
                    string skip = "0";

                    LOLDF_AzureSearchIndex.StartSearch(searchText, ref SearchResults, ref SearchFacets, filter, currentURL, skip);
                    return View(new AzureSearchResultsViewModel
                    {
                        SearchResults = SearchResults,
                        SearchFacets = SearchFacets
                    });
                }
                return View();
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SearchResultsController), nameof(Index), ex);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("SearchResults/GetResults")]
        public string GetResults(object parameters)
        {
            string searchtext = string.Empty;
            string filter = string.Empty;
            string skip = string.Empty;
            string results = string.Empty;
            Dictionary<string, object>? parameterDict = new Dictionary<string, object>();
            try
            {
                Dictionary<string, object>? _param = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(parameters, string.Empty));
                if (_param != null && _param.ContainsKey("parameters"))
                {
                    parameterDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(_param["parameters"], string.Empty));
                    if (parameterDict != null && parameterDict.Count > 0)
                    {
                        searchtext = parameterDict.ContainsKey("searchtext") ? ValidationHelper.GetString(parameterDict["searchtext"], "") : string.Empty;
                        skip = parameterDict.ContainsKey("skip") ? ValidationHelper.GetString(parameterDict["skip"], "") : string.Empty;
                        filter = parameterDict.ContainsKey("filter") ? ValidationHelper.GetString(parameterDict["filter"], "") : string.Empty;
                    }
                }
                if (filter == "undefined" || filter == null || filter.ToString().ToLower() == "all")
                {
                    filter = string.Empty;
                }
                results = LOLDF_AzureSearchIndex.lazySearch(searchtext, filter, skip).Replace("~", "").ToString().Replace("®", "<sup>&reg;</sup>").Replace("&reg;", "<sup>&reg;</sup>");

            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SearchResultsController), nameof(GetResults), ex);
            }
            return results;
        }

        [HttpGet]
        [Route("/api/gdiproduct/{productID}")]
        public JsonResult GdiProduct(string productID)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };

            Product product = GDI_ProductsInfoProvider.ProviderObject.Get().WhereEquals("GDIProductCode", productID).Select(x => new Product
            {
                GDIProductCode = x.GDIProductCode,
                GDIProductName = x.GDIProductName,
                GDIProductDisplayName = x.GDIProductDisplayName,
                GDIProductDescription = x.GDIProductDescription,
                GDIProductImgSM = x.GDIProductImgSM,
                GDIProductAvail = x.GDIProductAvail,
                GDIProductSampleAvail = x.GDIProductSampleAvail,
                GDIProductClaims = x.GDIProductClaims
            }).First();
            return Json(product, options);
        }

    }
}
