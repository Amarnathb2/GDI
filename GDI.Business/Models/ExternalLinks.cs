using XperienceAdapter.Models;
namespace GDI.Business.Models
{
    public class ExternalLinks: BasicPage
    {
        public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
       {
            "PageTitle",
             "PageUrl",
             "PageUrlTarget"

        });
        /// <summary>
        /// PageTitle
        /// </summary>
        public string? PageTitle { get; set; }
        /// <summary>
        /// PageUrl
        /// </summary>
        public string? PageUrl { get; set; }
        /// <summary>
        /// PageUrlTarget 
        /// </summary>
        public string? PageUrlTarget { get; set; }

    }
}
