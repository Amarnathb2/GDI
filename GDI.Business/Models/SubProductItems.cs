using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XperienceAdapter.Models;

namespace GDI.Business.Models
{
    public class SubProductItems: BasicPage
    {

        public override IEnumerable<string> SourceColumns => base.SourceColumns.Concat(new[]
         {
           "Title"
           });

        public string? Title { get; set; }

    }
}
