using CMS.Core;
using CMS.DocumentEngine;
using GDI.Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;

namespace GDI.Controller
{
    public class SitemapController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEventLogService _eventLogService;
        private readonly ISiteMapRepository _siteMapRepository;
        /// <summary>
        /// SitemapController
        /// </summary>
        /// <param name="eventLogService"></param>
        /// <param name="siteMapRepository"></param>
        public SitemapController(IEventLogService eventLogService, ISiteMapRepository siteMapRepository)
        {
            _eventLogService = eventLogService;
            _siteMapRepository = siteMapRepository;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [Route("/sitemap.xml")]
        [Route("sitemap")]
        public ActionResult Index()
        {
            try
            {
                List<SitemapNode> nodes = new();
                foreach (var doc in _siteMapRepository.GetXMLSiteMapDocuments())
                {
                    string url = DocumentURLProvider.GetAbsoluteUrl(doc);
                    nodes.Add(new SitemapNode(url)
                    {
                        LastModificationDate = Convert.ToDateTime(doc?.DocumentModifiedWhen.Date)
                    });
                }
                return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(ActionResult), nameof(Index), ex);
                return null!;
            }
        }
    }
    }
