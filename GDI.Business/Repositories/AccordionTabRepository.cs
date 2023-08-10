using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Business.Repositories
{
    public class AccordionTabRepository : BasePageRepository<AccordionTabData, AccordionTab>
    {

        /// <summary>
        /// Create an Instance for AccordionRepository
        /// </summary>
        /// <param name="repositoryDependencies"></param>
        public AccordionTabRepository(IRepositoryServices repositoryDependencies) : base(repositoryDependencies)
        {
        }
        /// <summary>
        /// maps the properties of Dto and the fileds on the page types
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dto"></param>
        public override void MapDtoProperties(AccordionTab _accordionTabPage, AccordionTabData _accordionTabDto)
        {
            _accordionTabDto.AccordionTitle = _accordionTabPage.AccordionTitle;
            _accordionTabDto.LeftSection = _accordionTabPage.LeftSection;
            _accordionTabDto.RightSection = _accordionTabPage.RightSection;
            _accordionTabDto.ButtonText = _accordionTabPage.ButtonText;
            _accordionTabDto.ButtonURL = _accordionTabPage.ButtonURL;
            _accordionTabDto.AccordionId = (_accordionTabPage.AccordionTitle).Replace(" ", "").ToLower();
            _accordionTabDto.TabImage = _accordionTabPage.TabImage;
            _accordionTabDto.NodeOrder = _accordionTabPage.NodeOrder;
        }
    }
}