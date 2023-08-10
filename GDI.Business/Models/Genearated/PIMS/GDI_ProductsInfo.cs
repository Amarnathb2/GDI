using CMS;
using CMS.DataEngine;
using CMS.Helpers;

using PIMS;

using System.Data;
using System.Runtime.Serialization;

[assembly: RegisterObjectType(typeof(GDI_ProductsInfo), GDI_ProductsInfo.OBJECT_TYPE)]

namespace PIMS
{
    /// <summary>
    /// Data container class for <see cref="GDI_ProductsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class GDI_ProductsInfo : AbstractInfo<GDI_ProductsInfo, IGDI_ProductsInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "pims.gdi_products";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(GDI_ProductsInfoProvider), OBJECT_TYPE, "PIMS.GDI_Products", "GDI_ProductsID", "GDI_ProductsLastModified", "GDI_ProductsGuid", "GDIProductName", "GDIProductName", null, null, null, null)
        {
            ModuleName = "PIMS",
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("GDICategoryID", "pims.gdi_category", ObjectDependencyEnum.Required),
            },
        };


        /// <summary>
        /// Products ID.
        /// </summary>
        [DatabaseField]
        public virtual int GDI_ProductsID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("GDI_ProductsID"), 0);
            }
            set
            {
                SetValue("GDI_ProductsID", value);
            }
        }


        /// <summary>
        /// Is active.
        /// </summary>
        [DatabaseField]
        public virtual bool IsActive
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("IsActive"), true);
            }
            set
            {
                SetValue("IsActive", value);
            }
        }


        /// <summary>
        /// GDI category ID.
        /// </summary>
        [DatabaseField]
        public virtual int GDICategoryID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("GDICategoryID"), 0);
            }
            set
            {
                SetValue("GDICategoryID", value);
            }
        }


        /// <summary>
        /// GDI product code.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductCode
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductCode"), String.Empty);
            }
            set
            {
                SetValue("GDIProductCode", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product name.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductName"), String.Empty);
            }
            set
            {
                SetValue("GDIProductName", value, String.Empty);
            }
        }


        /// <summary>
        /// Same as Product Name but with the flexibility to add HTML tagging.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductDisplayName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductDisplayName"), String.Empty);
            }
            set
            {
                SetValue("GDIProductDisplayName", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product description.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductDescription
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductDescription"), String.Empty);
            }
            set
            {
                SetValue("GDIProductDescription", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product img SM.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductImgSM
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductImgSM"), String.Empty);
            }
            set
            {
                SetValue("GDIProductImgSM", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product img M.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductImgM
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductImgM"), String.Empty);
            }
            set
            {
                SetValue("GDIProductImgM", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product img L.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductImgL
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductImgL"), String.Empty);
            }
            set
            {
                SetValue("GDIProductImgL", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product img XL.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductImgXL
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductImgXL"), String.Empty);
            }
            set
            {
                SetValue("GDIProductImgXL", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product img alt txt.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductImgAltTxt
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductImgAltTxt"), String.Empty);
            }
            set
            {
                SetValue("GDIProductImgAltTxt", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product avail.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductAvail
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductAvail"), String.Empty);
            }
            set
            {
                SetValue("GDIProductAvail", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product sample avail.
        /// </summary>
        [DatabaseField]
        public virtual bool GDIProductSampleAvail
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("GDIProductSampleAvail"), false);
            }
            set
            {
                SetValue("GDIProductSampleAvail", value);
            }
        }


        /// <summary>
        /// GDI product claims.
        /// </summary>
        [DatabaseField]
        public virtual string GDIProductClaims
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDIProductClaims"), String.Empty);
            }
            set
            {
                SetValue("GDIProductClaims", value, String.Empty);
            }
        }


        /// <summary>
        /// Dairy powder type.
        /// </summary>
        [DatabaseField]
        public virtual string DairyPowderType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DairyPowderType"), String.Empty);
            }
            set
            {
                SetValue("DairyPowderType", value, String.Empty);
            }
        }


        /// <summary>
        /// Dairy powder intensity.
        /// </summary>
        [DatabaseField]
        public virtual string DairyPowderIntensity
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DairyPowderIntensity"), String.Empty);
            }
            set
            {
                SetValue("DairyPowderIntensity", value, String.Empty);
            }
        }


        /// <summary>
        /// Dairy powder sweetness.
        /// </summary>
        [DatabaseField]
        public virtual string DairyPowderSweetness
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DairyPowderSweetness"), String.Empty);
            }
            set
            {
                SetValue("DairyPowderSweetness", value, String.Empty);
            }
        }


        /// <summary>
        /// Dairy powder flavors.
        /// </summary>
        [DatabaseField]
        public virtual string DairyPowderFlavors
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DairyPowderFlavors"), String.Empty);
            }
            set
            {
                SetValue("DairyPowderFlavors", value, String.Empty);
            }
        }


        /// <summary>
        /// Dairy powder tate.
        /// </summary>
        [DatabaseField]
        public virtual string DairyPowderTate
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DairyPowderTate"), String.Empty);
            }
            set
            {
                SetValue("DairyPowderTate", value, String.Empty);
            }
        }


        /// <summary>
        /// Cheese powder type.
        /// </summary>
        [DatabaseField]
        public virtual string CheesePowderType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CheesePowderType"), String.Empty);
            }
            set
            {
                SetValue("CheesePowderType", value, String.Empty);
            }
        }


        /// <summary>
        /// Cheese powder color.
        /// </summary>
        [DatabaseField]
        public virtual string CheesePowderColor
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CheesePowderColor"), String.Empty);
            }
            set
            {
                SetValue("CheesePowderColor", value, String.Empty);
            }
        }


        /// <summary>
        /// Cheese powder varietal flavor.
        /// </summary>
        [DatabaseField]
        public virtual string CheesePowderVarietalFlavor
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CheesePowderVarietalFlavor"), String.Empty);
            }
            set
            {
                SetValue("CheesePowderVarietalFlavor", value, String.Empty);
            }
        }


        /// <summary>
        /// Cheese powder cheddar intensity.
        /// </summary>
        [DatabaseField]
        public virtual string CheesePowderCheddarIntensity
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CheesePowderCheddarIntensity"), String.Empty);
            }
            set
            {
                SetValue("CheesePowderCheddarIntensity", value, String.Empty);
            }
        }


        /// <summary>
        /// Cheese powder favor tones.
        /// </summary>
        [DatabaseField]
        public virtual string CheesePowderFavorTones
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CheesePowderFavorTones"), String.Empty);
            }
            set
            {
                SetValue("CheesePowderFavorTones", value, String.Empty);
            }
        }


        /// <summary>
        /// Cheese powder taste.
        /// </summary>
        [DatabaseField]
        public virtual string CheesePowderTaste
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CheesePowderTaste"), String.Empty);
            }
            set
            {
                SetValue("CheesePowderTaste", value, String.Empty);
            }
        }


        /// <summary>
        /// Seasoning blend type.
        /// </summary>
        [DatabaseField]
        public virtual string SeasoningBlendType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SeasoningBlendType"), String.Empty);
            }
            set
            {
                SetValue("SeasoningBlendType", value, String.Empty);
            }
        }


        /// <summary>
        /// Seasoning blend heat level.
        /// </summary>
        [DatabaseField]
        public virtual string SeasoningBlendHeatLevel
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SeasoningBlendHeatLevel"), String.Empty);
            }
            set
            {
                SetValue("SeasoningBlendHeatLevel", value, String.Empty);
            }
        }


        /// <summary>
        /// Seasoning blend flavor.
        /// </summary>
        [DatabaseField]
        public virtual string SeasoningBlendFlavor
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SeasoningBlendFlavor"), String.Empty);
            }
            set
            {
                SetValue("SeasoningBlendFlavor", value, String.Empty);
            }
        }


        /// <summary>
        /// Seasoning blend spicy flavor.
        /// </summary>
        [DatabaseField]
        public virtual string SeasoningBlendSpicyFlavor
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SeasoningBlendSpicyFlavor"), String.Empty);
            }
            set
            {
                SetValue("SeasoningBlendSpicyFlavor", value, String.Empty);
            }
        }


        /// <summary>
        /// Seasoning blend smoky tone.
        /// </summary>
        [DatabaseField]
        public virtual string SeasoningBlendSmokyTone
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SeasoningBlendSmokyTone"), String.Empty);
            }
            set
            {
                SetValue("SeasoningBlendSmokyTone", value, String.Empty);
            }
        }


        /// <summary>
        /// Seasoning blend taste.
        /// </summary>
        [DatabaseField]
        public virtual string SeasoningBlendTaste
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SeasoningBlendTaste"), String.Empty);
            }
            set
            {
                SetValue("SeasoningBlendTaste", value, String.Empty);
            }
        }


        /// <summary>
        /// Meta title.
        /// </summary>
        [DatabaseField]
        public virtual string MetaTitle
        {
            get
            {
                return ValidationHelper.GetString(GetValue("MetaTitle"), String.Empty);
            }
            set
            {
                SetValue("MetaTitle", value, String.Empty);
            }
        }


        /// <summary>
        /// Meta description.
        /// </summary>
        [DatabaseField]
        public virtual string MetaDescription
        {
            get
            {
                return ValidationHelper.GetString(GetValue("MetaDescription"), String.Empty);
            }
            set
            {
                SetValue("MetaDescription", value, String.Empty);
            }
        }


        /// <summary>
        /// Meta keywords.
        /// </summary>
        [DatabaseField]
        public virtual string MetaKeywords
        {
            get
            {
                return ValidationHelper.GetString(GetValue("MetaKeywords"), String.Empty);
            }
            set
            {
                SetValue("MetaKeywords", value, String.Empty);
            }
        }


        /// <summary>
        /// GDI product item order.
        /// </summary>
        [DatabaseField]
        public virtual int GDIProductItemOrder
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("GDIProductItemOrder"), 0);
            }
            set
            {
                SetValue("GDIProductItemOrder", value, 0);
            }
        }


        /// <summary>
        /// Products guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid GDI_ProductsGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("GDI_ProductsGuid"), Guid.Empty);
            }
            set
            {
                SetValue("GDI_ProductsGuid", value);
            }
        }


        /// <summary>
        /// Products last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime GDI_ProductsLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("GDI_ProductsLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("GDI_ProductsLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected GDI_ProductsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="GDI_ProductsInfo"/> class.
        /// </summary>
        public GDI_ProductsInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="GDI_ProductsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public GDI_ProductsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}