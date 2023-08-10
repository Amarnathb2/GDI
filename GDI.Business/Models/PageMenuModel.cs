using XperienceAdapter.Models;
namespace GDI.Business.Models
{
    public class PageMenuModel : NavigationItem
    {

        /// <summary>
        /// Declaring the SourceColumns
        /// </summary>
        public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
        {
      "PageTitle",
       "Description",
       "TeaserImage",
       "TeaserImageAltText",
       "MenuItemGroup",
       "HasChild",
       "IsChild"
     });
        /// <summary>
        /// PageTitle
        /// </summary>
        public string? PageTitle { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// TeaserImage
        /// </summary>
        public string? TeaserImage { get; set; }

        /// <summary>
        /// TeaserImageAltText
        /// </summary>
        public string? TeaserImageAltText { get; set; }

        /// <summary>
        /// MenuItemGroup
        /// </summary>

        public string? MenuItemGroup { get; set; }
        /// <summary>
        /// For Parent pages
        /// </summary>
        public bool? HasChild { get; set; }
        /// <summary>
        /// For Child pages
        /// </summary>
        public bool? IsChild { get; set; }


    }
}
