using CMS.Core;

using GDI.Business.Repositories;
using GDI.Components.Widgets.CommoditiesOrderForm;
using GDI.Components.Widgets.SpecialtyPowderRequestForm;

using Kentico.PageBuilder.Web.Mvc;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

[assembly: RegisterWidget("SpecailtyPowderRequestForm", typeof(SpecialtyPowderRequestFormViewComponent), "Specialty Powder Request Form", typeof(SpecialtyPowderRequestFormProperties),
   Description = "This is Product Request Form Form", IconClass = "icon-form")]

namespace GDI.Components.Widgets.SpecialtyPowderRequestForm
{
    public class SpecialtyPowderRequestFormViewComponent : ViewComponent
    {
        /// <summary>
        /// eventLogService service Registeration
        /// </summary>
        private readonly IEventLogService _eventLogService;
        private readonly IContactFormRepository _contactFormRepository;

        public SpecialtyPowderRequestFormViewComponent(IEventLogService eventLogService, IContactFormRepository ContactFormRepository)
        {
            _eventLogService = eventLogService;
            _contactFormRepository = ContactFormRepository;

        }
        public async Task<IViewComponentResult> InvokeAsync(SpecialtyPowderRequestFormProperties properties, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (properties != null && properties.IsVisible == true)
                {
                    FormDetails data = new FormDetails()
                    {
                        RecordType = properties?.RecordType,
                        Origin = properties?.Origin,
                    };
                    SpecialtyPowderRequestFormViewModel prm = new()
                    {
                        IsVisible = properties.IsVisible,
                        Title = properties.Title,
                        Subtitle = properties.Subtitle,
                        Getstates = _contactFormRepository.GetStatesData(),
                        GetCountries = _contactFormRepository.GetCountriesData()
                    };
                    TempData["SpecialityProductRequestForm"] = JsonSerializer.Serialize(data);
                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/SpecialtyPowderRequestForm/_SpecailtyPowderRequestForm.cshtml", prm));
                }
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(SpecialtyPowderRequestFormViewComponent), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }
    }
}
