using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace GDI.Components.Widgets.Location
{
    public class LocationProperties : IWidgetProperties
    {


        #region ToolTips Constants
        /// <summary>
        /// 
        /// </summary>
        public const string visibleToolTip = "Indicates if the widget should be displayed.";

        #endregion
        #region Widget Properties


        [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Visible", Order = 0, Tooltip = visibleToolTip)]
        public bool Visible { get; set; } = true;


        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Adress Line1")]
        public string? AddressLine1 { get; set; }


        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Adress Line2")]

        public string? AddressLine2 { get; set; }


        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "City")]
        public string? City { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "State")]

        public string? State { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Country")]
        public string? Country { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "Zip Code")]
        public string? ZipCode { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "Phone")]
        public string? Phone { get; set; }


        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "Email")]
        public string? Email { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "Latitude")]
        public string? Latitude { get; set; }


        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "Longitude")]
        public string? Longitude { get; set; }

        #endregion

    }

}
