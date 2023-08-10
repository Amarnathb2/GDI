using CMS.Core;
using Kentico.PageBuilder.Web.Mvc;
using GDI.Components.Widgets.Location;
using Microsoft.AspNetCore.Mvc;
[assembly: RegisterWidget("GDI.Components.Widgets.Location", typeof(LocationViewComponent), "{$GDI.Components.Widgets.Location$}", typeof(LocationProperties),
    Description = "{$GDI.Components.Widgets.Location$}", IconClass = "icon-arrow-right-top-square")]
namespace GDI.Components.Widgets.Location
{
    public class LocationViewComponent : ViewComponent
    {
        private readonly IEventLogService _eventLogService;

        public LocationViewComponent(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        }

        public async Task<IViewComponentResult> InvokeAsync(LocationProperties properties)
        {
            try
            {
                if (properties != null)
                {
                    LocationViewModel locationModel = new();
                    locationModel.Visible = properties.Visible;
                    locationModel.AddressLine1 = properties.AddressLine1;
                    locationModel.AddressLine2 = properties.AddressLine2;
                    locationModel.City = properties.City;
                    locationModel.State = properties.State;
                    locationModel.Country = properties.Country;
                    locationModel.Email = properties.Email;
                    locationModel.Phone = properties.Phone;
                    locationModel.Latitude = properties.Latitude;
                    locationModel.Longitude = properties.Longitude;
                    locationModel.ZipCode = properties.ZipCode;
                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/Location/_Location.cshtml", locationModel));
                }

                else { return await Task.FromResult<IViewComponentResult>(Content(string.Empty)); }
            }
            catch (Exception ex)
            {

                _eventLogService.LogException(nameof(LocationViewComponent), nameof(InvokeAsync), ex);


                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

            }

        }

    }

}
