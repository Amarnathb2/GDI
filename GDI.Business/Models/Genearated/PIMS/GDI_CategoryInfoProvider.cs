using CMS.DataEngine;

namespace PIMS
{
    /// <summary>
    /// Class providing <see cref="GDI_CategoryInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IGDI_CategoryInfoProvider))]
    public partial class GDI_CategoryInfoProvider : AbstractInfoProvider<GDI_CategoryInfo, GDI_CategoryInfoProvider>, IGDI_CategoryInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GDI_CategoryInfoProvider"/> class.
        /// </summary>
        public GDI_CategoryInfoProvider()
            : base(GDI_CategoryInfo.TYPEINFO)
        {
        }
    }
}