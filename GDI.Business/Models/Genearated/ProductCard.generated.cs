//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.GDI;

[assembly: RegisterDocumentType(ProductCard.CLASS_NAME, typeof(ProductCard))]

namespace CMS.DocumentEngine.Types.GDI
{
	/// <summary>
	/// Represents a content item of type ProductCard.
	/// </summary>
	public partial class ProductCard : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "GDI.ProductCard";


		/// <summary>
		/// The instance of the class that provides extended API for working with ProductCard fields.
		/// </summary>
		private readonly ProductCardFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// ProductCardID.
		/// </summary>
		[DatabaseIDField]
		public int ProductCardID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("ProductCardID"), 0);
			}
			set
			{
				SetValue("ProductCardID", value);
			}
		}


		/// <summary>
		/// Image.
		/// </summary>
		[DatabaseField]
		public string Image
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Image"), @"");
			}
			set
			{
				SetValue("Image", value);
			}
		}


		/// <summary>
		/// Image Alt Text.
		/// </summary>
		[DatabaseField]
		public string ImageAltText
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ImageAltText"), @"");
			}
			set
			{
				SetValue("ImageAltText", value);
			}
		}


		/// <summary>
		/// Product Title.
		/// </summary>
		[DatabaseField]
		public string ProductTitle
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ProductTitle"), @"");
			}
			set
			{
				SetValue("ProductTitle", value);
			}
		}


		/// <summary>
		/// Background Color.
		/// </summary>
		[DatabaseField]
		public string BackgroundColor
		{
			get
			{
				return ValidationHelper.GetString(GetValue("BackgroundColor"), @"");
			}
			set
			{
				SetValue("BackgroundColor", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with ProductCard fields.
		/// </summary>
		[RegisterProperty]
		public ProductCardFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with ProductCard fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class ProductCardFields : AbstractHierarchicalObject<ProductCardFields>
		{
			/// <summary>
			/// The content item of type ProductCard that is a target of the extended API.
			/// </summary>
			private readonly ProductCard mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="ProductCardFields" /> class with the specified content item of type ProductCard.
			/// </summary>
			/// <param name="instance">The content item of type ProductCard that is a target of the extended API.</param>
			public ProductCardFields(ProductCard instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// ProductCardID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.ProductCardID;
				}
				set
				{
					mInstance.ProductCardID = value;
				}
			}


			/// <summary>
			/// Image.
			/// </summary>
			public string Image
			{
				get
				{
					return mInstance.Image;
				}
				set
				{
					mInstance.Image = value;
				}
			}


			/// <summary>
			/// Image Alt Text.
			/// </summary>
			public string ImageAltText
			{
				get
				{
					return mInstance.ImageAltText;
				}
				set
				{
					mInstance.ImageAltText = value;
				}
			}


			/// <summary>
			/// Product Title.
			/// </summary>
			public string ProductTitle
			{
				get
				{
					return mInstance.ProductTitle;
				}
				set
				{
					mInstance.ProductTitle = value;
				}
			}


			/// <summary>
			/// Background Color.
			/// </summary>
			public string BackgroundColor
			{
				get
				{
					return mInstance.BackgroundColor;
				}
				set
				{
					mInstance.BackgroundColor = value;
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductCard" /> class.
		/// </summary>
		public ProductCard() : base(CLASS_NAME)
		{
			mFields = new ProductCardFields(this);
		}

		#endregion
	}
}