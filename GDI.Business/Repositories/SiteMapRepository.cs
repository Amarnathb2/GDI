using CMS.Core;
using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Business.Repositories
{
    public class SiteMapRepository : ISiteMapRepository
    {
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// SiteMapRepository
        /// </summary>
        /// <param name="eventLogService"></param>
        public SiteMapRepository(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        /// <summary>
        /// GetXMLSiteMapDocuments
        /// </summary>
        /// <returns></returns>
        public MultiDocumentQuery GetXMLSiteMapDocuments()
        {
            try
            {
                var currentlyOpenedLink = CMS.Helpers.RequestContext.URL.AbsolutePath;
                var defaultOrderBy = "NodeLevel, NodeOrder, DocumentName";
                var dataLoadMethod = new MultiDocumentQuery()
                                .Type("GDI.PageMenu", x => x.WhereEquals("ShowInSiteMap", true))
                                .Path("/%")
                                .OrderBy()
                                .PublishedVersion(true)
                                  .Published(true)
                                  .OrderBy(defaultOrderBy)
                                  .OnCurrentSite();
                return dataLoadMethod;
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(SiteMapRepository), nameof(GetXMLSiteMapDocuments), ex);
                return null!;
            }
        }
    }
    }
