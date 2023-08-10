using CMS;
using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.IO;
using CMS.SiteProvider;

using GDI;

[assembly: RegisterModule(typeof(CustomModule))]
namespace GDI
{
    public class CustomModule : Module
    {
        private const string CMSConnectionString = "CMSConnectionString";
        private const string CMSAzureAccountName = "CMSAzureAccountName";
        public CustomModule() : base("")
        {
        }

        protected override void OnPreInit()
        {
            base.OnPreInit();
            ApplicationEvents.PreInitialized.Execute += PreInitialized_Execute;
        }


        private void PreInitialized_Execute(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(CMSConnectionString)))
                {
                    ConnectionHelper.ConnectionString = Environment.GetEnvironmentVariable(CMSConnectionString);
                }
                string SiteName = SiteInfo.Provider.Get("GDI").SiteName;
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(CMSAzureAccountName)) && SettingsKeyInfoProvider.GetBoolValue(SiteName + ".IsConnectToBlob") && !string.IsNullOrEmpty(SiteName))
                {

                    var mediaProvider = StorageProvider.CreateAzureStorageProvider();
                    mediaProvider.CustomRootPath = SettingsKeyInfoProvider.GetValue(SiteName + ".BlobStorageContainer");
                    mediaProvider.PublicExternalFolderObject = true;
                    StorageHelper.MapStoragePath("~/" + SiteName + "/media/", mediaProvider);
                    Service.Resolve<IEventLogService>().LogInformation("CustomModule", "AzureKeyVault", "Connected To Blob");
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException("PreInitialized_Execute", "KeyVault", ex);
            }
        }
    }
}