using CMS.DocumentEngine;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GDI.Components.Widgets.Repeater
{
    public class RepeaterPropertiesViewModel
    {
        /// <summary>
        /// Page Types purpose
        /// </summary>
        public List<SelectListItem>? AvailablePageTypes { get; set; }
        /// <summary>
        /// Page type
        /// </summary>
        public string? PageTypeClassName { get; set; }

        /// <summary>
        /// Contains all data related to a particular page type
        /// </summary>
        public List<TreeNode>? PageTypeData { get; set; }

        /// <summary>
        /// Widget is visible or not
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// DataId
        /// </summary>
        public string? DataId { get; set; }

        /// <summary>
        /// NoDataText
        /// </summary>
        public string? NoDataText { get; set; } = "";

        public string? ViewName { get; set; }

        public int NumLarge { get; set; }

        public int NumSmall { get; set; }
        public int NumMedium { get; set; }

    }
}

