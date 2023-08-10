using CMS.Core;
using CMS.DocumentEngine.Types.GDI;
using CMS.Helpers;
using GDI.Business.Models;
using GDI.Models;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Repositories;
namespace GDI.Components.ViewComponents;
public class HeaderLogo: ViewComponent
{
    private  string PagePath = @ResHelper.GetString("GDI.MasterPage.PageConfiguration.MasterPageConfiguration");
    private readonly IPageRepository<PageDataConfiguration, PageConfiguration> _pageConfigurationRepository;
    private readonly IEventLogService _eventLogService;
    /// <summary>
    /// injecting the dependence at constructor level
    /// </summary>
    /// <param name="pageConfigurationRepository"></param>
    /// <param name="eventLogService"></param>
    public HeaderLogo(IPageRepository<PageDataConfiguration, PageConfiguration> pageConfigurationRepository, IEventLogService eventLogService)
    {
        _pageConfigurationRepository = pageConfigurationRepository;
        _eventLogService = eventLogService;
    }
    /// <summary>
    /// Getting the data from the _pageConfigurationRepository repository.
    /// Binding the data to the model
    /// Return the view
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken? cancellationToken = null)
    {
        try
        {
            var modelData = (await _pageConfigurationRepository.GetPagesInCurrentCultureAsync(
            CancellationToken.None,
            filter => filter
                .Path(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                .Published(),
            buildCacheAction:
                cache => cache
                    .Key($"{nameof(HeaderLogo)}|GdiHeaderLogo")
                    .Dependencies((_, builder) => builder
                       .PageType(PageConfiguration.CLASS_NAME)
                       .PagePath(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                       .PageOrder()))).FirstOrDefault();
            return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/HeaderLogo/_HeaderLogo.cshtml", new HeaderLogoModel()
            {
                HeaderPage = modelData
            }));
        }
        catch (System.Exception ex)
        {
            _eventLogService.LogException(nameof(IViewComponentResult), nameof(InvokeAsync), ex);
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }
    }
}






