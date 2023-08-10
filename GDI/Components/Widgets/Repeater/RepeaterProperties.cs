using GDI.Models.FormComponents;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace GDI.Components.Widgets.Repeater
{
    public class RepeaterProperties: IWidgetProperties
    {
        #region ToolTips Constants
        /// <summary>
        /// 
        /// </summary>
        public const string visibleToolTip = "Indicates if the widget should be displayed.";
        /// <summary>
        /// 
        /// </summary>
        public const string pathToolTip = "Specifies the path of the selected pages. If you leave the path empty, the widget either loads all child pages or selects the current page(depending on the page type and configuration of the widget other properties).";

        /// <summary>
        /// 
        /// </summary>
        public const string maxitemsdisplayedToolTip = "Specifies the maximum of pages to be loaded. At least as many pages as in the 'visible' value of the 'initialization script' property need to be specified. If empty, all possible pages will be selected.";
        /// <summary>
        /// 
        /// </summary>
        public const string orderByToolTip = "Sets the value of the ORDER BY clause in the SELECT statement used to retrieve the content. You can specify only the columns common to all of the selected page types.";
        /// <summary>
        /// 
        /// </summary>


        /// <summary>
        /// 
        /// </summary>
        public const string viewPathToolTip = "Configure the view with the corresponding page type-related fields and with the proper design after assigning the view path to this field(View Path). View path is being considered from 'Views/Shared/' path, just input the remaining path of a partial view without the extension. E.g.: Articles/_ArticleViewList";

        /// <summary>
        /// 
        /// </summary>
        public const string noDataTextToolTip = "Text to be displayed if no records are found.";
        #endregion
        #region Widget Properties
        /// <summary>
        /// Selected page type
        /// </summary>

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Visible", Order = 0, Tooltip = visibleToolTip)]
        public bool Visible { get; set; } = true;


        [EditingComponent(RepeaterPageTypeSelectorComponent.IDENTIFIER, Order = 1, Label = "Page Type Class Name*")]
        public string? PageTypeClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EditingComponent(PathSelector.IDENTIFIER, Order = 2, Label = "Page Path*", Tooltip = pathToolTip)]
        [EditingComponentProperty(nameof(PathSelectorProperties.RootPath), "/")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select the path")]
        public IList<PathSelectorItem>? Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EditingComponent(RepeaterTransformationSelectorComponent.IDENTIFIER, Label = "Tranformation*", Order = 3, Tooltip = viewPathToolTip)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select the Transformation")]
        public string? ViewName { get; set; }
        /// <summary>
        /// 
        /// </summary>



        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Items per row (Large screens)", Order = 4, Tooltip = maxitemsdisplayedToolTip)]
        [Range(1, 100, ErrorMessage = "Please enter valid number")]
        public int NumLarge { get; set; } = 4;

        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Items per row (Medium screens)", Order = 5, Tooltip = maxitemsdisplayedToolTip)]
        [Range(1, 100, ErrorMessage = "Please enter valid number")]
        public int NumMedium { get; set; } = 2;

        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Items per row (Small screens)", Order = 6, Tooltip = maxitemsdisplayedToolTip)]
        [Range(1, 100, ErrorMessage = "Please enter valid number")]
        public int NumSmall { get; set; } = 1;

        /// <summary>
        /// TopN
        /// </summary>
        [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Max Items Displayed", Order = 7, Tooltip = maxitemsdisplayedToolTip)]
        [Range(1, 100, ErrorMessage = "Please enter valid number")]
        public int MaxItemsDisplayed { get; set; } = 10;
        /// <summary>
        /// 
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Order by", Order = 8, Tooltip = orderByToolTip)]
        public string? OrderBy { get; set; } = "NodeOrder";

        /// <summary>
        /// 
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Where", Order = 9)]
        public string? Where { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Label = "Content Before", Order = 10)]
        public string? ContentBefore { get; set; }
        [EditingComponent(RichTextComponent.IDENTIFIER, Label = "Content After", Order = 11)]
        public string? ContentAfter { get; set; }


        #endregion


    }
}

