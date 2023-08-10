using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;
namespace GDI.Business.Repositories
{
    public class PageConfigurationRepository: BasePageRepository<PageDataConfiguration, PageConfiguration>
    {

        public PageConfigurationRepository(IRepositoryServices repositoryServices) : base(repositoryServices)
        {

        }

        public override void MapDtoProperties(PageConfiguration page, PageDataConfiguration dto)
        {
            dto.HeaderLogo=page.HeaderLogo;
            dto.HeaderLogoAltText=page.HeaderLogoAltText;
            dto.HeaderLogoLink=page.HeaderLogoLink; 
            dto.CopyRightText=page.CopyRightText;
            dto.FooterLogoLink=page.FooterLogoLink;
            dto.FooterLogoClass=page.FooterLogoClass;
        }
    }
}
