using CMS.Core;

using GDI.Business.Repositories;
using GDI.Components.Widgets.CommoditiesOrderForm;
using GDI.Components.Widgets.GeneralContactForm;

using Kentico.PageBuilder.Web.Mvc;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

[assembly: RegisterWidget("GDI.Widget.GeneralContactForm", typeof(GeneralContactFormViewComponent), "General Form", typeof(GenralContactFormProperties),
   Description = "This is General Contact Form", IconClass = "icon-form")]

namespace GDI.Components.Widgets.GeneralContactForm;
public class GeneralContactFormViewComponent : ViewComponent
{
    /// <summary>
    /// eventLogService service Registeration
    /// </summary>
    private readonly IEventLogService _eventLogService;
    private readonly IContactFormRepository _contactFormRepository;

    public GeneralContactFormViewComponent(IEventLogService eventLogService, IContactFormRepository ContactFormRepository)
    {
        _eventLogService = eventLogService;
        _contactFormRepository = ContactFormRepository;

    }
    public async Task<IViewComponentResult> InvokeAsync(GenralContactFormProperties properties, CancellationToken? cancellationToken = null)
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
                GeneralContactFormViewModel gcm = new()
                {
                    IsVisible = properties.IsVisible,
                    Title = properties.Title,
                    Subtitle = properties.Subtitle,
                    Getstates = _contactFormRepository.GetStatesData(),
                    GetCountries = _contactFormRepository.GetCountriesData()

                };
                TempData["ContactUsForm"] = JsonSerializer.Serialize(data);
                return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/GeneralContactForm/_GeneralContactForm.cshtml", gcm));
            }
        }
        catch (Exception ex)
        {
            _eventLogService.LogException(nameof(GeneralContactFormViewComponent), nameof(InvokeAsync), ex);
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }
        return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

    }
}

