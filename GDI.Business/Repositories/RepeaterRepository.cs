using CMS.Core;
using CMS.DocumentEngine;
using GDI.Business.Models;
using Kentico.Content.Web.Mvc;
namespace GDI.Business.Repositories
{
    public class RepeaterRepository : IRepeaterRepository
    {
        private readonly IPageRetriever _pageRetriever;
        private readonly IDocumentTypeHelperRepository _documentTypeHelperRepository;
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// Create an Instance for RepeaterRepository
        /// </summary>
        /// <param name="pageRetriever"></param>
        /// <param name="documentTypeHelperRepository"></param>
        /// <param name="eventLogService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RepeaterRepository(IPageRetriever pageRetriever, IDocumentTypeHelperRepository documentTypeHelperRepository, IEventLogService eventLogService)
        {
            _pageRetriever = pageRetriever ?? throw new ArgumentNullException(nameof(pageRetriever));
            _documentTypeHelperRepository = documentTypeHelperRepository ?? throw new ArgumentNullException(nameof(documentTypeHelperRepository));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        }
        /// <summary>
        /// Get particular pagetye data based on widget properties
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Particular pagetype data based on these parameters</returns>
        public List<TreeNode> GetParticularPageTypeData(Repeater dto)
        {
            string? className = dto.PageTypeClassName;
            string? selectedPath = dto.SelectedPath;
            int topN = dto.MaxItemsDisplayed;
            string? where = dto.Where;
            string? orderBy = dto.OrderBy;

            List<TreeNode> PageTypeData = new();
            try
            {
                if (selectedPath != string.Empty)
                {
                    PageTypeData = _pageRetriever.Retrieve(className,
                                                    query => query
                                                            .Path(selectedPath, PathTypeEnum.Children)
                                                            .TopN(topN)
                                                            .OrderBy(orderBy)
                                                           
                                                            .Where(where),
                                                    buildCacheAction: cache => cache
                                                            .Key($"{selectedPath}")
                                                            .Dependencies((_, builder) => builder
                                                            .PageType(className)
                                                            .PagePath(selectedPath, PathTypeEnum.Children)
                                                            .PageOrder())).ToList();
                }
                return PageTypeData;
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(RepeaterRepository), nameof(GetParticularPageTypeData), ex);
                return PageTypeData;

            }
        }
    }
}
