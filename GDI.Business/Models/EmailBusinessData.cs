using XperienceAdapter.Models;

namespace GDI.Business.Models
{
	public class EmailBusinessData : BasicPage
	{
		/// <summary>
		/// page type properties
		/// </summary>
		public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
			{
		"Title",
		"Description",
		"BackgroundColor",
		});
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? BackgroundColor { get; set; }
		public string? SourceId { get; set; }
		public int NodeOrder { get; set; }

	}
}

