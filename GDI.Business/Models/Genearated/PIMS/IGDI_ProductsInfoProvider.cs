using CMS.DataEngine;

namespace PIMS
{
    /// <summary>
    /// Declares members for <see cref="GDI_ProductsInfo"/> management.
    /// </summary>
    public partial interface IGDI_ProductsInfoProvider : IInfoProvider<GDI_ProductsInfo>, IInfoByIdProvider<GDI_ProductsInfo>, IInfoByNameProvider<GDI_ProductsInfo>
    {
    }
}