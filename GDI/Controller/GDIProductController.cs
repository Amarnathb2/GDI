using CMS.Core;
using CMS.Helpers;

using GDI.Business.Models;

using Microsoft.AspNetCore.Mvc;

using PIMS;

using System.Data;
using System.Text.Json;

namespace GDI.Controller
{

    //[Route("api/[controller]")]
    [ApiController]
    public class GDIProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEventLogService _eventLogService;
        //private readonly IResult _result;
        public GDIProductController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }
        [HttpGet]
        [Route("api/FetchFilterJson")]
        public JsonResult GetProductFilter(string param = "")
        {
            FilterItem[] objProductFilter;
            List<FilterItem> lstFilters = new List<FilterItem>();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };

            //input the valid category names, else will return status 204 No Content Found
            if (!string.IsNullOrEmpty(param))
            {
                switch (param.ToLower())
                {
                    case "cheese powders":
                        lstFilters = CheesePowders();
                        break;
                    case "dairy powders":
                        lstFilters = DairyPowders();
                        break;
                    case "seasoning blends":
                        lstFilters = SeasoningBlends();
                        break;
                }

            }
            objProductFilter = lstFilters.ToArray();
            return Json(objProductFilter, options);


        }

        [NonAction]
        public List<FilterItem> CheesePowders()
        {
            List<FilterItem> lstFilters = new List<FilterItem>();

            //update the order to show the menu in different order on page left filter

            lstFilters.Add(GetFilterItem("Cheese Type", "CheesePowderType", "PIMSGDI.CheesePowderType"));

            lstFilters.Add(GetFilterItem("Certifications & Claims", "GDIProductClaims", "PIMSGDI.GDIProductClaims"));

            lstFilters.Add(GetFilterItem("Varietal Flavor", "CheesePowderVarietalFlavor", "PIMSGDI.CheesePowderVarietalFlavor"));

            lstFilters.Add(GetFilterItem("Color", "CheesePowderColor", "PIMSGDI.CheesePowderColor"));

            lstFilters.Add(GetFilterItem("Cheddar Intensity", "CheesePowderCheddarIntensity", "PIMSGDI.CheesePowderCheddarIntensity"));

            lstFilters.Add(GetFilterItem("Flavor Tones", "CheesePowderFavorTones", "PIMSGDI.CheesePowderFavorTones"));

            lstFilters.Add(GetFilterItem("Taste/Mouthfeel", "CheesePowderTaste", "PIMSGDI.CheesePowderTaste"));

            lstFilters.Add(GetFilterItem("Availability", "GDIProductAvail", "PIMSGDI.GDIProductAvail"));

            return lstFilters;
        }
        [NonAction]
        public List<FilterItem> DairyPowders()
        {

            List<FilterItem> lstFilters = new List<FilterItem>();

            //update the order to show the menu in different order on page left filter

            lstFilters.Add(GetFilterItem("Dairy Powder Type", "DairyPowderType", "PIMSGDI.DairyPowderType"));

            lstFilters.Add(GetFilterItem("Certifications & Claims", "GDIProductClaims", "PIMSGDI.GDIProductClaims"));

            lstFilters.Add(GetFilterItem("Dairy Intensity", "DairyPowderIntensity", "PIMSGDI.DairyPowderIntensity"));

            lstFilters.Add(GetFilterItem("Sweetness", "DairyPowderSweetness", "PIMSGDI.DairyPowderSweetness"));

            lstFilters.Add(GetFilterItem("Flavors", "DairyPowderFlavors", "PIMSGDI.DairyPowderFlavors"));

            lstFilters.Add(GetFilterItem("Taste/Finish", "DairyPowderTate", "PIMSGDI.DairyPowderTate"));

            lstFilters.Add(GetFilterItem("Availability", "GDIProductAvail", "PIMSGDI.GDIProductAvail"));

            return lstFilters;
        }

        [NonAction]
        public List<FilterItem> SeasoningBlends()
        {
            List<FilterItem> lstFilters = new List<FilterItem>();

            //update the order to show the menu in different order on page left filter

            lstFilters.Add(GetFilterItem("Seasoning Type", "SeasoningBlendType", "PIMSGDI.SeasoningBlendType"));

            lstFilters.Add(GetFilterItem("Certifications & Claims", "GDIProductClaims", "PIMSGDI.GDIProductClaims"));

            lstFilters.Add(GetFilterItem("Heat Levels", "SeasoningBlendHeatLevel", "PIMSGDI.SeasoningBlendHeatLevel"));

            lstFilters.Add(GetFilterItem("Flavors", "SeasoningBlendFlavor", "PIMSGDI.SeasoningBlendFlavor"));

            lstFilters.Add(GetFilterItem("Spicy Flavors", "SeasoningBlendSpicyFlavor", "PIMSGDI.SeasoningBlendSpicyFlavor"));

            lstFilters.Add(GetFilterItem("Smoky Tones", "SeasoningBlendSmokyTone", "PIMSGDI.SeasoningBlendSmokyTone"));

            lstFilters.Add(GetFilterItem("Taste", "SeasoningBlendTaste", "PIMSGDI.SeasoningBlendTaste"));

            lstFilters.Add(GetFilterItem("Availability", "GDIProductAvail", "PIMSGDI.GDIProductAvail"));

            return lstFilters;
        }

        [NonAction]
        public static FilterItem GetFilterItem(string strFilterName, string strFieldName, string strResourceKey)
        {
            string[] filterOption;
            if (string.IsNullOrEmpty(strResourceKey))
            {
                filterOption = ValidationHelper.GetString(strFieldName, "").Split('\n').ToArray<string>();
            }
            else
            {
                filterOption = ValidationHelper.GetString(ResHelper.GetString(strResourceKey), "").Split('\n').ToArray<string>();
            }
            FilterItem objFilterItem = new FilterItem()
            {
                FilterFiledName = strFieldName,
                FilterName = strFilterName,
                FilterOptions = filterOption
            };

            return objFilterItem;
        }
        [HttpGet]
        [Route("api/FetchProductList")]
        public JsonResult GetProductsByCategory(string param = "")
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };
            ProductDto[] products = GDI_ProductsInfoProvider.ProviderObject.Get()
               .Source(s => s.Join<GDI_CategoryInfo>("GDICategoryID", "GDI_CategoryID"))
             .Where("ISNULL(PIMS_GDI_Products.IsActive,0) = 1")
             .And().Where("GDICategoryName = '" + param + "'").OrderBy("GDIProductItemOrder").Select(x => new ProductDto
             {
                 GDICategoryName = param,
                 GDI_ProductsID = x.GDI_ProductsID,
                 GDIProductName = x.GDIProductName,
                 GDIProductDescription = x.GDIProductDescription,
                 GDIProductDisplayName = x.GDIProductDisplayName,
                 //MetaTitle=x.MetaTitle,
                 //MetaDescription=x.MetaDescription,
                 GDIProductImgXL = x.GDIProductImgXL,
                 GDIProductImgL = x.GDIProductImgL,
                 GDIProductImgM = x.GDIProductImgM,
                 GDIProductImgSM = x.GDIProductImgSM,
                 GDIProductImgAltTxt = x.GDIProductImgAltTxt,
                 GDIProductItemOrder = x.GDIProductItemOrder,
                 GDIProductAvail = x.GDIProductAvail,
                 GDIProductSampleAvail = x.GDIProductSampleAvail,
                 GDIProductClaims = x.GDIProductClaims,
                 DairyPowderType = x.DairyPowderType,
                 DairyPowderIntensity = x.DairyPowderIntensity,
                 DairyPowderSweetness = x.DairyPowderSweetness,
                 DairyPowderFlavors = x.DairyPowderFlavors,
                 DairyPowderTate = x.DairyPowderTate,
                 CheesePowderType = x.CheesePowderType,
                 CheesePowderColor = x.CheesePowderColor,
                 SeasoningBlendType = x.SeasoningBlendType,
                 SeasoningBlendHeatLevel = x.SeasoningBlendHeatLevel,
                 SeasoningBlendSmokyTone = x.SeasoningBlendSmokyTone,
                 GDIProductCode = x.GDIProductCode,
                 SeasoningBlendFlavor = x.SeasoningBlendFlavor,
                 SeasoningBlendSpicyFlavor = x.SeasoningBlendSpicyFlavor,
                 SeasoningBlendTaste = x.SeasoningBlendTaste,
                 CheesePowderVarietalFlavor = x.CheesePowderVarietalFlavor,
                 CheesePowderCheddarIntensity = x.CheesePowderCheddarIntensity,
                 CheesePowderFavorTones = x.CheesePowderFavorTones,
                 CheesePowderTaste = x.CheesePowderTaste
             }).ToArray();

            return Json(products, options);

        }
    }
    public class ProductFilter
    {
        public FilterItem[]? Filters;
    }
    public class FilterItem
    {
        public string? FilterFiledName { get; set; }
        public string? FilterName { get; set; }
        public string[]? FilterOptions { get; set; }
    }
}

