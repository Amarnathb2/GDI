using CMS.DocumentEngine;
using GDI.Business.Models;
using Kentico.Components.Web.Mvc.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Business.Repositories
{
    public interface IRepeaterRepository
    {   
        /// <summary>
        /// GetParticularPageTypeData
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<TreeNode> GetParticularPageTypeData(Repeater dto);
    }
}
