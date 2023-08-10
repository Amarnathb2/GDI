using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Business.Models
{
    public class ProductDto
    {
        public string GDICategoryName { get; set; }
        public int GDI_ProductsID { get; set; }

        public string GDIProductName { get; set; }
        public string GDIProductDescription { get; set; }
        public string GDIProductDisplayName { get; set; }

        //public string MetaTitle { get; set; }

        //public string MetaDescription { get; set; }
        public string GDIProductImgXL { get; set; }

        public string GDIProductImgL { get; set; }

        public string GDIProductImgM { get; set; }

        public string GDIProductImgSM { get; set; }
        public string GDIProductImgAltTxt { get; set; }

public int GDIProductItemOrder { get; set; }
        public  string GDIProductAvail { get; set; }

        public bool GDIProductSampleAvail { get; set; }
        public string GDIProductClaims { get; set; }
        public string DairyPowderType { get; set; }
        public string DairyPowderIntensity { get; set; }
        public string DairyPowderSweetness { get; set; }
        public string DairyPowderFlavors { get; set; }
        public string DairyPowderTate { get; set; }
        public string CheesePowderType { get; set; }
        public string CheesePowderColor { get; set; }
        public string SeasoningBlendType { get; set; }
        public string SeasoningBlendHeatLevel { get; set; }
        public string SeasoningBlendSmokyTone { get; set; }
        public string GDIProductCode { get; set; }

        public string SeasoningBlendFlavor { get; set; }
        public string SeasoningBlendSpicyFlavor { get; set; }
        public string SeasoningBlendTaste { get; set; }
        public string CheesePowderVarietalFlavor { get; set; }
        public string CheesePowderCheddarIntensity { get; set; }
        public string CheesePowderFavorTones { get; set; }
        public string CheesePowderTaste { get; set; }
      

    }
}
