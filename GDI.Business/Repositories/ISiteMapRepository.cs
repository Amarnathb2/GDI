using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Business.Repositories
{
    public interface ISiteMapRepository
    {
        /// <summary>
        /// GetXMLSiteMapDocuments
        /// </summary>
        /// <returns></returns>
        public MultiDocumentQuery GetXMLSiteMapDocuments();
    }

}
