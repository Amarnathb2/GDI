using CMS.Core;
using CMS.Helpers;

using GDI.Helpers;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Net;

namespace GDI.Controller
{
    public class SalesForceController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEventLogService _eventLogService;
        public const string _object_Manage_Lead_Contact = "Manage_Lead_Contact__e";
        public SalesForceController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }
        enum FormType
        {
            None,
            GDIContactUsForm = 27,
            GDISpecialPowderRequestForm = 26,
            GDICommoditiesOrder = 28,
            GDISpecialityPowderForm = 3,

        }

        [HttpPost]
        [Route("SalesForce/ManageLeadContact")]
        public string ManageLeadContact([FromBody] object param)
        {
            FormType vFormType = FormType.None;
            Dictionary<string, object> vDictParam = new Dictionary<string, object>();
            Dictionary<string, string> vDictResponse = new Dictionary<string, string>();
            string vJsonResult = string.Empty;
            try
            {
                if (param != null)
                {
                    Dictionary<string, object>? _param = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(param, string.Empty));
                    if (_param.ContainsKey("param"))
                    {
                        Dictionary<string, object>? dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(_param["param"], string.Empty));
                        if (dict.ContainsKey("data"))
                        {
                            vDictParam = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(dict["data"], string.Empty));
                            if (vDictParam != null && vDictParam.Count > 0)
                            {
                                if (vDictParam.ContainsKey("formName"))
                                {
                                    vFormType = (FormType)Enum.Parse(typeof(FormType), vDictParam["formName"].ToString());
                                }
                                switch (vFormType)
                                {
                                    case FormType.GDIContactUsForm:
                                        if (vDictParam.ContainsKey("mlc"))
                                        {
                                            Dictionary<string, object> dictObjMLC = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(vDictParam["mlc"], string.Empty));
                                            if (dictObjMLC != null && dictObjMLC.Count > 0)
                                            {
                                                Dictionary<string, string> vDictMCL = new Dictionary<string, string>();
                                                dictObjMLC.Add("type", _object_Manage_Lead_Contact);
                                                foreach (KeyValuePair<string, object> pair in dictObjMLC)
                                                {
                                                    vDictMCL.Add(pair.Key.ToString(), pair.Value.ToString());
                                                }
                                                vJsonResult = SFPardot.BulkUpsert(new List<Dictionary<string, string>>() { vDictMCL }, true);
                                                return vJsonResult;
                                            }
                                        }
                                        break;
                                    case FormType.GDISpecialPowderRequestForm:
                                        if (vDictParam.ContainsKey("mlc"))
                                        {
                                            Dictionary<string, object> dictObjMLC = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(vDictParam["mlc"], string.Empty));
                                            if (dictObjMLC != null && dictObjMLC.Count > 0)
                                            {
                                                Dictionary<string, string> vDictMCL = new Dictionary<string, string>();
                                                dictObjMLC.Add("type", _object_Manage_Lead_Contact);
                                                foreach (KeyValuePair<string, object> pair in dictObjMLC)
                                                {
                                                    vDictMCL.Add(pair.Key.ToString(), pair.Value.ToString());
                                                }
                                                vJsonResult = SFPardot.BulkUpsert(new List<Dictionary<string, string>>() { vDictMCL }, true);
                                                return vJsonResult;
                                            }
                                        }
                                        break;
                                    case FormType.GDICommoditiesOrder:
                                        if (vDictParam.ContainsKey("mlc"))
                                        {
                                            Dictionary<string, object> dictObjMLC = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(vDictParam["mlc"], string.Empty));
                                            if (dictObjMLC != null && dictObjMLC.Count > 0)
                                            {
                                                Dictionary<string, string> vDictMCL = new Dictionary<string, string>();
                                                dictObjMLC.Add("type", _object_Manage_Lead_Contact);
                                                foreach (KeyValuePair<string, object> pair in dictObjMLC)
                                                {
                                                    vDictMCL.Add(pair.Key.ToString(), pair.Value.ToString());
                                                }
                                                vJsonResult = SFPardot.BulkUpsert(new List<Dictionary<string, string>>() { vDictMCL }, true);
                                                return vJsonResult;
                                            }
                                        }
                                        break;
                                    case FormType.GDISpecialityPowderForm:

                                        if (vDictParam.ContainsKey("mlc"))
                                        {
                                            Dictionary<string, object> dictObjMLC = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(vDictParam["mlc"], string.Empty));
                                            if (dictObjMLC != null && dictObjMLC.Count > 0)
                                            {
                                                Dictionary<string, string> vDictMCL = new Dictionary<string, string>();
                                                dictObjMLC.Add("type", _object_Manage_Lead_Contact);
                                                foreach (KeyValuePair<string, object> pair in dictObjMLC)
                                                {
                                                    vDictMCL.Add(pair.Key.ToString(), pair.Value.ToString());
                                                }

                                                vJsonResult = SFPardot.BulkUpsert(new List<Dictionary<string, string>>() { vDictMCL }, true);

                                                return vJsonResult;
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(SalesForceController), nameof(ManageLeadContact), ex);
            }

            return SFPardot.DictToJson(vDictResponse);
        }

        //[System.Web.Services.WebMethod]
        [HttpPost]
        [Route("SalesForce/PostToSalesForce")]
        public string PostToSalesForce([FromBody] object param)
        {
            string sfUrl = string.Empty;
            string formData = string.Empty;
            try
            {

                if (param != null)
                {
                    Dictionary<string, object>? filterDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(param, string.Empty));
                    if (filterDict.ContainsKey("param"))
                    {
                        Dictionary<string, object>? dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ValidationHelper.GetString(filterDict["param"], string.Empty));
                        if (dict != null && dict.Count > 0)
                        {
                            if (dict.ContainsKey("sfUrl"))
                                sfUrl = dict["sfUrl"].ToString();

                            if (dict.ContainsKey("formData"))
                                formData = dict["formData"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(SalesForceController), nameof(PostToSalesForce), ex);
            }
            return SendToSalesForce(sfUrl, formData);
        }

        public string SendToSalesForce(string sfUrl, string formData)
        {
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(sfUrl + "?callback=" + formData);
            webreq.ContentType = "application/json; charset=utf-8";
            webreq.Accept = "text/json";
            webreq.Headers.Clear();
            webreq.Method = "POST";
            try
            {
                using (var streamWriter = new StreamWriter(webreq.GetRequestStream()))
                {
                    string json = formData;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                    HttpWebResponse response = (HttpWebResponse)webreq.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var httpResponse = response;
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            return result;
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                var httpResponse = wex.Response as HttpWebResponse;
                if (httpResponse != null)
                {
                    throw new ApplicationException(string.Format(
                        "Remote server call {0} {1} resulted in a http error {2} {3}.",
                        "SendToSalesForce",
                        "",
                        httpResponse.StatusCode,
                        httpResponse.StatusDescription), wex);
                }
                else
                {
                    throw new ApplicationException(string.Format(
                        "Remote server call {0} {1} resulted in an error.",
                        "SendToSalesForce",
                        ""), wex);
                }
            }
            return null;
        }
    }
}

