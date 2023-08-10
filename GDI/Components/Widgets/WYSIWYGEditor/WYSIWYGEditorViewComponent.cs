using CMS.Core;
using GDI.Components.Widgets.WYSIWYGEditor;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
[assembly: RegisterWidget(WYSIWYGEditorViewComponent.IDENTIFIER, typeof(WYSIWYGEditorViewComponent), "{$GDI.Widget.WYSIWYGEditor.Name$}", typeof(WYSIWYGEditorProperties), Description = "{$GDI.Widget.WYSIWYGEditor.Description}", IconClass = "icon-l-text")]

namespace GDI.Components.Widgets.WYSIWYGEditor
{
    public class WYSIWYGEditorViewComponent : ViewComponent
    {
        public const string IDENTIFIER = "WYSIWYGEditor.Widget";
        private readonly IEventLogService? _eventLogService;
        public async Task<IViewComponentResult> InvokeAsync(WYSIWYGEditorProperties properties, CancellationToken? cancellationToken = null)
        {

            try
            {
                if (properties != null)
                {

                    WYSIWYGEditorViewModel _wysiwygModel = new();
                    _wysiwygModel.IsVisible = properties.Visible;
                    _wysiwygModel.HtmlText = properties.HtmlText;

                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/WYSIWYGEditor/_WYSIWYGEditor.cshtml", _wysiwygModel));
                }

                else { return await Task.FromResult<IViewComponentResult>(Content(string.Empty)); }
            }
            catch (Exception ex)
            {

                _eventLogService.LogException(nameof(WYSIWYGEditorViewComponent), nameof(InvokeAsync), ex);


                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

            }

        }

    }
}