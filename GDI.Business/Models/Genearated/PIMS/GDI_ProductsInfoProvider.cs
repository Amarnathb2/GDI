using CMS.DataEngine;

namespace PIMS
{
    /// <summary>
    /// Class providing <see cref="GDI_ProductsInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IGDI_ProductsInfoProvider))]
    public partial class GDI_ProductsInfoProvider : AbstractInfoProvider<GDI_ProductsInfo, GDI_ProductsInfoProvider>, IGDI_ProductsInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GDI_ProductsInfoProvider"/> class.
        /// </summary>
        public GDI_ProductsInfoProvider()
            : base(GDI_ProductsInfo.TYPEINFO)
        {
        }
    }
}