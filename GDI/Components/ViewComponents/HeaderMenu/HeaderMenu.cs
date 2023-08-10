using CMS.Core;
using GDI.Business.Models;
using GDI.Models;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Extensions;
using XperienceAdapter.Repositories;
namespace GDI.Components.ViewComponents
{
    public class HeaderMenu: ViewComponent
    {

        private readonly INavigationRepository<PageMenuModel> _navigationRepository;
        private readonly IEventLogService _eventLogService;

        public HeaderMenu(INavigationRepository<PageMenuModel> navigationRepository, IEventLogService eventLogService)
        {
            _navigationRepository = navigationRepository;
            _eventLogService = eventLogService;
        }
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            try
            {
                var currentCulture = Thread.CurrentThread.CurrentUICulture.ToSiteCulture();
                var navigation = await _navigationRepository.GetNavigationAsync(currentCulture);
                return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/HeaderMenu/_HeaderMenu.cshtml", new MenuView()
                {
                    HeaderMenu = navigation
                }));
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(HeaderMenu), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
        }
    }
}
