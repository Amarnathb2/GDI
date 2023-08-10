using XperienceAdapter.Models;

namespace GDI.Business.Models
{
    public class AccordionData : BasicPage
    {
        /// <summary>
        /// page type properties
        /// </summary>
        public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
            {
        "AccordionName",
        "AccordionTitle",
        "LeftSection",
        "RightSection",
        "ButtonText",
        "ButtonURL",
        });
        public string? AccordionName { get; set; }
        public string? AccordionTitle { get; set; }
        public string? LeftSection { get; set; }
        public string? RightSection { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonURL { get; set; }
        public string? AccordionId { get; set; }
        public int NodeOrder { get; set; }

    }
}

