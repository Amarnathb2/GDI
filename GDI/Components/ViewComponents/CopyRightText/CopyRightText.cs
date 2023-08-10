using CMS.Core;
using CMS.DocumentEngine.Types.GDI;
using CMS.Helpers;
using GDI.Business.Models;
using GDI.Models;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Repositories;
namespace GDI.Components.ViewComponents
{
    public class CopyRightText: ViewComponent
    {
        private  string PagePath = @ResHelper.GetString("GDI.MasterPage.PageConfiguration.MasterPageConfiguration");

        private readonly IPageRepository<PageDataConfiguration, PageConfiguration> _pageConfigurationRepository;
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// injecting the dependence at constructor level
        /// </summary>
        /// <param name="pageConfigurationRepository"></param>
        /// <param name="eventLogService"></param>
        public CopyRightText(IPageRepository<PageDataConfiguration, PageConfiguration> pageConfigurationRepository, IEventLogService eventLogService)
        {
            _pageConfigurationRepository = pageConfigurationRepository;
            _eventLogService = eventLogService;
        }
        /// <summary>
        /// 
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
                        .Key($"{nameof(CopyRightText)}|GDICopyRightText")
                        .Dependencies((_, builder) => builder
                           .PageType(PageConfiguration.CLASS_NAME)
                           .PagePath(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                           .PageOrder()))).FirstOrDefault();
                return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/CopyRightText/_CopyRight.cshtml", new HeaderLogoModel()
              
                {
                    CopyRight = modelData
                }));
            }
            catch (System.Exception ex)
            {
                _eventLogService.LogException(nameof(IViewComponentResult), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
        }

    }
}
