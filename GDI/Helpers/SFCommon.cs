using CMS.SiteProvider;
using CMS.Helpers;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;
using CMS.DocumentEngine;
using CMS.Membership;
using Newtonsoft.Json;
using CMS.Core;
using CMS.DataEngine;

namespace GDI.Helpers
{
    public static class SFCommon
    {
        #region Properties 
        public const string _apiURLSOObjects = "/services/data/v54.0/sobjects/";
        public const string _apiURLCompositeTree = ":/services/data/v43.0/composite/sobjects";
        public const string _apiURLCompositeTreeBulk = "/services/data/v38.0/sobjects";
        public const string _apiURLQuery = "/services/data/v54.0/query/?q=";

        public const string _object_Contact = "contact";
        public const string _object_Lead = "lead";
        public const string _object_PreferenceQuestionResult = "Preference_Question_Result__c";
        public const string _object_Subscription = "Subscription__c";
        public const string _object_Interest = "Interest__c";
        public const string _object_Case = "case";

        public const string _field_FirstName = "FirstName";
        public const string _field_LastName = "LastName";
        public const string _field_Email = "Email";
        public const string _field_Phone = "Phone";
        public const string _field_MailingStreet = "MailingStreet";
        public const string _field_MailingCity = "MailingCity";
        public const string _field_MailingState = "MailingState";
        public const string _field_MailingPostalCode = "MailingPostalCode";
        public const string _field_MailingCountry = "MailingCountry";
        public const string _field_Street = "Street";
        public const string _field_City = "City";
        public const string _field_State = "State";
        public const string _field_PostalCode = "PostalCode";
        public const string _field_Country = "Country";
        public const string _field_Contact__c = "Contact__c";
        public const string _field_Lead__c = "Lead__c";
        public const string _field_Campaign__c = "Campaign__c";
        public const string _field_Session_id__c = "Session_id__c";
        public const string _field_Preference_Question__c = "Preference_Question__c";
        public const string _field_Preference_Answer__c = "Preference_Answer__c";
        public const string _field_Source__c = "Source__c";
        public const string _field_type = "type";
        public const string _field_Opt_In__c = "Opt_In__c";
        public const string _field_Opt_In_Source__c = "Opt_In_Source__c";
        public const string _field_Opt_In_Date__c = "Opt_In_Date__c";
        public const string _field_Subscription2__c = "Subscription2__c";
        public const string _field_Interest_Selection__c = "Interest_Selection__c";
        private const string _field_OwnerId = "OwnerId";
        private const string _field_RecordTypeId = "RecordTypeId";
        private const string _field_Company = "Company";
        private const string _field_Id = "Id";
        public const string _field_Contact_Web_Operation_Type__c = "Web_Operation_Type__c";
        public const string _field_Contact_Web_RecipientEmailAddress__c = "Web_RecipientEmailAddress__c";
        public const string _field_Contact_Description = "Description";
        private const string _field_LeadSource = "LeadSource";

        #endregion

        #region Core Methods
        /// <summary>
        /// Get the settings key value based on the site and nodeAliasPath
        /// </summary>
        /// <param name="_isProduction"></param>
        /// <param name="_settingKey"></param>
        /// <returns></returns>
        public static DataSet GetSettingsKey()
        {
            DataSet ds = new DataSet();
            try
            {
                string NodeAliasPath = string.Empty;
                int SiteID = SiteContext.CurrentSiteID;
                string strWhere = string.Empty;

                strWhere = "NodeSiteID =" + SiteID;

                TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser); // View_CMS_tree_joined 
                var query = tree.SelectNodes(); //Get its query
                query.Type("LOL.SF_Configuration"); //join with page type
                query.Path(NodeAliasPath, PathTypeEnum.Single);
                query.WhereCondition = strWhere;

                //query.Where("NodeAliasPath")
                query.Published(true);
                //query.PublishedVersion(true);​
                query.Columns("UserName, Password, AuthURL, ConsumerKey, ConsumerSecret, SecurityToken");
                ds = query.Result;
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(GetSettingsKey), ex);
            }
            return ds;
        }

        /// <summary>
        /// Get the settings key value with the site specific overriding the global settings
        /// </summary>
        /// <param name="_isProduction"></param>
        /// <param name="_settingKey"></param>
        /// <returns></returns>
        public static string GetSettingsKey(bool _isProduction, string _settingKey)
        {
            string _keyFirst = "SF_";
            string _keyPart = (_isProduction ? "Production_" : "Sandbox_");
            SettingsKeyInfo _currentSiteKey = new SettingsKeyInfo();
            SettingsKeyInfo _globalSiteKey = new SettingsKeyInfo();
            try
            {
                _currentSiteKey = SettingsKeyInfoProvider.GetSettingsKeyInfo(SiteContext.CurrentSiteName + "." + _keyFirst + _keyPart + _settingKey);
                _globalSiteKey = SettingsKeyInfoProvider.GetSettingsKeyInfo(_keyFirst + _keyPart + _settingKey);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(GetSettingsKey), ex);
                return string.Empty;
            }
            return (_currentSiteKey == null || _currentSiteKey.KeyValue == null ? (_globalSiteKey == null ? "" : _globalSiteKey.KeyValue) : _currentSiteKey.KeyValue);
        }

        /// <summary>
        /// Method to authenticate and return the authentication params in a dataset.
        /// </summary>
        /// <param name="_cs"></param>
        /// <returns></returns>
        public static DataSet AuthenticateUsingHTTPClient(CacheSettings _cs)
        {
            string userName = string.Empty;
            string password = string.Empty;
            string authURL = string.Empty;
            string consumerKey = string.Empty;
            string consumerSecret = string.Empty;
            string SecurityToken = string.Empty;

            DataSet _dsResult = new DataSet();
            DataTable _dt = new DataTable();
            _dt.Columns.Add("access_token");
            _dt.Columns.Add("instance_url");

            string _errorType = string.Empty;
            string _errorDescription = string.Empty;
            try
            {
                bool _IsProduction = ValidationHelper.GetBoolean(GetSettingsKey(true, "IsProduction"), false);

                var _dictionaryForLogin = new Dictionary<string, string>
        {
            {"grant_type","password" },
            {"client_id",ValidationHelper.GetString(GetSettingsKey(_IsProduction, "ConsumerKey"), string.Empty) },
            {"client_secret",ValidationHelper.GetString(GetSettingsKey(_IsProduction, "ConsumerSecret"), string.Empty) },
            { "username",ValidationHelper.GetString(GetSettingsKey(_IsProduction, "Username"), string.Empty)},
            { "password",ValidationHelper.GetString(GetSettingsKey(_IsProduction, "Password"), string.Empty)+ValidationHelper.GetString(GetSettingsKey(_IsProduction, "SecurityToken"), string.Empty)}
        };

                HttpClient _httpClient = new HttpClient();
                HttpContent _httpContent = new FormUrlEncodedContent(_dictionaryForLogin);
                HttpResponseMessage _httpResponse = _httpClient.PostAsync(authURL, _httpContent).GetAwaiter().GetResult();
                string _responseMessage = _httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                JObject _jobject = JObject.Parse(_responseMessage);
                if (_httpResponse.IsSuccessStatusCode && _jobject.Count > 0)
                {
                    DataRow _dr = _dt.NewRow();
                    _dr["access_token"] = ValidationHelper.GetString(_jobject["access_token"], string.Empty);
                    _dr["instance_url"] = ValidationHelper.GetString(_jobject["instance_url"], string.Empty);
                    _dt.Rows.Add(_dr);
                    _dsResult.Tables.Add(_dt);
                }
                else
                {
                    _errorType = (string)_jobject["error"];
                    _errorDescription = (string)_jobject["error_description"];
                }

                if (_cs.Cached)
                    _cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] { "cms.settingskey|byname|SF_Sandbox_Username", "cms.settingskey|byname|SF_Sandbox_Password", "cms.settingskey|byname|SF_Sandbox_AuthURL", "cms.settingskey|byname|SF_Sandbox_ConsumerKey", "cms.settingskey|byname|SF_Sandbox_ConsumerSecret", "cms.settingskey|byname|SF_Sandbox_SecurityToken", "cms.settingskey|byname|SF_Production_IsProduction", "cms.settingskey|byname|SF_Production_Username", "cms.settingskey|byname|SF_Production_Password", "cms.settingskey|byname|SF_Production_AuthURL", "cms.settingskey|byname|SF_Production_ConsumerKey", "cms.settingskey|byname|SF_Production_ConsumerSecret", "cms.settingskey|byname|SF_Production_SecurityToken" });
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(AuthenticateUsingHTTPClient), ex);
            }
            return _dsResult;
        }

        /// <summary>
        /// Get authentication params from the cached dataset
        /// </summary>
        /// <param name="_authToken"></param>
        /// <param name="_instanceURL"></param>
        public static bool GetAuthenticationParams(out string _authToken, out string _instanceURL)
        {
            _authToken = string.Empty;
            _instanceURL = string.Empty;

            DataSet ds = CacheHelper.Cache(cs => AuthenticateUsingHTTPClient(cs), new CacheSettings(10, SiteContext.CurrentSiteName.Replace(".", "").Replace(" ", "") + "_SFPardot_AuthParams"));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _authToken = ValidationHelper.GetString(ds.Tables[0].Rows[0]["access_token"], string.Empty);
                _instanceURL = ValidationHelper.GetString(ds.Tables[0].Rows[0]["instance_url"], string.Empty);
                return true;
            }
            return false;
        }
        //Meth
        public static string GetSelectedFieldsUsingQuery(string _query)
        {
            string _authToken;
            string _instanceURL;
            //List<Dictionary<string, string>> lstResponseDict = new List<Dictionary<string, string>>();
            //Dictionary<string, string> responseDict = new Dictionary<string, string>();
            string _httpResponse = string.Empty;
            HttpResponseMessage _httpResponseMessage = new HttpResponseMessage();
            try
            {
                GetAuthenticationParams(out _authToken, out _instanceURL);

                HttpClient _httpClient = new HttpClient();
                string _restCallURL = _instanceURL + _apiURLQuery + _query;
                HttpRequestMessage _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _restCallURL);
                _httpRequestMessage.Headers.Add("authorization", "Bearer " + _authToken);
                _httpResponseMessage = _httpClient.SendAsync(_httpRequestMessage).GetAwaiter().GetResult();
                _httpResponse = _httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(GetSelectedFieldsUsingQuery), ex);
            }
            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                return _httpResponse;
            }
            else throw new System.Net.WebException(_httpResponse);
        }

        /// <summary>
        /// Convert the httpResponse from salesforce to a Dictionary for the specified fields
        /// </summary>
        /// <param name="pHttpResponse"></param>
        /// <param name="pDictFields"></param>
        /// <returns></returns>

        public static List<Dictionary<string, string>> ConvertHttpResponseToDictionary(string pHttpResponse, Dictionary<string, string> pDictFields)
        {
            List<Dictionary<string, string>> lstResponseDict = new List<Dictionary<string, string>>();
            Dictionary<string, string> responseDict = new Dictionary<string, string>();
            try
            {
                JObject _jObject = JObject.Parse(pHttpResponse);
                JToken _jTokens = _jObject["records"];
                if (_jTokens != null && _jTokens.Children().Count() > 0)
                {
                    foreach (JToken _token in _jTokens.Children())
                    {
                        responseDict = new Dictionary<string, string>();
                        foreach (JProperty _property in _token)
                        {
                            if (pDictFields.ContainsKey(_property.Name.ToString().ToLower()))
                            {
                                responseDict.Add(_property.Name.ToString().ToLower(), _property.Value.ToString());
                            }
                        }

                        if (responseDict != null && responseDict.Count > 0)
                        {
                            lstResponseDict.Add(responseDict);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(ConvertHttpResponseToDictionary), ex);
            }
            return lstResponseDict;
        }

        /// <summary>
        /// Upserts the record in Salesforce based on the idToUpsert or creates a new one if idToUpsert is blank
        /// </summary>
        /// <param name="_objectName"></param>
        /// <param name="_stringContent"></param>
        /// <param name="_idToUpsert"></param>
        /// <returns></returns>
        public static string Upsert(string _objectName, string _stringContent, string _idToUpsert = "")
        {
            bool _isIDPresent = (_idToUpsert.Trim() != string.Empty || _idToUpsert.Trim() != "");
            HttpMethod _httpMethod = _isIDPresent ? new HttpMethod("PATCH") : HttpMethod.Post;
            string _restURL = _apiURLSOObjects + _objectName + (_isIDPresent ? "/" + _idToUpsert : "");
            return PostDataToSalesforce(_httpMethod, _objectName, _stringContent, _restURL);
        }

        /// <summary>
        /// Upsert multiple records a time
        /// </summary>
        /// <param name="_lstDict"></param>
        /// <returns></returns>
        public static string BulkUpsert(List<Dictionary<string, string>> _lstDict)
        {
            //return PostDataToSalesforce(HttpMethod.Post, _objectName, _stringContent, (_apiURLCompositeTree));
            return PostDataToSalesforceBulk(BulkDictToJson(_lstDict));
        }

        //Send data to salesforce
        public static string PostDataToSalesforce(HttpMethod _httpMethod, string _objectName, string _stringContent, string _restURL)
        {
            string _authToken;
            string _instanceURL;
            string _message = string.Empty;
            try
            {
                GetAuthenticationParams(out _authToken, out _instanceURL);

                HttpClient _httpClient = new HttpClient();

                string _restCallURL = _instanceURL + _restURL;

                HttpRequestMessage _httpRequestMessage = new HttpRequestMessage(_httpMethod, _restCallURL);
                _httpRequestMessage.Headers.Add("authorization", "Bearer " + _authToken);
                _httpRequestMessage.Content = new StringContent(_stringContent, Encoding.UTF8, "application/json");

                HttpResponseMessage _httpResponseMessage = _httpClient.SendAsync(_httpRequestMessage).GetAwaiter().GetResult();

                string _httpResponse = _httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (_httpResponseMessage.IsSuccessStatusCode)
                {
                    if (_httpResponse != "")
                    {
                        JObject _jObject = JObject.Parse(_httpResponse);
                        JToken _jTokens = _jObject["id"];
                        if (_jTokens != null)
                        {
                            _message = "New " + _objectName + " created successfully with id = " + _jTokens.ToString();
                            return _jTokens.ToString();
                        }
                    }
                    else
                        _message = _objectName + " upserted with the new question";
                }
                else
                    throw new System.Net.WebException(_httpResponse);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(PostDataToSalesforce), ex);
            }
            return string.Empty;
        }

        public static string PostDataToSalesforceBulk(string _stringContent)
        {
            string _authToken;
            string _instanceURL;
            string _message = string.Empty;
            string _httpResponse = string.Empty;
            try
            {
                GetAuthenticationParams(out _authToken, out _instanceURL);

                HttpClient _httpClient = new HttpClient();

                //string _restCallURL = _instanceURL + _restURL;
                string _restCallURL = _instanceURL;

                HttpRequestMessage _httpRequestMessage = new HttpRequestMessage(new HttpMethod("POST"), _restCallURL + "/services/data/v38.0/composite/");
                _httpRequestMessage.Headers.Add("authorization", "Bearer " + _authToken);
                _httpRequestMessage.Content = new StringContent(_stringContent, Encoding.UTF8, "application/json");

                HttpResponseMessage _httpResponseMessage = _httpClient.SendAsync(_httpRequestMessage).GetAwaiter().GetResult();
                _httpResponse = _httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(PostDataToSalesforceBulk), ex);
            }
            return _httpResponse;
        }

        /// <summary>
        /// Converts a Dictionary object to Json
        /// </summary>
        /// <param name="_Dict"></param>
        /// <returns></returns>
        public static string DictToJson(Dictionary<string, string> _Dict)
        {
            var _json = new StringBuilder();
            try
            {
                foreach (var key in _Dict.Keys)
                {
                    if (_json.Length != 0)
                        _json = _json.Append(",\n");

                    _json.AppendFormat("\"{0}\" : \"{1}\"", key, _Dict[key]);
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(DictToJson), ex);
            }
            return "{" + _json.ToString() + "}";
        }

        /// <summary>
        /// Converts a list of Dictionary object to Json
        /// </summary>
        /// <param name="_lstDict"></param>
        /// <returns></returns>
        public static string BulkDictToJson(List<Dictionary<string, string>> _lstDict)
        {
            var _json = new StringBuilder();
            var _jsonBody = new StringBuilder();
            int i = 0;
            try
            {
                _json.Append("{\"allOrNone\" : false");
                _json = _json.Append(",\n");
                _json = _json.Append("\"compositeRequest\" :[");
                _json = _json.Append("\n");
                foreach (var dict in _lstDict)
                {
                    if (i != 0)
                        _json = _json.Append(",\n");
                    else
                    {
                        _json = _json.Append("\n");
                    }
                    _json = _json.Append("{");

                    int j = 0;
                    //_json.Append("{");


                    if (dict.ContainsKey(_field_Id))
                    {
                        _json.Append("\"method\" : \"PATCH\"");
                        _json = _json.Append(",\n");
                        _json.Append("\"url\" : \"" + _apiURLCompositeTreeBulk + "/" + dict[_field_type] + "/" + dict[_field_Id] + "\"");
                    }
                    else
                    {
                        _json.Append("\"method\" : \"POST\"");
                        _json = _json.Append(",\n");
                        _json.Append("\"url\" : \"" + _apiURLCompositeTreeBulk + "/" + dict[_field_type] + "\"");
                    }

                    _json = _json.Append(",\n");
                    _json.Append("\"referenceId\" : \"ref" + i + "\"");
                    _json = _json.Append(",\n");

                    _jsonBody = new StringBuilder();
                    _jsonBody = _jsonBody.Append("\"body\" :{");
                    foreach (var key in dict.Keys)
                    {
                        if (key.ToLower() == "id" || key.ToLower() == "type")
                        {
                            continue;
                        }
                        if (j != 0)
                            _jsonBody = _jsonBody.Append(",\n");
                        _jsonBody.AppendFormat("\"{0}\" : \"{1}\"", key, dict[key]);
                        j++;
                    }
                    _jsonBody = _jsonBody.Append("\n}");
                    _json.Append(_jsonBody);
                    _json.Append("\n}");
                    i++;
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(BulkDictToJson), ex);
            }
            return "" + _json.ToString() + "]}";
        }

        /// <summary>
        /// Converts DataTable to List
        /// </summary>
        /// <param name="_dt"></param>
        /// <returns></returns>
        public static List<string> ConvertDataTableToList(DataTable _dt)
        {
            List<string> list = new List<string>();
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (DataRow row in _dt.Rows)
                    {
                        list.Add(row["value"] + ";" + row["text"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(ConvertDataTableToList), ex);
            }
            return list;
        }

        /// <summary>
        /// Converts a datatable to dictionary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ConvertDataTableToDictionary(DataTable dt)
        {
            return dt.AsEnumerable()
              .ToDictionary<DataRow, string, object>(row => row.Field<string>(0),
                                        row => row.Field<object>(1));
        }

        /// <summary>
        /// Converts a Lsit of dictionary JSON
        /// </summary>
        /// <param name="pListDict"></param>
        /// <returns></returns>
        public static String ConvertListOfDictionaryToJson(List<Dictionary<string, string>> pListDict)
        {
            List<Dictionary<string, string>> testDictionary = new List<Dictionary<string, string>>();
            string json = string.Empty;
            try
            {
                json = JsonConvert.SerializeObject(pListDict);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(ConvertListOfDictionaryToJson), ex);
            }
            return json;
        }
        #endregion

        #region Customizable Methods

        /// <summary>
        /// Fetch account details based on the firefly id
        /// </summary>
        /// <param name="pFirefly"></param>
        /// <returns>JSON string</returns>
        public static string SelectAccountIdByFireFly(string pFirefly)
        {
            Dictionary<string, string> dictFields = new Dictionary<string, string>();
            string _query = string.Empty;
            List<Dictionary<string, string>> lstDictResponse = new List<Dictionary<string, string>>();
            string _httpResponse = string.Empty;
            try
            {
                _query = "SELECT id, name, billingState, billingCity, billingStreet, billingPostalCode, Segment__c from Account Where Firefly_Id__c = '" + pFirefly + "'";

                _httpResponse = GetSelectedFieldsUsingQuery(_query);
                //lstDictResponse = ConvertHttpResponseToDictionary(_httpResponse, dictFields);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(SelectAccountIdByFireFly), ex);
            }
            //return ConvertListOfDictionaryToJson(lstDictResponse);
            return _httpResponse;
        }

        /// <summary>
        /// Inserts a case record
        /// </summary>
        /// <param name="pDictCase"></param>
        /// <returns>JSON string</returns>
        public static string SendCaseToSF(Dictionary<string, string> pDictCase)
        {
            string _stringContent = DictToJson(pDictCase);

            return Upsert(_object_Case, _stringContent);
        }

        /// <summary>
        /// Check for duplicate Cases based on firefly id
        /// </summary>
        /// <param name="pOrigin"></param>
        /// <param name="pFirefly"></param>
        /// <returns>JSON string</returns>
        public static List<Dictionary<string, string>> ValidateCaseByFireFly(string pOrigin, string pFirefly)
        {
            List<Dictionary<string, string>> lstDictResponse = new List<Dictionary<string, string>>();
            Dictionary<string, string> dictFields = new Dictionary<string, string>();
            string _query = string.Empty;
            string _httpResponse = string.Empty;
            try
            {
                _query = "SELECT COUNT(Id) rowCount FROM Case WHERE Origin = '" + pOrigin + "' AND UTM_Parameter__c = '" + pFirefly + "'";

                _httpResponse = GetSelectedFieldsUsingQuery(_query);

                dictFields.Add("rowcount", "");
                lstDictResponse = ConvertHttpResponseToDictionary(_httpResponse, dictFields);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFCommon), nameof(SelectAccountIdByFireFly), ex);
            }
            return lstDictResponse;
        }
        #endregion
    }
}
