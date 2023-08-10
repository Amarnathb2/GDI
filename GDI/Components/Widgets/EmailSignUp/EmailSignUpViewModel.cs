using Kentico.Components.Web.Mvc.FormComponents;

namespace GDI.Components.Widgets.EmailSignUp
{
	public class EmailSignUpViewModel
	{
		/// <summary>
		/// IsVisible
		/// </summary>
		public bool? IsVisible { get; set; } = true;
		/// <summary>
		/// Title
		/// </summary>
		public string? Title { get; set; }
		/// <summary>
		/// Description
		/// </summary>
		public string? Content { get; set; }
		/// <summary>
		/// GetBusinessData
		/// </summary>
		public IEnumerable<GDI.Business.Models.EmailBusinessData>? GetBusinessData { get; set; }
		/// <summary>
		/// Widget Path model
		/// </summary>
		public IList<PathSelectorItem>? Path { get; set; }
	}
}
