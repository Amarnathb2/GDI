using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.GDI;
using CMS.Helpers;
using GDI.Business.Models;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace GDI.Components.ViewComponents
{
    public class CustomMethods : ViewComponent
    {
        private readonly IPageRetriever _pageRetriever;
        private readonly IEventLogService _eventLogService;
        public CustomMethods(IPageRetriever pageRetriever, IEventLogService eventLogService)
        {
            _pageRetriever = pageRetriever;
            _eventLogService = eventLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken? cancellationToken = null)
        {

            try
            {

                var url = RequestContext.CurrentURL;
                if (url == "/")
                {
                    url = "/Home";
                }
                else if (url == "/specialty-powders/")
                {
                    url = "/SEASONINGS-INGREDIENTS";
                }
                else if (url == "/specialty-powders/cheese-powders/")
                {
                    url = "/SEASONINGS-INGREDIENTS/Cheese-Powders";
                }
                else if (url == "/specialty-powders/seasoning-blends/")
                {
                    url = "/SEASONINGS-INGREDIENTS/Seasoning-Blends";
                }

                else if (url == "/specialty-powders/dairy-powders/")
                {
                    url = "/SEASONINGS-INGREDIENTS/Dairy-Powders";
                }
                else if (url.Contains("/search"))
                {
                    url = "/search";
                }
                else
                {
                    url = url.TrimEnd('/');
                }
                TreeNode page = _pageRetriever.Retrieve<PageMenu>(query => query
                        .Path(url, PathTypeEnum.Single)
                        .TopN(1))
                        .FirstOrDefault();
                bool isProduction = SettingsKeyInfoProvider.GetBoolValue("GDI_IsProduction");
                if (page != null)
                {
                    var nodeAlias = page.NodeAliasPath.ToString();
                    string searchTag = string.Empty;
                    if (page.NodeName.ToLowerInvariant() == "search")
                    {
                        searchTag = "'event':{'name':'search'}";
                    }

                    var serverName = "";

                    if (serverName == "")
                    {
                        serverName = "gdi-prod";
                    }
                    string[] pathArray = url.Split('/').Where(c => c != "").ToArray();

                    DateTime lastPublished = ValidationHelper.GetDateTime(page.DocumentLastPublished, DateTime.Now).Year == 1 ? page.DocumentModifiedWhen.Year == 1 ? DateTime.Now : page.DocumentModifiedWhen : ValidationHelper.GetDateTime(page.DocumentLastPublished, DateTime.Now);
                    int age = Math.Abs(DateTime.Now.Month - lastPublished.Month + 12 * (DateTime.Now.Year - lastPublished.Year));
                    string pageTag = string.Empty;
                    string level1 = string.Empty;
                    string level2 = string.Empty;
                    string level3 = string.Empty;
                    string type = string.Empty;
                    string detail = string.Empty;
                    string publishedDate = string.Empty;
                    int level = pathArray.Count();
                    switch (level)
                    {
                        case 0:
                            pageTag = string.Empty;
                            break;
                        case 1:
                            level1 = page.NodeName.ToLowerInvariant() == "404" ? "error" : page.NodeName;
                            type = ValidationHelper.GetString(level1, string.Empty);
                            publishedDate = lastPublished.Year + "" + lastPublished.Month.ToString("D2");
                            if (page.NodeName == "Home")
                            {
                                pageTag = "'level1':'home','type':'homepage','server':'gdi-prod'";
                            }
                            else
                            {
                                if (url.Contains("natural-cheese") || url.Contains("butter-butterfats") || url.Contains("milk-powders") || url.Contains("whey-powders"))
                                {
                                    pageTag = "'level1':'products','level2':'bulk-dairy-products','level3':'" + pathArray[0].Replace("butter-butterfats", "butter").ToLowerInvariant() + "','type':'product-cat','server':'" + "gdi-prod" + "'";

                                }
                                else if (url.Contains("our-story") || url.Contains("quality") || url.Contains("r-and-d-process"))
                                {
                                    pageTag = "'level1':'about-us','detail':'" + pathArray[0].Replace("r-and-d-process", "r-d-process") + "','type':'about-us-detail','server':'" + "gdi-prod" + "'";
                                }
                                else if (url.Contains("commodities-order") || url.Contains("specialty-powders-request-form"))
                                {
                                    pageTag = "'level1':'orders','detail':'" + pathArray[0].Replace("specialty-powders-request-form", "speciality-order").Replace("commodities-order", "commodity-order") + "','type':'order-request','server':'" + "gdi-prod" + "'";
                                }
                                else if (url == "/SEASONINGS-INGREDIENTS")
                                {
                                    pageTag = "'level1':'products','level2':'specialty-powders','type':'product-cat','server':'gdi-prod'";
                                }
                                else if (url.Contains("contact-us"))
                                {
                                    pageTag = "'level1':'customer-service','type':'" + pathArray[0] + "','server':'gdi-prod'";
                                }
                                else if (url.Contains("product-request"))
                                {
                                    pageTag = "'level1':'product-request','type':'" + "lol-gdiproductrequest" + "','server':'gdi-prod'";
                                } 
                                else if (url.Contains("search"))
                                {
                                    pageTag = "'level1':'search','type':'search','server':'gdi-prod'";
                                  
                                }
                                else
                                {
                                    pageTag = "'level1':'products','level2':'" + pathArray[0].ToLowerInvariant() + "','type':'product-cat','server':'gdi-prod'";
                                }
                            }

                            break;
                        case 2:
                            level1 = pathArray[0];
                            level2 = pathArray[1];
                            type = ValidationHelper.GetString(level1 + "-" + level2, string.Empty);
                            publishedDate = lastPublished.Year + "" + lastPublished.Month.ToString("D2");
                            if (url.Contains("contact-us"))
                            {
                                pageTag = "'level1':'customer-service','type':'" + pathArray[0] + "','server':'gdi-prod'";
                            }
                            else if (url.Contains("product-request"))
                            {
                                pageTag = "'level1':'product-request','type':'" + "lol-gdiproductrequest" + "','server':'gdi-prod'";
                            }
                            else if (url == "/SEASONINGS-INGREDIENTS/Cheese-Powders" || url == "/SEASONINGS-INGREDIENTS/Seasoning-Blends" || url == "/SEASONINGS-INGREDIENTS/Dairy-Powders/")
                            {
                                pageTag = "'level1':'products','level2':'specialty-powders' ,'level3':'" + pathArray[1].ToLowerInvariant() + "','type':'product-cat','server':'gdi-prod'";
                            }
                            else
                            {
                                pageTag = "'level1':'products','level2':'" + pathArray[0].ToLowerInvariant() + "','level3':'" + pathArray[1].ToLowerInvariant() + "','type':'product-cat','server':'gdi-prod'";
                            }
                            break;
                        default:
                            level1 = pathArray[0];
                            level2 = pathArray[pathArray.Length - 1];

                            type = ValidationHelper.GetString(level1 + "-" + level2, string.Empty);
                            publishedDate = lastPublished.Year + "" + lastPublished.Month.ToString("D2");
                            pageTag = type;
                            break;

                    }
                    string str = pageTag;
                    return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/CustomMethods.cshtml", new CustomMethodsModel()
                    {
                        loldata = str,
                        isProduction = isProduction
                    }));
                }
                else
                {
                    string str = "";
                    return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/CustomMethods.cshtml", new CustomMethodsModel()
                    {
                        loldata = str,
                        isProduction = isProduction
                    }));
                }
            }

            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(IViewComponentResult), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }

        }
    }
}
