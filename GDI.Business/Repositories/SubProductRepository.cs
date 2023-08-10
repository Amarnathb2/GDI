using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Business.Repositories
{
    public class SubProductRepository: BasePageRepository<SubProductItems, Subproduct>
    {
        public SubProductRepository(IRepositoryServices repositoryServices) : base(repositoryServices)
        {


        }
        public override void MapDtoProperties(Subproduct page, SubProductItems dto)
        {
            dto.Title = page.Title;
        }
    }
}
