using CMS.Core;
using CMS.DocumentEngine.Types.GDI;
using CMS.Helpers;
using GDI.Business.Models;
using GDI.Models;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Repositories;
namespace GDI.Components.ViewComponents
{
    public class ExternalPageLinksView: ViewComponent
    {
        private  string PagePath = @ResHelper.GetString("GDI.MasterPage.PageConfiguration.ExternalLinks");

        private readonly IPageRepository<ExternalLinks, ExternalPageLinks>_ExternalPageRepository;
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// injecting the dependence at constructor level
        /// </summary>
        /// <param name="pageConfigurationRepository"></param>
        /// <param name="eventLogService"></param>
        public ExternalPageLinksView(IPageRepository<ExternalLinks, ExternalPageLinks> ExternalPageLinkRepository, IEventLogService eventLogService)
        {
            _ExternalPageRepository = ExternalPageLinkRepository;
            _eventLogService = eventLogService;
        }
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken? cancellationToken = null)
        {
            try
            {
                var modelData = (await _ExternalPageRepository.GetPagesInCurrentCultureAsync(
                CancellationToken.None,
                filter => filter
                    .Path(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                    .Published(),
                buildCacheAction:
                    cache => cache
                        .Key($"{nameof(ExternalPageLinksView)}|GdiExternalPageLinks")
                        .Dependencies((_, builder) => builder
                           .PageType(ExternalPageLinks.CLASS_NAME)
                           .PagePath(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                           .PageOrder()))).ToList();
                return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/ExternalPageLinks/_ExternalPageLinks.cshtml", new ExternalLinksViewModel()
                {
                    ExternalPageLinks = modelData.ToList(),
                })) ;
            }
            catch (System.Exception ex)
            {
                _eventLogService.LogException(nameof(IViewComponentResult), nameof(InvokeAsync), ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
        }


    }
}
