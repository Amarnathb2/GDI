using CMS.Core;
using CMS.Helpers;
using GDI.Business.Models;
using GDI.Components.Widgets.Accordion;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;
[assembly: RegisterWidget(AccordionViewComponent.IDENTIFIER, typeof(AccordionViewComponent), "{$GDI.Widget.Accordion.Name$}",
    typeof(AccordionProperties), Description = "{$GDI.Widget.Accordion.Description$}",
    IconClass = "icon-plus")]
namespace GDI.Components.Widgets.Accordion
{
    public class AccordionViewComponent : ViewComponent
    {
        public const string IDENTIFIER = "GDI.Widget.Accordion";
        private readonly IEventLogService _eventLogService;
        private readonly IMediaFileRepository _mediaFileRepository;

        private readonly IPageRepository<AccordionData, CMS.DocumentEngine.Types.GDI.Accordion> _pageRepository;
        private readonly IPageRepository<AccordionTabData, CMS.DocumentEngine.Types.GDI.AccordionTab> _accordionTabPageRepository;
        /// Injecting the dependence at constructor level
        /// <summary>
        /// <param name="pageRepository"></param>
        /// <param name="eventLogService"></param>
        /// </summary>
        public AccordionViewComponent(IMediaFileRepository mediaFileRepository, IPageRepository<AccordionData, CMS.DocumentEngine.Types.GDI.Accordion> pageRepository, IPageRepository<AccordionTabData, CMS.DocumentEngine.Types.GDI.AccordionTab> accordionTabPageRepository, IEventLogService eventLogService)
        {
            _mediaFileRepository = mediaFileRepository ?? throw new ArgumentNullException(nameof(mediaFileRepository));
            _pageRepository = pageRepository;
            _accordionTabPageRepository = accordionTabPageRepository;
            _eventLogService = eventLogService;
        }
        /// <summary>
        /// Mapping the AccordionProperties data to AccordionViewModel
        /// Return the widget view
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(AccordionProperties properties, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (properties != null)
                {
                    string? path = properties.Path?.FirstOrDefault()?.NodeAliasPath;

                    var accordion = await _pageRepository.GetPagesInCurrentCultureAsync(
                    CancellationToken.None,
                    filter => filter.FilterDuplicates()
                        .Path(path, CMS.DocumentEngine.PathTypeEnum.Children),
                    buildCacheAction:
                        cache => cache
                            .Key($"{nameof(AccordionViewComponent)}|{nameof(InvokeAsync)}|{path}|Accordion")
                            .Dependencies((_, builder) => builder
                                .PageType(CMS.DocumentEngine.Types.GDI.Accordion.CLASS_NAME)
                                .PagePath(path, CMS.DocumentEngine.PathTypeEnum.Children)
                                .PageOrder()
                                ));

                    IEnumerable<AccordionTabData> accordionTab = new List<AccordionTabData>();
                    try
                    {
                        accordionTab = await _accordionTabPageRepository.GetPagesInCurrentCultureAsync(
                    CancellationToken.None,
                    filter => filter.FilterDuplicates()
                        .Path(path, CMS.DocumentEngine.PathTypeEnum.Children)
                        .TopN(6),
                    buildCacheAction:
                        cache => cache
                            .Key($"{nameof(AccordionViewComponent)}|{nameof(InvokeAsync)}|{path}|AccordionTab")
                            .Dependencies((_, builder) => builder
                                .PageType(CMS.DocumentEngine.Types.GDI.AccordionTab.CLASS_NAME)
                                .PagePath(path, CMS.DocumentEngine.PathTypeEnum.Children)
                                .PageOrder()));
                    }
                    catch (Exception e)
                    {
                        _eventLogService.LogException(nameof(AccordionViewComponent), nameof(InvokeAsync), e);
                    }


                    AccordionViewModel _accordion = new AccordionViewModel()
                    {
                        GetAccordions = accordion.OrderBy(x => x.NodeOrder).ToList(),
                        GetAccordionTabs = accordionTab.OrderBy(x => x.NodeOrder).ToList(),
                        Content = properties?.Content,
                        Path = properties?.Path,
                    };

                    if (properties?.BackgroundImage?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.BackgroundImage.First().FileGuid);
                        _accordion.BackgroundImage = (mediaFile?.MediaFileUrl?.DirectPath)?.Replace('~', ' ');
                    }
                    else
                    {
                        _accordion.BackgroundImage = null;
                    }

                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/Accordion/_Accordion.cshtml", _accordion));
                }
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(AccordionViewComponent), nameof(InvokeAsync), ex);
            }
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }
    }

}
