using CMS.Core;

using GDI.Business.Repositories;
using GDI.Components.Widgets.CommoditiesOrderForm;
using GDI.Components.Widgets.ProductRequestForm;

using Kentico.PageBuilder.Web.Mvc;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

[assembly: RegisterWidget("GDI.Widget.ProductRequestForm", typeof(ProductRequestFormViewComponent), "Product Request Form", typeof(ProductRequestFormProperties),
   Description = "This is Product Request Form Form", IconClass = "icon-form")]
namespace GDI.Components.Widgets.ProductRequestForm
{
    public class ProductRequestFormViewComponent : ViewComponent
    {
        /// <summary>
        /// eventLogService service Registeration
        /// </summary>
        private readonly IEventLogService _eventLogService;
        private readonly IContactFormRepository _contactFormRepository;

        public ProductRequestFormViewComponent(IEventLogService eventLogService, IContactFormRepository ContactFormRepository)
        {
            _eventLogService = eventLogService;
            _contactFormRepository = ContactFormRepository;

        }
        public async Task<IViewComponentResult> InvokeAsync(ProductRequestFormProperties properties, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (properties != null && properties.IsVisible == true)
                {
                    FormDetails data = new FormDetails()
                    {
                        RecordType = properties?.RecordType,
                        Origin = properties?.Origin,
                        Tags = properties?.Tags,
                        ExternalCampaignId = properties?.ExternalCampaignId,
                        Type = properties?.Type,
                    };
                    ProductRequestFormViewModel prm = new()
                    {
                        IsVisible = properties.IsVisible,
                        Title = properties.Title,
                        Subtitle = properties.Subtitle,
                        Getstates = _contactFormRepository.GetStatesData(),
                        GetCountries = _contactFormRepository.GetCountriesData()
                    };
                    TempData["ProductRequestForm"] = JsonSerializer.Serialize(data);
                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/ProductRequestForm/_ProductRequestForm.cshtml", prm));
                }
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(ProductRequestFormViewComponent), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

        }

    }
}
