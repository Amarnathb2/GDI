using XperienceAdapter.Models;
namespace GDI.Business.Models
{
    public class SocialIcons: BasicPage
    {
        public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
      {
            "Title",
             "RedirectUrl",
             "Target",
            "IconClass"

        });
        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// IconUrl
        /// </summary>
        public string? RedirectUrl { get; set; }
        /// <summary>
        /// Target
        /// </summary>
        public string? Target { get; set; }
        /// <summary>
        /// iconclass
        /// </summary>
        public string? IconClass { get; set; }  
    }
}
