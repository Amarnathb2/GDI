using CMS.DataEngine;

namespace PIMS
{
    /// <summary>
    /// Declares members for <see cref="GDI_CategoryInfo"/> management.
    /// </summary>
    public partial interface IGDI_CategoryInfoProvider : IInfoProvider<GDI_CategoryInfo>, IInfoByIdProvider<GDI_CategoryInfo>, IInfoByNameProvider<GDI_CategoryInfo>
    {
    }
}