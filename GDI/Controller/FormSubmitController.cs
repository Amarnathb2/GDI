using CMS.Core;

using GDI.Components.Widgets.CommoditiesOrderForm;
using GDI.Components.Widgets.GeneralContactForm;
using GDI.Helpers;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace GDI.Controller
{
    public class FormSubmitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEventLogService _eventLogService;
        public const string _object_Manage_Lead_Contact = "Manage_Lead_Contact__e";
        public FormSubmitController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        [ValidateAntiForgeryToken]
        public string SubmitContactForm(ContactUsForm contactUs)
        {
            FormDetails data = JsonSerializer.Deserialize<FormDetails>(TempData["ContactUsForm"].ToString());
            Dictionary<string, string> vDictMCL = new Dictionary<string, string>();
            vDictMCL.Add("First_Name__c", contactUs.FirstName);
            vDictMCL.Add("Last_Name__c", contactUs.LastName);
            vDictMCL.Add("Title__c", contactUs.JobTitle);
            vDictMCL.Add("Email__c", contactUs.Email);
            vDictMCL.Add("Company__c", contactUs.Company);
            vDictMCL.Add("Phone__c", contactUs.Phone);
            vDictMCL.Add("Street__c", contactUs.Address);
            vDictMCL.Add("Country__c", contactUs.Country);
            vDictMCL.Add("City__c", contactUs.City);
            vDictMCL.Add("State__c", contactUs.State);
            vDictMCL.Add("Postal_Code__c", contactUs.ZipCode);
            vDictMCL.Add("Source__c", data.Origin);
            vDictMCL.Add("Tags__c", contactUs.OptIn == "1" ? data.Tags : "");
            vDictMCL.Add("External_Campaign_Id__c", data.ExternalCampaignId);
            vDictMCL.Add("Type__c", data.Type);
            vDictMCL.Add("type", _object_Manage_Lead_Contact);
            string vJsonResult = SFPardot.BulkUpsert(new List<Dictionary<string, string>>() { vDictMCL }, true);

            Dictionary<string, string> dist = new Dictionary<string, string>();
            dist.Add("RecordTypeId", data.RecordType);
            dist.Add("origin", data.Origin);
            dist.Add("GDI_Web_Form_First_Name_Int__c", contactUs.FirstName);
            dist.Add("GDI_Web_Form_Last_Name_Int__c", contactUs.LastName);
            dist.Add("JobTitle__c", contactUs.JobTitle);
            dist.Add("SuppliedEmail", contactUs.Email);
            dist.Add("SuppliedCompany", contactUs.Company);
            dist.Add("SuppliedPhone", contactUs.Phone);
            dist.Add("Street_Address__c", contactUs.Address);
            dist.Add("Country__c", contactUs.Country);
            dist.Add("City__c", contactUs.City);
            dist.Add("State__c", contactUs.State);
            dist.Add("Postal_Zip_Code__c", contactUs.ZipCode);
            dist.Add("Opt_In_to_News_and_Promotions__c", contactUs.OptIn == "1" ? "true" : "false");
            dist.Add("Reason_Comments__c", contactUs.Comments);
            SFCommon.SendCaseToSF(dist);
            return string.Empty;
        }

        [ValidateAntiForgeryToken]
        public string SubmitCommoditiesOrderForm(CommoditiesOrderForm commoditiesOrderForm)
        {
            FormDetails data = JsonSerializer.Deserialize<FormDetails>(TempData["CommoditiesOrderForm"].ToString());
            Dictionary<string, string> dist = new Dictionary<string, string>();
            dist.Add("RecordTypeId", data.RecordType);
            dist.Add("origin", data.Origin);
            dist.Add("GDI_Web_Form_First_Name_Int__c", commoditiesOrderForm.FirstName);
            dist.Add("GDI_Web_Form_Last_Name_Int__c", commoditiesOrderForm.LastName);
            dist.Add("JobTitle__c", commoditiesOrderForm.JobTitle);
            dist.Add("SuppliedEmail", commoditiesOrderForm.Email);
            dist.Add("SuppliedCompany", commoditiesOrderForm.Company);
            dist.Add("SuppliedPhone", commoditiesOrderForm.Phone);
            dist.Add("City__c", commoditiesOrderForm.City);
            dist.Add("State__c", commoditiesOrderForm.State);
            dist.Add("Postal_Zip_Code__c", commoditiesOrderForm.ZipCode);
            dist.Add("Opt_In_to_News_and_Promotions__c", commoditiesOrderForm.OptIn == "1" ? "true" : "false");
            dist.Add("Reason_Comments__c", commoditiesOrderForm.Comments);
            dist.Add("Description", commoditiesOrderForm.Description);
            SFCommon.SendCaseToSF(dist);
            return string.Empty;
        }

        [ValidateAntiForgeryToken]
        public string SubmitProductRequestForm(ProductRequestForm productRequestForm)
        {
            FormDetails data = JsonSerializer.Deserialize<FormDetails>(TempData["ProductRequestForm"].ToString());
            Dictionary<string, string> vDictMCL = new Dictionary<string, string>();
            vDictMCL.Add("First_Name__c", productRequestForm.FirstName);
            vDictMCL.Add("Last_Name__c", productRequestForm.LastName);
            vDictMCL.Add("Title__c", productRequestForm.JobTitle);
            vDictMCL.Add("Email__c", productRequestForm.Email);
            vDictMCL.Add("Company__c", productRequestForm.Company);
            vDictMCL.Add("Phone__c", productRequestForm.Phone);
            vDictMCL.Add("Street__c", productRequestForm.Address);
            vDictMCL.Add("City__c", productRequestForm.City);
            vDictMCL.Add("State__c", productRequestForm.State);
            vDictMCL.Add("Postal_Code__c", productRequestForm.ZipCode);
            vDictMCL.Add("Source__c", data.Origin);
            vDictMCL.Add("Tags__c", productRequestForm.OptIn == "1" ? data.Tags : "");
            vDictMCL.Add("External_Campaign_Id__c", data.ExternalCampaignId);
            vDictMCL.Add("Type__c", data.Type);
            vDictMCL.Add("type", _object_Manage_Lead_Contact);
            string vJsonResult = SFPardot.BulkUpsert(new List<Dictionary<string, string>>() { vDictMCL }, true);

            Dictionary<string, string> dist = new Dictionary<string, string>();
            dist.Add("RecordTypeId", data.RecordType);
            dist.Add("origin", data.Origin);
            dist.Add("GDI_Web_Form_First_Name_Int__c", productRequestForm.FirstName);
            dist.Add("GDI_Web_Form_Last_Name_Int__c", productRequestForm.LastName);
            dist.Add("JobTitle__c", productRequestForm.JobTitle);
            dist.Add("SuppliedEmail", productRequestForm.Email);
            dist.Add("SuppliedCompany", productRequestForm.Company);
            dist.Add("SuppliedPhone", productRequestForm.Phone);
            dist.Add("Street_Address__c", productRequestForm.Address);
            dist.Add("City__c", productRequestForm.City);
            dist.Add("State__c", productRequestForm.State);
            dist.Add("Postal_Zip_Code__c", productRequestForm.ZipCode);
            dist.Add("Opt_In_to_News_and_Promotions__c", productRequestForm.OptIn == "1" ? "true" : "false");
            dist.Add("Reason_Comments__c", productRequestForm.Comments);
            dist.Add("Description", productRequestForm.Description);
            SFCommon.SendCaseToSF(dist);
            return string.Empty;
        }

        [ValidateAntiForgeryToken]
        public string SubmitSpecialtyPowderRequestForm(ProductRequestForm specialtyPowderRequestForm)
        {
            FormDetails data = JsonSerializer.Deserialize<FormDetails>(TempData["SpecialityProductRequestForm"].ToString());
            Dictionary<string, string> dist = new Dictionary<string, string>();
            dist.Add("RecordTypeId", data.RecordType);
            dist.Add("origin", data.Origin);
            dist.Add("GDI_Web_Form_First_Name_Int__c", specialtyPowderRequestForm.FirstName);
            dist.Add("GDI_Web_Form_Last_Name_Int__c", specialtyPowderRequestForm.LastName);
            dist.Add("JobTitle__c", specialtyPowderRequestForm.JobTitle);
            dist.Add("SuppliedEmail", specialtyPowderRequestForm.Email);
            dist.Add("SuppliedCompany", specialtyPowderRequestForm.Company);
            dist.Add("SuppliedPhone", specialtyPowderRequestForm.Phone);
            dist.Add("Street_Address__c", specialtyPowderRequestForm.Address);
            dist.Add("City__c", specialtyPowderRequestForm.City);
            dist.Add("State__c", specialtyPowderRequestForm.State);
            dist.Add("Postal_Zip_Code__c", specialtyPowderRequestForm.ZipCode);
            dist.Add("Opt_In_to_News_and_Promotions__c", specialtyPowderRequestForm.OptIn == "1" ? "true" : "false");
            dist.Add("Reason_Comments__c", specialtyPowderRequestForm.Comments);
            dist.Add("Description", specialtyPowderRequestForm.Description);
            SFCommon.SendCaseToSF(dist);
            return string.Empty;
        }
    }
}
