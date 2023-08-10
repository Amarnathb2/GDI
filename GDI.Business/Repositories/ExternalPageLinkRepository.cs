using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;
namespace GDI.Business.Repositories
{
    public class ExternalPageLinkRepository : BasePageRepository<ExternalLinks, ExternalPageLinks>
    {
        public ExternalPageLinkRepository(IRepositoryServices repositoryServices) : base(repositoryServices)
        {

        }
        public override void MapDtoProperties(ExternalPageLinks page, ExternalLinks dto)
        {
            dto.PageTitle = page.PageTitle;
            dto.PageUrl = page.PageUrl;
            dto.PageUrlTarget = page.PageUrlTarget;
        }
    }
}
