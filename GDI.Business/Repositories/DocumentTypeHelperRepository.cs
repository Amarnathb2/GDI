using CMS.DataEngine;
using CMS.DocumentEngine;
namespace GDI.Business.Repositories
{
    public class DocumentTypeHelperRepository : IDocumentTypeHelperRepository
    {
        /// <summary>
        /// GetPageTypeClasses
        /// </summary>
        /// <returns></returns>
        public ObjectQuery<DataClassInfo> GetPageTypeClasses()
        {
            return DocumentTypeHelper.GetDocumentTypeClasses();
        }
    }
}
