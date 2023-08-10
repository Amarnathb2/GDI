using GDI.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Business.Repositories
{
    public  interface IContactFormRepository
    {
        /// <summary>
        /// getting the states data
        /// </summary>
        /// <returns></returns>
        IEnumerable<ContactFormStatesData> GetStatesData();

        IEnumerable<ContactFormCountriesData> GetCountriesData();

    }
}
