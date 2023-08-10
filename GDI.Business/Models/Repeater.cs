using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Business.Models
{
    public class Repeater
    {
        /// <summary>
        /// Current page type name
        /// </summary>
        public string? PageTypeClassName { get; set; }

        /// <summary>
        /// Selected page path
        /// </summary>
        public string? SelectedPath { get; set; }

        /// <summary>
        /// Number of records display
        /// </summary>
        //public int TopN { get; set; }

        /// <summary>
        /// Selected columns
        /// </summary>
        //public string? Column { get; set; }

        /// <summary>
        /// Order by filter data
        /// </summary>
        public string? OrderBy { get; set; }
        public int MaxItemsDisplayed { get; set; }

        public string? Where { get; set; }
    }
}
