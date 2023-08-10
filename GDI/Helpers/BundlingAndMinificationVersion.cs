namespace GDI.Helpers
{
    public static class BundlingAndMinificationVersion
    {
        public static Dictionary<string, string> GetVersionedFileUrls(Dictionary<string, string> fileMappings)
        {
            Dictionary<string, string> versionedUrls = new();

            foreach (var fileMapping in fileMappings)
            {
                string filekey = fileMapping.Key;
                string filePath = fileMapping.Value;
                string versionedUrl = GetVersionedFileUrl(filekey, filePath);
                versionedUrls.Add(filekey, versionedUrl);
            }
            return versionedUrls;
        }
        public static string GetVersionedFileUrl(string filekey, string filePath)
        {
            string versionNumber = GetFileLastModified(filePath);
            return $"{filekey}?v={versionNumber}";
        }
        public static string GetFileLastModified(string path)
        {
            DateTime lastModified = File.GetLastWriteTime(path);
            return lastModified.ToString("yyyyMMddHHmmss");
        }
    }
}