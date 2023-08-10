using GDI.Business.Models;

using Kentico.Components.Web.Mvc.FormComponents;
namespace GDI.Components.Widgets.CommoditiesOrderForm
{
    public class CommoditiesOrderViewModel
    {

        public IList<PathSelectorItem>? Path { get; set; }
        /// <summary>
        /// Displays the number of Pages to be retrieved 
        /// </summary>
        public int? TopN { get; set; }
        /// <summary>
        ///  IsVisibility Feature to Editor
        /// </summary>
        public bool? IsVisible { get; set; } = true;
        /// <summary>
        /// Declaring the productData list item
        /// </summary>
        public List<ProductFormData>? productData { get; set; }
        /// <summary>
        /// Declaring the SubProduct list item
        /// </summary>
        public List<SubProductItems>? SubProduct { get; set; }
        /// <summary>
        /// Declaring the ProductCardTitle 
        /// </summary>
        public string? ProductCardTitle { get; set; }
        /// <summary>
        /// Declaring the ProductFormTitle
        /// </summary>
        public string? ProductFormTitle { get; set; }
        /// <summary>
        /// Declaring the states dropdown field
        /// </summary>
        public IEnumerable<ContactFormStatesData>? Getstates { get; set; }
        /// <summary>
        /// Declaring the Countries DropDown field
        /// </summary>
        public IEnumerable<ContactFormCountriesData>? GetCountries { get; set; }

    }

    public class CommoditiesOrderForm
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public string? ZipCode { get; set; }
        public string? OptIn { get; set; }
        public string? Comments { get; set; }
    }

    public class ProductRequestForm
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }

        public string? OptIn { get; set; }
        public string? Comments { get; set; }
    }
    public class FormDetails
    {
        public string? RecordType { get; set; }
        public string? Origin { get; set; }
        public string? Tags { get; set; }
        public string? ExternalCampaignId { get; set; }
        public string? Type { get; set; }
    }
}
