using CMS.Core;
using CMS.DocumentEngine.Types.GDI;
using CMS.Helpers;
using GDI.Business.Models;
using GDI.Models;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Repositories;
namespace GDI.Components.ViewComponents
{
    public class SocialiconsView: ViewComponent
    {
        private string PagePath = @ResHelper.GetString("GDI.MasterPage.PageConfiguration.SocialMedia");
        private readonly IPageRepository<SocialIcons, SocialMedia> _SocialIconRepository;
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// injecting the dependence at constructor level
        /// </summary>
        /// <param name="pageConfigurationRepository"></param>
        /// <param name="eventLogService"></param>
        public SocialiconsView(IPageRepository<SocialIcons, SocialMedia> SocialIconRepository, IEventLogService eventLogService)
        {
            _SocialIconRepository = SocialIconRepository;
            _eventLogService = eventLogService;
        }
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken? cancellationToken = null)
        {
            try
            {
                var modelData = (await _SocialIconRepository.GetPagesInCurrentCultureAsync(
                CancellationToken.None,
                filter => filter
                    .Path(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                    .Published(),
                buildCacheAction:
                    cache => cache
                        .Key($"{nameof(SocialiconsView)}|SocialMedia")
                        .Dependencies((_, builder) => builder
                           .PageType(SocialMedia.CLASS_NAME)
                           .PagePath(PagePath, CMS.DocumentEngine.PathTypeEnum.Children)
                           .PageOrder()))).ToList();
                return await Task.FromResult((IViewComponentResult)View("~/Components/ViewComponents/SocialMedia/_SocialMedia.cshtml", new SocialMediaViewModel()
                {
                    Socialicons = modelData.ToList(),
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
