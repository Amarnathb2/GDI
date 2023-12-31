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
using CMS.CustomTables;
using CMS.CustomTables.Types.GDI;

[assembly: RegisterCustomTable(StatesItem.CLASS_NAME, typeof(StatesItem))]

namespace CMS.CustomTables.Types.GDI
{
	/// <summary>
	/// Represents a content item of type StatesItem.
	/// </summary>
	public partial class StatesItem : CustomTableItem
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "GDI.States";


		/// <summary>
		/// The instance of the class that provides extended API for working with StatesItem fields.
		/// </summary>
		private readonly StatesItemFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// State.
		/// </summary>
		[DatabaseField]
		public string State
		{
			get
			{
				return ValidationHelper.GetString(GetValue("State"), @"");
			}
			set
			{
				SetValue("State", value);
			}
		}


		/// <summary>
		/// Abbreviation.
		/// </summary>
		[DatabaseField]
		public string Abbreviation
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Abbreviation"), @"");
			}
			set
			{
				SetValue("Abbreviation", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with StatesItem fields.
		/// </summary>
		[RegisterProperty]
		public StatesItemFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with StatesItem fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class StatesItemFields : AbstractHierarchicalObject<StatesItemFields>
		{
			/// <summary>
			/// The content item of type StatesItem that is a target of the extended API.
			/// </summary>
			private readonly StatesItem mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="StatesItemFields" /> class with the specified content item of type StatesItem.
			/// </summary>
			/// <param name="instance">The content item of type StatesItem that is a target of the extended API.</param>
			public StatesItemFields(StatesItem instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// State.
			/// </summary>
			public string State
			{
				get
				{
					return mInstance.State;
				}
				set
				{
					mInstance.State = value;
				}
			}


			/// <summary>
			/// Abbreviation.
			/// </summary>
			public string Abbreviation
			{
				get
				{
					return mInstance.Abbreviation;
				}
				set
				{
					mInstance.Abbreviation = value;
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="StatesItem" /> class.
		/// </summary>
		public StatesItem() : base(CLASS_NAME)
		{
			mFields = new StatesItemFields(this);
		}

		#endregion
	}
}