using CMS.Core;
using CMS.DocumentEngine.Types.GDI;

using GDI.Business.Models;
using GDI.Business.Repositories;
using GDI.Components.Widgets.CommoditiesOrderForm;

using Kentico.PageBuilder.Web.Mvc;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

using XperienceAdapter.Repositories;
[assembly: RegisterWidget("GDI.Widget.CommoditiesOrderForm", typeof(CommoditiesOrderViewComponent), "Commodities Form", typeof(CommoditiesOrderProperties),
   Description = "This is Commodities Order Form", IconClass = "icon-form")]
namespace GDI.Components.Widgets.CommoditiesOrderForm
{
    public class CommoditiesOrderViewComponent : ViewComponent
    {
        /// <summary>
        /// Identifier for the EducationWidget 
        /// </summary>
        public const string IDENTIFIER = "GDI.Widget.CommoditiesOrderFormWidget";
        private readonly IEventLogService _eventLogService;
        private readonly IPageRepository<ProductFormData, ProductCard> _pageRepository;
        private readonly IPageRepository<SubProductItems, Subproduct> _subproductrepository;
        private readonly IContactFormRepository _contactFormRepository;


        public CommoditiesOrderViewComponent(IPageRepository<ProductFormData, ProductCard> pageRepository, IPageRepository<SubProductItems, Subproduct> subproductrepository, IEventLogService eventLogService, IContactFormRepository ContactFormRepository)
        {
            _pageRepository = pageRepository;
            _eventLogService = eventLogService;
            _subproductrepository = subproductrepository;
            _contactFormRepository = ContactFormRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(CommoditiesOrderProperties properties, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (properties != null)
                {
                    string? path = properties.Path?.FirstOrDefault()?.NodeAliasPath;
                    var model = await _pageRepository.GetPagesInCurrentCultureAsync(
                    CancellationToken.None,
                    filter => filter.FilterDuplicates()
                        .Path(path, CMS.DocumentEngine.PathTypeEnum.Children),
                    buildCacheAction:
                        cache => cache
                            .Key($"{nameof(CommoditiesOrderViewComponent)}|{nameof(InvokeAsync)}")
                            .Dependencies((_, builder) => builder
                                .PageType(CMS.DocumentEngine.Types.GDI.ProductCard.CLASS_NAME)
                                .PagePath(path, CMS.DocumentEngine.PathTypeEnum.Children)
                                .PageOrder()));

                    var subitem = await _subproductrepository.GetPagesInCurrentCultureAsync(
                   CancellationToken.None,
                   filter => filter.FilterDuplicates()
                       .Path(path, CMS.DocumentEngine.PathTypeEnum.Children),
                   buildCacheAction:
                       cache => cache
                           .Key($"{nameof(CommoditiesOrderViewComponent)}|{nameof(InvokeAsync)}")
                           .Dependencies((_, builder) =>
                           builder
                               .PageType(CMS.DocumentEngine.Types.GDI.Subproduct.CLASS_NAME)
                               .PagePath(path, CMS.DocumentEngine.PathTypeEnum.Children)
                               .PageOrder()));
                    FormDetails data = new FormDetails()
                    {
                        RecordType = properties?.RecordType,
                        Origin = properties?.Origin,
                    };
                    CommoditiesOrderViewModel cm = new();
                    cm.productData = model.ToList();
                    cm.SubProduct = subitem.ToList();
                    cm.IsVisible = properties?.IsVisible;
                    cm.ProductCardTitle = properties?.ProductCardTitle;
                    cm.ProductFormTitle = properties?.ProductFormTitle;
                    cm.Path = properties?.Path;
                    cm.TopN = properties?.TopN;
                    cm.Getstates = _contactFormRepository.GetStatesData();
                    TempData["CommoditiesOrderForm"] = JsonSerializer.Serialize(data);
                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/CommoditiesOrderForm/_CommoditiesOrderPage.cshtml", cm));
                }
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(CommoditiesOrderViewComponent), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

        }
    }
}
