using CMS.Core;
using CMS.DocumentEngine;
using CMS.Helpers;
using GDI.Components.Widgets.Repeater;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using GDI.Business.Repositories;
[assembly: RegisterWidget("GDI.Widgets.Repeater", typeof(RepeaterViewComponent), "{$GDI.Widget.RepeaterWidget.Name$}", typeof(RepeaterProperties),
    Description = "{$GDI.Widget.RepeaterWidget.Description$}", IconClass = "icon-arrows-crooked")]
namespace GDI.Components.Widgets.Repeater
{
    public class RepeaterViewComponent : ViewComponent
    {
        private readonly IRepeaterRepository _repeaterRepository;
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// Create an instance of RepeaterViewComponent
        /// </summary>
        /// <param name="repeaterRepository"></param>
        /// <param name="eventLogService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RepeaterViewComponent(IRepeaterRepository repeaterRepository, IEventLogService eventLogService)
        {
            _repeaterRepository = repeaterRepository ?? throw new ArgumentNullException(nameof(repeaterRepository));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        }
        /// <summary>
        /// Create an Instance for InvokeAsync
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IViewComponentResult> InvokeAsync(RepeaterProperties properties)
        {
            try
            {
                if (properties.PageTypeClassName != null)
                {

                    int topN = ValidationHelper.GetInteger(properties?.MaxItemsDisplayed, 10);
                    string? selectedPath = properties?.Path == null ? "" : properties?.Path?.FirstOrDefault()?.NodeAliasPath;
                    Business.Models.Repeater repeater = new()
                    {

                        PageTypeClassName = properties?.PageTypeClassName,
                        SelectedPath = selectedPath,
                        MaxItemsDisplayed = topN,
                        OrderBy = properties?.OrderBy,

                    };
                    List<TreeNode> pagetypesData = await Task.Run(() => _repeaterRepository.GetParticularPageTypeData(repeater));

                    if (properties?.MaxItemsDisplayed > 0)
                    {
                        return View("~/Components/Widgets/Repeater/" + properties.ViewName + ".cshtml", new RepeaterPropertiesViewModel
                        {

                            ViewName = properties.ViewName,
                            PageTypeClassName = properties.PageTypeClassName,
                            PageTypeData = pagetypesData,
                            Visible = properties.Visible,
                            NumMedium = properties.NumMedium,
                            NumSmall = properties.NumSmall,
                            NumLarge = properties.NumLarge,

                        });
                    }

                    else
                    {
                        return View("~/Components/ViewComponents/Widgets/Repeater/_NoDataFound.cshtml", new RepeaterPropertiesViewModel { PageTypeClassName = properties?.PageTypeClassName, PageTypeData = pagetypesData, Visible = properties!.Visible });
                    }
                }
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }

            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(RepeaterViewComponent), "InvokeAysnc", ex);
                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
            }
        }
    }
}
