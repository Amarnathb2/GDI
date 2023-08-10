using CMS.Core;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.GDI;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GDI.Controller
{
    public class RobotsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPageRetriever _pageRetriever;
        private readonly IPageDataContextInitializer _pageDataContextInitializer;
        public RobotsController(IPageRetriever pageRetriever, IPageDataContextInitializer pageDataContextInitializer)
        {
            _pageRetriever = pageRetriever;
            _pageDataContextInitializer = pageDataContextInitializer;
        }
        [Route("/robots.txt")]
        public ActionResult Index(CancellationToken cancellationToken)
        {
            try
            {

                var data = _pageRetriever.Retrieve<Robots>(query => query
                    .Path("/Robots", PathTypeEnum.Single)).FirstOrDefault();
                if (data != null)
                {
                    _pageDataContextInitializer.Initialize(data);
                    var objReader = data.RobotsText;
                    return this.Content(objReader.ToString(), "text/plain", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(RobotsController), nameof(Index), ex);
            }
            return NotFound();
        }
    }

}