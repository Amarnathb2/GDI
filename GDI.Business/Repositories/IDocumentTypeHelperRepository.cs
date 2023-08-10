using CMS.DataEngine;
namespace GDI.Business.Repositories
{
    public interface IDocumentTypeHelperRepository
    {
        /// <summary>
        /// GetPageTypeClasses
        /// </summary>
        /// <returns></returns>
        public ObjectQuery<DataClassInfo> GetPageTypeClasses();
    }
}
