using XperienceAdapter.Models;
namespace GDI.Business.Models
{
    public class PageDataConfiguration : BasicPage
    {
    public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
   {
   "HeaderLogo",
    "HeaderLogoAltText",
    "HeaderLogoLink",
    "CopyRightText",
    "FooterLogoLink",
    "FooterLogoClass"
   });

        /// <summary>
        /// HeaderLogo
        /// </summary>
        public string? HeaderLogo { get; set; }
        /// <summary>
        /// PageConfiguration HeaderLogoAltText properties
        /// </summary>
        public string? HeaderLogoAltText { get; set; }
        /// <summary>
        /// HeaderLogoLink
        /// </summary>
        public string? HeaderLogoLink { get; set; }
        /// <summary>
        /// CopyRightText
        /// </summary>
        public string? CopyRightText { get; set; }

        public string? FooterLogoLink { get; set; }

        public string? FooterLogoClass { get; set; }
    }
}
