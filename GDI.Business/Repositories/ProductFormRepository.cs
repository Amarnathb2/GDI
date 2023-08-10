using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Business.Repositories
{
    public class ProductFormRepository : BasePageRepository<ProductFormData, ProductCard>
    {
        public ProductFormRepository(IRepositoryServices repositoryServices) : base(repositoryServices)
        {


        }

        public override void MapDtoProperties(ProductCard page, ProductFormData dto)
        {
            dto.ProductTitle = page.ProductTitle;
            dto.Image = page.Image;
            dto.ImageAltText = page.ImageAltText;
            dto.BackgroundColor = page.BackgroundColor;
        }
    }
}
