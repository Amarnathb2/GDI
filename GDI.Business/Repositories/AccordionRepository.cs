using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Business.Repositories
{
    public class AccordionRepository : BasePageRepository<AccordionData, Accordion>
    {

        /// <summary>
        /// Create an Instance for AccordionRepository
        /// </summary>
        /// <param name="repositoryDependencies"></param>
        public AccordionRepository(IRepositoryServices repositoryDependencies) : base(repositoryDependencies)
        {
        }
        /// <summary>
        /// maps the properties of Dto and the fileds on the page types
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dto"></param>
        public override void MapDtoProperties(Accordion page, AccordionData dto)
        {
            dto.AccordionTitle = page.AccordionTitle;
            dto.LeftSection = page.LeftSection;
            dto.RightSection = page.RightSection;
            dto.ButtonText = page.ButtonText;
            dto.ButtonURL = page.ButtonURL;
            dto.AccordionId = (page.AccordionTitle).Replace(" ", "").ToLower();
            dto.NodeOrder = page.NodeOrder;
        }
    }
}