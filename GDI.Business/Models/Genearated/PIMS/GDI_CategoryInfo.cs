using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using PIMS;

[assembly: RegisterObjectType(typeof(GDI_CategoryInfo), GDI_CategoryInfo.OBJECT_TYPE)]

namespace PIMS
{
    /// <summary>
    /// Data container class for <see cref="GDI_CategoryInfo"/>.
    /// </summary>
    [Serializable]
    public partial class GDI_CategoryInfo : AbstractInfo<GDI_CategoryInfo, IGDI_CategoryInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "pims.gdi_category";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(GDI_CategoryInfoProvider), OBJECT_TYPE, "PIMS.GDI_Category", "GDI_CategoryID", "GDI_CategoryLastModified", "GDI_CategoryGuid", "GDICategoryName", "GDICategoryName", null, null, null, null)
        {
            ModuleName = "PIMS",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Category ID.
        /// </summary>
        [DatabaseField]
        public virtual int GDI_CategoryID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("GDI_CategoryID"), 0);
            }
            set
            {
                SetValue("GDI_CategoryID", value);
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
        /// GDI category name.
        /// </summary>
        [DatabaseField]
        public virtual string GDICategoryName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GDICategoryName"), String.Empty);
            }
            set
            {
                SetValue("GDICategoryName", value, String.Empty);
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
        /// GDI category item order.
        /// </summary>
        [DatabaseField]
        public virtual int GDICategoryItemOrder
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("GDICategoryItemOrder"), 0);
            }
            set
            {
                SetValue("GDICategoryItemOrder", value, 0);
            }
        }


        /// <summary>
        /// Category guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid GDI_CategoryGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("GDI_CategoryGuid"), Guid.Empty);
            }
            set
            {
                SetValue("GDI_CategoryGuid", value);
            }
        }


        /// <summary>
        /// Category last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime GDI_CategoryLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("GDI_CategoryLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("GDI_CategoryLastModified", value);
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
        protected GDI_CategoryInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="GDI_CategoryInfo"/> class.
        /// </summary>
        public GDI_CategoryInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="GDI_CategoryInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public GDI_CategoryInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}