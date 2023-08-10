using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;
using CMS.EventLog;
using CMS.Core;

namespace GDI.Helpers
{
    public static class SFPardot
    {
        #region Properties 
        //public const string _apiURLSOObjects = "/services/data/v20.0/sobjects/";
        //public const string _apiURLCompositeTree = ":/services/data/v43.0/composite/sobjects";
        public const string _apiURLSOObjects = "/services/data/v54.0/sobjects/";
        public const string _apiURLCompositeTreeBulk = "/services/data/v38.0/sobjects";
        public const string _apiURLQuery = "/services/data/v54.0/query/?q=";

        public const string _object_Contact = "contact";
        public const string _object_Lead = "lead";
        public const string _object_PreferenceQuestionResult = "Preference_Question_Result__c";
        public const string _object_Subscription = "Subscription__c";
        public const string _object_Interest = "Interest__c";

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
        public const string _field_Web_Comments__c = "Web_Comments__c";
        public const string _field_Opted_Out_Of_Email__c = "HasOptedOutOfEmail";
        public const string _field_Unsubscribe_Reason__c = "Unsubscribe_Reason__c";
        public const string _field_Opt_Out_Reason__c = "Opt_Out_Reason__c";

        #endregion

        #region Core Methods
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
            SettingsKeyInfo _currentSiteKey = SettingsKeyInfoProvider.GetSettingsKeyInfo(SiteContext.CurrentSiteName + "." + _keyFirst + _keyPart + _settingKey);
            SettingsKeyInfo _globalSiteKey = SettingsKeyInfoProvider.GetSettingsKeyInfo(_keyFirst + _keyPart + _settingKey);

            return (_currentSiteKey == null || _currentSiteKey.KeyValue == null ? (_globalSiteKey == null ? "" : _globalSiteKey.KeyValue) : _currentSiteKey.KeyValue);
        }

        /// <summary>
        /// Method to authenticate and return the authentication params in a dataset.
        /// </summary>
        /// <param name="_cs"></param>
        /// <returns></returns>
        public static DataSet AuthenticateUsingHTTPClient(CacheSettings _cs)
        {
            DataSet _dsResult = new DataSet();
            DataTable _dt = new DataTable();
            _dt.Columns.Add("access_token");
            _dt.Columns.Add("instance_url");
            try
            {
                string _errorType = string.Empty;
                string _errorDescription = string.Empty;
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
                HttpResponseMessage _httpResponse = _httpClient.PostAsync(ValidationHelper.GetString(GetSettingsKey(_IsProduction, "AuthURL"), string.Empty), _httpContent).GetAwaiter().GetResult();
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
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(AuthenticateUsingHTTPClient), ex);
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

        ///// <summary>
        ///// Method to execute SOQL Queries in salesforce
        ///// </summary>
        ///// <param name="_query"></param>
        ///// <param name="_fieldName"></param>
        ///// <param name="_firstItemText"></param>
        ///// <returns></returns>
        public static DataTable GetDataUsingQuery(string _query, string _fieldName, string _firstItemText)
        {
            string _authToken;
            string _instanceURL;
            DataTable _dt = new DataTable();
            try
            {
                GetAuthenticationParams(out _authToken, out _instanceURL);

                HttpClient _httpClient = new HttpClient();
                string _restCallURL = _instanceURL + _apiURLQuery + _query;
                HttpRequestMessage _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _restCallURL);
                _httpRequestMessage.Headers.Add("authorization", "Bearer " + _authToken);
                HttpResponseMessage _httpResponseMessage = _httpClient.SendAsync(_httpRequestMessage).GetAwaiter().GetResult();
                string _httpResponse = _httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();


                _dt.Columns.Add("value");
                _dt.Columns.Add("text");

                DataRow _dr = _dt.NewRow();
                if (!string.IsNullOrEmpty(_firstItemText))
                {
                    _dr["value"] = "";
                    _dr["text"] = _firstItemText;
                    _dt.Rows.Add(_dr);
                }

                if (_httpResponseMessage.IsSuccessStatusCode)
                {
                    JObject _jObject = JObject.Parse(_httpResponse);
                    JToken _jTokens = _jObject["records"];
                    if (_jTokens != null && _jTokens.Children().Count() > 0)
                    {
                        foreach (JToken _token in _jTokens.Children())
                        {
                            _dr = _dt.NewRow();
                            foreach (JProperty _property in _token)
                            {
                                if (_property.Name.ToString().ToLower() == "id")
                                    _dr["value"] = _property.Value.ToString();
                                else if (_property.Name.ToString().ToLower() == _fieldName.ToLower())
                                    _dr["text"] = _property.Value.ToString();
                            }
                            _dt.Rows.Add(_dr);
                        }
                    }
                }
                else throw new System.Net.WebException(_httpResponse);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(GetDataUsingQuery), ex);
            }
            return _dt;
        }

        public static string GetSelectedFieldsUsingQuery(string _query)
        {
            string _authToken;
            string _instanceURL;
            HttpResponseMessage _httpResponseMessage = new HttpResponseMessage();
            string _httpResponse = string.Empty;
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
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(GetSelectedFieldsUsingQuery), ex);
            }
            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                return _httpResponse;
            }
            else throw new System.Net.WebException(_httpResponse);
        }

        /// <summary>
        /// Method to insert objects using Bulk API
        /// </summary>
        /// <param name="_lstDict"></param>
        /// <returns></returns>
        public static string BulkUpsert(List<Dictionary<string, string>> _lstDict, bool allOrNone = false)
        {
            return PostDataToSalesforceBulk(BulkDictToJson(_lstDict, allOrNone));
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
                string _restCallURL = _instanceURL;

                HttpRequestMessage _httpRequestMessage = new HttpRequestMessage(new HttpMethod("POST"), _restCallURL + "/services/data/v38.0/composite/");
                _httpRequestMessage.Headers.Add("authorization", "Bearer " + _authToken);
                _httpRequestMessage.Content = new StringContent(_stringContent, Encoding.UTF8, "application/json");

                HttpResponseMessage _httpResponseMessage = _httpClient.SendAsync(_httpRequestMessage).GetAwaiter().GetResult();

                _httpResponse = _httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(PostDataToSalesforceBulk), ex);
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
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(DictToJson), ex);
            }
            return "{" + _json.ToString() + "}";
        }

        public static string BulkDictToJson(List<Dictionary<string, string>> _lstDict, bool allOrNone = false)
        {
            var _json = new StringBuilder();
            var _jsonBody = new StringBuilder();
            int i = 0;
            try
            {
                _json.Append("{\"allOrNone\" : " + ValidationHelper.GetBoolean(allOrNone, false).ToString().ToLower());
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
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(BulkDictToJson), ex);
            }
            return "" + _json.ToString() + "]}";
        }

        /// <summary>
        /// Fetch Available Subscriptiions
        /// </summary>
        /// <returns>JSON string</returns>
        public static string SelectAvailableSubscriptions()
        {
            string _query = string.Empty;
            string _httpResponse = string.Empty;

            _query = "SELECT Id, Image_URL__c, Order__c, Display_Text__c, Display_Description__c FROM Available_Subscription__c where Visibility__c = 'Public' and Brand__c = 'DairyRetail' and Category_Area__c = 'Interest' and Status__c = true order by Order__c";

            _httpResponse = GetSelectedFieldsUsingQuery(_query);

            return _httpResponse;
        }

        #endregion

        #region Customizable Methods
        #region Contacts & Leads Methods

        ///// Checks if a contact exists then updates it else checks for a lead and upserts it
        ///// </summary>
        ///// <param name="_contactID"></param>
        ///// <param name="_leadID"></param>
        ///// <param name="_firstName"></param>
        ///// <param name="_lastName"></param>
        ///// <param name="_email"></param>
        ///// <param name="_phone"></param>
        ///// <param name="_state"></param>
        public static void UpsertContactLead(ref string _contactID, ref string _leadID, string _firstName, string _lastName, string _email, string? _phone = null, string? _address = null, string? _city = null, string? _state = null, string? _zipCode = null, string? _country = null, Dictionary<string, string>? _dictExtraInfo = null, string? _recordTypeIdOld = null, string? _recordTypeIdNew = null, string _leadSource = "")
        {
            string _stringContent = string.Empty;
            Dictionary<string, string> _dictContent = new Dictionary<string, string>();

            try
            {

                if (!String.IsNullOrEmpty(_firstName))
                    _dictContent.Add(_field_FirstName, _firstName);
                if (!String.IsNullOrEmpty(_lastName))
                    _dictContent.Add(_field_LastName, _lastName);
                if (!String.IsNullOrEmpty(_email))
                    _dictContent.Add(_field_Email, _email);
                if (!String.IsNullOrEmpty(_phone))
                    _dictContent.Add(_field_Phone, _phone);

                //Check Contacts
                string _query = "SELECT ID, Email FROM " + _object_Contact + " Where Email = '" + _email + "'";

                DataTable _dt = GetDataUsingQuery(_query, _field_Email, string.Empty);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    //Contact Exists
                    if (!String.IsNullOrEmpty(_address))
                        _dictContent.Add(_field_MailingStreet, _address);
                    if (!String.IsNullOrEmpty(_city))
                        _dictContent.Add(_field_MailingCity, _state);
                    if (!String.IsNullOrEmpty(_state))
                        _dictContent.Add(_field_MailingState, _state);
                    if (!String.IsNullOrEmpty(_zipCode))
                        _dictContent.Add(_field_MailingPostalCode, _zipCode);
                    if (!String.IsNullOrEmpty(_country))
                        _dictContent.Add(_field_MailingCountry, _country);

                    if (_dictExtraInfo != null)
                    {
                        if (_dictExtraInfo.ContainsKey("Operation_Type__c"))
                        {
                            _dictContent.Add(_field_Contact_Web_Operation_Type__c, _dictExtraInfo["Operation_Type__c"].ToString());
                        }

                        if (_dictExtraInfo.ContainsKey("RecipientEmailAddress__c"))
                        {
                            _dictContent.Add(_field_Contact_Web_RecipientEmailAddress__c, _dictExtraInfo["RecipientEmailAddress__c"].ToString());
                        }

                        if (_dictExtraInfo.ContainsKey("Description"))
                        {
                            _dictContent.Add(_field_Contact_Description, _dictExtraInfo["Description"].ToString());
                        }

                        if (_dictExtraInfo.ContainsKey("Comments__c"))
                        {
                            _dictContent.Add(_field_Web_Comments__c, _dictExtraInfo["Comments__c"].ToString());
                        }
                    }

                    _stringContent = DictToJson(_dictContent);

                    SFCommon.Upsert(_object_Contact, _stringContent, _dt.Rows[0]["value"].ToString());
                    _contactID = _dt.Rows[0]["value"].ToString();
                }
                else
                {
                    if (!String.IsNullOrEmpty(_address))
                        _dictContent.Add(_field_Street, _address);
                    if (!String.IsNullOrEmpty(_city))
                        _dictContent.Add(_field_City, _city);
                    if (!String.IsNullOrEmpty(_state))
                        _dictContent.Add(_field_State, _state);
                    if (!String.IsNullOrEmpty(_zipCode))
                        _dictContent.Add(_field_PostalCode, _zipCode);
                    if (!String.IsNullOrEmpty(_country))
                        _dictContent.Add(_field_Country, _country);
                    if (string.IsNullOrEmpty(_recordTypeIdNew))
                        _recordTypeIdNew = _recordTypeIdOld;

                    if (_dictExtraInfo != null)
                        _dictContent = _dictContent.Concat(_dictExtraInfo).ToDictionary(x => x.Key, x => x.Value);

                    //Check Lead
                    _query = "SELECT ID,Email FROM " + _object_Lead + " WHERE IsConverted = false AND Email = '" + _email + "'";

                    _dt = GetDataUsingQuery(_query, _field_Email, string.Empty);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        //Lead Exists
                        if (!string.IsNullOrEmpty(_recordTypeIdOld))
                            _dictContent.Add(_field_RecordTypeId, _recordTypeIdOld);

                        _stringContent = DictToJson(_dictContent);
                        SFCommon.Upsert(_object_Lead, _stringContent, _dt.Rows[0]["value"].ToString());
                        _leadID = _dt.Rows[0]["value"].ToString();
                    }
                    else
                    {
                        //Insert Lead
                        if (!string.IsNullOrEmpty(_recordTypeIdNew))
                            _dictContent.Add(_field_RecordTypeId, _recordTypeIdNew);

                        //Add Lead Source only when we insert a New Lead Source
                        if (!string.IsNullOrEmpty(_leadSource))
                            _dictContent.Add(_field_LeadSource, _leadSource);

                        _dictContent.Add(_field_Company, _firstName + " " + _lastName);
                        _stringContent = DictToJson(_dictContent);
                        _leadID = SFCommon.Upsert(_object_Lead, _stringContent);
                    }
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(UpsertContactLead), ex);
            }
        }

        #endregion
        public static string UpsertCampaign(string _contactID, string _leadID, string _campaignId)
        {

            string _campaignMemberID = string.Empty;
            string _stringContent = string.Empty;
            string _query = string.Empty;
            Dictionary<string, string> dictSubscription = new Dictionary<string, string>();
            DataTable dt;
            try
            {
                _query = "SELECT ID,createdbyid FROM CampaignMember  Where "
                    + (_contactID != string.Empty ? "(ContactId = '" + _contactID + "' " : "(")
                    + (_contactID != string.Empty && _leadID != string.Empty ? " OR " : "")
                    + (_leadID != string.Empty ? " LeadID = '" + _leadID + "')" : ")")
                    + " and CampaignId = '" + _campaignId + "'";

                dt = GetDataUsingQuery(_query, "createdbyid", string.Empty);


                if (_campaignId != string.Empty)
                    dictSubscription.Add("CampaignId", _campaignId);

                if (_leadID != string.Empty)
                    dictSubscription.Add("LeadID", _leadID);

                if (_contactID != string.Empty)
                    dictSubscription.Add("ContactId", _contactID);


                _stringContent = DictToJson(dictSubscription);
                if (dt != null && dt.Rows.Count > 0)
                    _campaignMemberID = dt.Rows[0]["value"].ToString();
                else
                    _campaignMemberID = SFCommon.Upsert("CampaignMember", _stringContent);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(UpsertCampaign), ex);
            }
            return _campaignMemberID;
        }

        /// <summary>
        /// Upserts a subscription record
        /// </summary>
        /// <param name="_leadID"></param>
        /// <param name="_contactID"></param>
        /// <param name="_opt_In_Source"></param>
        /// <param name="_interestSubscriptionID"></param>
        /// <returns></returns>
        public static string UpsertSubscription(string _leadID, string _contactID, string _opt_In_Source, string _interestSubscriptionID)
        {

            string _subscriptionID = string.Empty;
            string _stringContent = string.Empty;
            string _query = string.Empty;
            Dictionary<string, string> dictSubscription = new Dictionary<string, string>();
            DataTable dt;
            try
            {
                _query = "SELECT ID,createdbyid FROM Subscription__c Where "
                         + (_contactID != string.Empty ? "(Contact__C = '" + _contactID + "' " : "(")
                         + (_contactID != string.Empty && _leadID != string.Empty ? " OR " : "")
                         + (_leadID != string.Empty ? " Lead__C = '" + _leadID + "')" : ")")
                         + " and Subscription2__c = '" + _interestSubscriptionID + "'";

                dt = GetDataUsingQuery(_query, "createdbyid", string.Empty);

                dictSubscription.Add("Subscription2__c", _interestSubscriptionID);

                if (_contactID != string.Empty)
                    dictSubscription.Add("Contact__c", _contactID);

                if (_leadID != string.Empty)
                    dictSubscription.Add("Lead__c", _leadID);

                dictSubscription.Add("Opt_In_Date__c", DateTime.Now.ToString("yyyy-MM-dd"));
                dictSubscription.Add("Opt_In__c", "true");
                dictSubscription.Add("Opt_In_Source__c", _opt_In_Source);

                _stringContent = DictToJson(dictSubscription);
                if (dt != null && dt.Rows.Count > 0)
                    _subscriptionID = Upsert("Subscription__c", _stringContent, dt.Rows[0]["value"].ToString());
                else
                    _subscriptionID = Upsert("Subscription__c", _stringContent);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(UpsertSubscription), ex);
            }
            return _subscriptionID;
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
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(PostDataToSalesforce), ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get existing subscriptions
        /// </summary>
        /// <param name="_leadID"></param>
        /// <param name="_contactID"></param>
        /// <param name="_opt_In_Source"></param>
        /// <param name="lstSubscriptionids"></param>
        /// <returns></returns>
        public static DataTable GetExistingSubscriptionRecords(string _leadID, string _contactID, string _opt_In_Source, string lstSubscriptionids)
        {
            string _query = string.Empty;
            DataTable _dt = new DataTable();
            try
            {
                _query = "SELECT ID,opt_in__c, Subscription2__c  FROM " + _object_Subscription + " Where " + (_contactID != string.Empty ? "(Contact__c = '" + _contactID + "' " : "(") + (_contactID != string.Empty && _leadID != string.Empty ? " OR " : "") + (_leadID != string.Empty ? " Lead__c = '" + _leadID + "')" : ")") + " and Subscription2__c IN(" + lstSubscriptionids + ")";

                _dt = GetDataUsingQuery(_query, _field_Subscription2__c, string.Empty);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(GetExistingSubscriptionRecords), ex);
            }
            return _dt;
        }

        ///// <summary>
        ///// Creates a Subscription object as a dictionary object
        ///// </summary>
        ///// <param name="_subscriptionID"></param>
        ///// <param name="_leadID"></param>
        ///// <param name="_contactID"></param>
        ///// <param name="_opt_In_Source"></param>
        ///// <param name="_interestSubscriptionID"></param>
        ///// <returns></returns>
        public static Dictionary<string, string> CreateSubscriptionObject(string _subscriptionID, string _leadID, string _contactID, string _opt_in, string _opt_In_Source, string _interestSubscriptionID)
        {
            Dictionary<string, string> _dictSubscription = GetBaseSubscriptionObject(_subscriptionID, _leadID, _contactID, _opt_in, _opt_In_Source, _interestSubscriptionID);
            _dictSubscription.Add(_field_type, _object_Subscription);
            return _dictSubscription;
        }

        ///// <summary>
        ///// Gets the Base Subscription object
        ///// </summary>
        ///// <param name="_subscriptionID"></param>
        ///// <param name="_leadID"></param>
        ///// <param name="_contactID"></param>
        ///// <param name="_opt_In_Source"></param>
        ///// <param name="_interestSubscriptionID"></param>
        ///// <returns></returns>
        public static Dictionary<string, string> GetBaseSubscriptionObject(string _subscriptionID, string _leadID, string _contactID, string _opt_in, string _opt_In_Source, string _interestSubscriptionID)
        {
            Dictionary<string, string> _dictSubscription = new Dictionary<string, string>();
            try
            {
                if (_subscriptionID != string.Empty)
                {
                    _dictSubscription.Add(_field_Id, _subscriptionID);
                }

                if (!string.IsNullOrEmpty(_leadID))
                {
                    _dictSubscription.Add(_field_Lead__c, _leadID);
                }

                if (!string.IsNullOrEmpty(_contactID))
                {
                    _dictSubscription.Add(_field_Contact__c, _contactID);
                }

                _dictSubscription.Add(_field_Opt_In__c, _opt_in);
                _dictSubscription.Add(_field_Opt_In_Source__c, _opt_In_Source);
                _dictSubscription.Add(_field_Opt_In_Date__c, DateTime.Now.ToString("yyyy-MM-dd"));
                _dictSubscription.Add(_field_Subscription2__c, _interestSubscriptionID);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(GetBaseSubscriptionObject), ex);
            }
            return _dictSubscription;
        }

        //#endregion

        //#region Interest Methods
        public static DataTable GetExistingInterestRecords(string _leadID, string _contactID, string _opt_In_Source, string lstInterestids)
        {
            string _query = string.Empty;
            DataTable _dt = new DataTable();
            try
            {
                _query = "SELECT ID,opt_in__c, Interest_Selection__c  FROM " + _object_Interest + " Where " + (_contactID != string.Empty ? "(Contact__c = '" + _contactID + "' " : "(") + (_contactID != string.Empty && _leadID != string.Empty ? " OR " : "") + (_leadID != string.Empty ? " Lead__c = '" + _leadID + "')" : ")") + " and Interest_Selection__c IN(" + lstInterestids + ")";
                if (!string.IsNullOrEmpty(_leadID))
                {
                    _query += " AND Lead__r.IsConverted = false";
                }

                _dt = GetDataUsingQuery(_query, _field_Interest_Selection__c, string.Empty);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(GetBaseSubscriptionObject), ex);
            }
            return _dt;
        }

        ///// <summary>
        ///// Creates a Interest object as a dictionary object
        ///// </summary>
        ///// <param name="_interestID"></param>
        ///// <param name="_leadID"></param>
        ///// <param name="_contactID"></param>
        ///// <param name="_opt_In_Source"></param>
        ///// <param name="_interestSubscriptionID"></param>
        ///// <returns></returns>
        public static Dictionary<string, string> CreateInterestObject(string _interestID, string _leadID, string _contactID, string _opt_in, string _opt_In_Source, string _interestSubscriptionID)
        {
            Dictionary<string, string> _dictInterest = GetBaseInterestObject(_interestID, _leadID, _contactID, _opt_in, _opt_In_Source, _interestSubscriptionID);
            _dictInterest.Add(_field_type, _object_Interest);
            return _dictInterest;
        }

        ///// <summary>
        ///// Gets the Base Interest object
        ///// </summary>
        ///// <param name="_interestID"></param>
        ///// <param name="_leadID"></param>
        ///// <param name="_contactID"></param>
        ///// <param name="_opt_In_Source"></param>
        ///// <param name="_interestSubscriptionID"></param>
        ///// <returns></returns>
        public static Dictionary<string, string> GetBaseInterestObject(string _interestID, string _leadID, string _contactID, string _opt_in, string _opt_In_Source, string _interestSubscriptionID)
        {
            Dictionary<string, string> _dictInterest = new Dictionary<string, string>();
            try
            {
                if (_interestID != string.Empty)
                {
                    _dictInterest.Add(_field_Id, _interestID);
                }

                if (!string.IsNullOrEmpty(_leadID))
                {
                    _dictInterest.Add(_field_Lead__c, _leadID);
                }

                if (!string.IsNullOrEmpty(_contactID))
                {
                    _dictInterest.Add(_field_Contact__c, _contactID);
                }

                _dictInterest.Add(_field_Opt_In__c, _opt_in);
                _dictInterest.Add(_field_Interest_Selection__c, _interestSubscriptionID);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(GetBaseInterestObject), ex);
            }
            return _dictInterest;
        }

        //#endregion

        //#region Retail New Letter Sign up

        /// <summary>
        /// Subscriptions by emailId
        /// </summary>
        /// <param name="_emailId"></param>
        /// <returns></returns>
        public static DataTable SelectSubscriptionByEmailId(string _emailId)
        {
            string Subscription2__c = "a1F0a0000056AjCEAU";
            string _query = string.Empty;
            DataTable _dt = new DataTable();
            try
            {
                _query = "SELECT ID, opt_in__c, Subscription2__c  FROM " + _object_Subscription + " Where Subscription2__c ='" + Subscription2__c +
                        "' AND (Contact__r.Email = '" + _emailId + "' OR Lead__r.Email='" + _emailId + "')";

                _dt = GetDataUsingQuery(_query, _field_Subscription2__c, string.Empty);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(SelectSubscriptionByEmailId), ex);
            }
            return _dt;
        }

        /// <summary>
        /// Select intrests by emailId
        /// </summary>
        /// <param name="_emailId"></param>
        /// <param name="lstInterestids"></param>
        /// <returns></returns>
        public static string SelectInterestByEmailId(string _emailId, string lstInterestids)
        {
            string _query = string.Empty;
            string _httpResponse = string.Empty;
            try
            {
                _query = "SELECT id,opt_in__c, Interest_Selection__c  FROM " + _object_Interest + " Where " +
                        " Interest_Selection__c IN(" + lstInterestids + ") AND (Contact__r.Email = '" + _emailId + "' OR Lead__r.Email='" + _emailId + "') ";

                _httpResponse = GetSelectedFieldsUsingQuery(_query);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(SelectInterestByEmailId), ex);
            }
            return _httpResponse;
        }

        public static EmailOptIn IsContactOrLead(string pEmail)
        {
            Dictionary<string, string> vDictFields = new Dictionary<string, string>();
            vDictFields.Add("id", "");
            vDictFields.Add("email", "");
            vDictFields.Add("hasoptedoutofemail", "");
            vDictFields.Add("firstname", "");
            vDictFields.Add("lastname", "");
            vDictFields.Add("postalcode", "");

            EmailOptIn vEmailOpt = new EmailOptIn();
            try
            {
                //Check Contacts
                string _query = "SELECT ID, Email, HasOptedOutOfEmail FROM " + _object_Contact + " Where Email = '" + pEmail + "'";

                string vHttpResponse = GetSelectedFieldsUsingQuery(_query);
                List<Dictionary<string, string>> vListDictResponse = SFCommon.ConvertHttpResponseToDictionary(vHttpResponse, vDictFields);

                if (vListDictResponse != null && vListDictResponse.Count > 0 && vListDictResponse[0].ContainsKey("id"))
                {
                    vEmailOpt.isContact = true;
                    vEmailOpt.contactId = Convert.ToString(vListDictResponse[0]["id"]);
                    if (vListDictResponse[0].ContainsKey("hasoptedoutofemail"))
                    {
                        vEmailOpt.HasOptedOutOfEmail = Convert.ToBoolean(vListDictResponse[0]["hasoptedoutofemail"]);
                    }
                }

                if (!vEmailOpt.isContact)
                {
                    //Check Lead
                    _query = "SELECT ID,Email,FirstName,LastName,PostalCode, HasOptedOutOfEmail FROM " + _object_Lead + " WHERE IsConverted = false AND Email = '" + pEmail + "'";
                    vHttpResponse = GetSelectedFieldsUsingQuery(_query);
                    vListDictResponse = SFCommon.ConvertHttpResponseToDictionary(vHttpResponse, vDictFields);

                    if (vListDictResponse != null && vListDictResponse.Count > 0 && vListDictResponse[0].ContainsKey("id"))
                    {
                        vEmailOpt.isLead = true;
                        vEmailOpt.leadId = Convert.ToString(vListDictResponse[0]["id"]);
                        vEmailOpt.FirstName = Convert.ToString(vListDictResponse[0]["firstname"]);
                        vEmailOpt.LastName = Convert.ToString(vListDictResponse[0]["lastname"]);
                        vEmailOpt.ZipCode = Convert.ToString(vListDictResponse[0]["postalcode"]);
                        if (vListDictResponse[0].ContainsKey("hasoptedoutofemail"))
                        {
                            vEmailOpt.HasOptedOutOfEmail = Convert.ToBoolean(vListDictResponse[0]["hasoptedoutofemail"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(IsContactOrLead), ex);
            }
            return vEmailOpt;
        }

        /// <summary>
        /// UnsubscribeLeadContact
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pObjectType"></param>
        /// <returns></returns>
        public static String UnsubscribeLeadContact(string pId, string pObjectType)
        {
            Dictionary<string, string> vDictObj = new Dictionary<string, string>();
            string vStringContent = string.Empty;
            string vResult = string.Empty;
            try
            {
                vDictObj.Add(_field_Opted_Out_Of_Email__c, "true");

                vStringContent = DictToJson(vDictObj);
                vResult = Upsert(pObjectType, vStringContent, pId);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(UnsubscribeLeadContact), ex);
            }
            return vResult;
        }

        /// <summary>
        /// SendUnSubscribeFeedback
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pObjectType"></param>
        /// <param name="pSubscriptionRecordId"></param>
        /// <param name="pUnsubscribeReason"></param>
        /// <returns></returns>
        public static String SendUnSubscribeFeedback(string pId, string pObjectType, string pSubscriptionRecordId, string pUnsubscribeReason)
        {
            List<Dictionary<string, string>> vListBulkUpsert = new List<Dictionary<string, string>>();
            Dictionary<string, string> vDictSubscription = new Dictionary<string, string>();
            Dictionary<string, string> vDictBaseObject = new Dictionary<string, string>();
            string vResult = string.Empty;
            try
            {
                vDictSubscription.Add(_field_Id, pSubscriptionRecordId);
                vDictSubscription.Add(_field_Unsubscribe_Reason__c, pUnsubscribeReason);
                vDictSubscription.Add(_field_type, _object_Subscription);
                vListBulkUpsert.Add(vDictSubscription);

                vDictBaseObject.Add(_field_Id, pId);
                vDictBaseObject.Add(_field_Opt_Out_Reason__c, pUnsubscribeReason);
                vDictBaseObject.Add(_field_type, pObjectType);
                vListBulkUpsert.Add(vDictBaseObject);

                vResult = SFPardot.BulkUpsert(vListBulkUpsert);
            }
            catch (Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(SFPardot), nameof(SendUnSubscribeFeedback), ex);
            }
            return vResult;
        }
        public class AvaliableSubscriptions
        {
            public string? Display_Description__c { get; set; }
            public string? Display_Text__c { get; set; }
            public string? Id { get; set; }
            public string? Image_URL__c { get; set; }
            public string? Order__c { get; set; }
        }

        public class EmailOptIn
        {
            public Boolean isLead { get; set; }
            public Boolean isContact { get; set; }
            public string contactId { get; set; }
            public string leadId { get; set; }
            public Boolean HasOptedOutOfEmail { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ZipCode { get; set; }
            public string subscriptionRecordId { get; set; }
            public EmailOptIn()
            {
                this.isLead = false;
                this.isContact = false;
                this.contactId = string.Empty;
                this.leadId = string.Empty;
                this.HasOptedOutOfEmail = false;
                this.subscriptionRecordId = string.Empty;
                this.FirstName = string.Empty;
                this.LastName = string.Empty;
                this.ZipCode = string.Empty;
            }
        }
        public class SalesforceJsonResponse
        {
            public List<SalesforceCompositeResponse> compositeResponse { get; set; }
        }
        public class SalesforceCompositeResponse
        {
            public SalesforeceResponseBody body { get; set; }
            public string httpStatusCode { get; set; }
            public string referenceId { get; set; }
        }

        public class SalesforeceResponseBody
        {
            public string id { get; set; }
            public bool success { get; set; }
        }
        //#endregion
        #endregion
    }
}
