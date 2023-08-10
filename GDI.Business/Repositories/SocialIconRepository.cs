using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;
namespace GDI.Business.Repositories
{
    public class SocialIconRepository : BasePageRepository<SocialIcons, SocialMedia>
    {
        public SocialIconRepository(IRepositoryServices repositoryServices) : base(repositoryServices)
        {

        }
        public override void MapDtoProperties(SocialMedia page, SocialIcons dto)
        {
            dto.Title = page.Title;
            dto.Target = page.Target;
            dto.RedirectUrl = page.RedirectUrl;
            dto.IconClass = page.IconClass;
        }
    }
}
