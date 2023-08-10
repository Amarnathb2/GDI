using CMS.DocumentEngine.Types.GDI;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Business.Repositories
{
	public class EmailBusinessDataRepository : BasePageRepository<EmailBusinessData, EmailBusiness>
	{

		/// <summary>
		/// Create an Instance for AccordionRepository
		/// </summary>
		/// <param name="repositoryDependencies"></param>
		public EmailBusinessDataRepository(IRepositoryServices repositoryDependencies) : base(repositoryDependencies)
		{
		}
		/// <summary>
		/// maps the properties of Dto and the fileds on the page types
		/// </summary>
		/// <param name="emailBusiness"></param>
		/// <param name="emailBusinessData"></param>
		public override void MapDtoProperties(EmailBusiness emailBusiness, EmailBusinessData emailBusinessData)
		{
			emailBusinessData.Title = emailBusiness.Title;
			emailBusinessData.Description = emailBusiness.Description;
			emailBusinessData.BackgroundColor = emailBusiness.BackgroundColor;
			emailBusinessData.SourceId = (emailBusiness.Title).Replace(" ", "");
			emailBusinessData.NodeOrder = emailBusiness.NodeOrder;
		}
	}
}