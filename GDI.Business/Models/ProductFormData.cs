using XperienceAdapter.Models;

namespace GDI.Business.Models
{
    public class ProductFormData: BasicPage
    {
        public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
          {
            "ProductTitle",
            "ImageAltText",
            "Image",
            "BackgroundColor"
           });
        /// <summary>
        /// Title
        /// </summary>
        public string? ProductTitle { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// BackgroundColor
        /// </summary>
        public string? BackgroundColor { get; set; }
        /// <summary>
        /// ImageAltText
        /// </summary>
        public string? ImageAltText { get; set; }   
    }
}
