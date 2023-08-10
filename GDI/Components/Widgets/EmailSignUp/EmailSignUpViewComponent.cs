using CMS.Core;
using GDI.Business.Models;
using GDI.Components.Widgets.EmailSignUp;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Repositories;
[assembly: RegisterWidget(EmailSignUpViewComponent.IDENTIFIER, typeof(EmailSignUpViewComponent), "{$GDI.Widget.EmailSignUp.Name$}",
	typeof(EmailSignUpProperties), Description = "{$GDI.Widget.EmailSignUp.Description$}",
	IconClass = "icon-message")]
namespace GDI.Components.Widgets.EmailSignUp
{
	public class EmailSignUpViewComponent : ViewComponent
	{
		public const string IDENTIFIER = "GDI.Widget.EmailSignUp";
		private readonly IEventLogService _eventLogService;
		private readonly IMediaFileRepository _mediaFileRepository;

		private readonly IPageRepository<EmailBusinessData, CMS.DocumentEngine.Types.GDI.EmailBusiness> _emailBusinessRepository;
		/// Injecting the dependence at constructor level
		/// <summary>
		/// <param name="pageRepository"></param>
		/// <param name="eventLogService"></param>
		/// </summary>
		public EmailSignUpViewComponent(IMediaFileRepository mediaFileRepository, IPageRepository<EmailBusinessData, CMS.DocumentEngine.Types.GDI.EmailBusiness> emailBusinessRepository, IEventLogService eventLogService)
		{
			_mediaFileRepository = mediaFileRepository ?? throw new ArgumentNullException(nameof(mediaFileRepository));
			_emailBusinessRepository = emailBusinessRepository;
			_eventLogService = eventLogService;
		}
		/// <summary>
		/// Mapping the AccordionProperties data to AccordionViewModel
		/// Return the widget view
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IViewComponentResult> InvokeAsync(EmailSignUpProperties properties, CancellationToken? cancellationToken = null)
		{
			try
			{
				if (properties != null)
				{
					string? path = properties.Path?.FirstOrDefault()?.NodeAliasPath;

					var _emailBusinessData = await _emailBusinessRepository.GetPagesInCurrentCultureAsync(
					CancellationToken.None,
					filter => filter.FilterDuplicates()
						.Path(path, CMS.DocumentEngine.PathTypeEnum.Children),
					buildCacheAction:
						cache => cache
							.Key($"{nameof(EmailSignUpViewComponent)}|{nameof(InvokeAsync)}")
							.Dependencies((_, builder) => builder
								.PageType(CMS.DocumentEngine.Types.GDI.EmailBusiness.CLASS_NAME)
								.PagePath(path, CMS.DocumentEngine.PathTypeEnum.Children)
								.PageOrder()
								));


					EmailSignUpViewModel _email = new EmailSignUpViewModel();

					_email.GetBusinessData = _emailBusinessData.OrderBy(x => x.NodeOrder).ToList();
					_email.Title = properties.Title;
					_email.Content = properties?.Content;
					_email.Path = properties?.Path;

					return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/EmailSignUp/_EmailSignUp.cshtml", _email));
				}
				else
				{ return await Task.FromResult<IViewComponentResult>(Content(string.Empty)); }
			}
			catch (Exception ex)
			{
				_eventLogService.LogException(nameof(EmailSignUpViewComponent), nameof(InvokeAsync), ex);
				return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
			}
		}
	}

}
