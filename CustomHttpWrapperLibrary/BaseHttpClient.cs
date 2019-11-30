using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CustomHttpWrapperLibrary.Helpers;
using CustomHttpWrapperLibrary.Utils;

namespace CustomHttpWrapperLibrary
{
    /// <summary>
    /// Base HTTP singleton client.
    /// </summary>
    public class BaseHttpClient
    {
        /// <summary>
        /// HttpClient singleton instance.
        /// </summary>
        private HttpClient Client = null;

        /// <summary>
        /// Default message handler for HttpClient singleton.
        /// </summary>
        public HttpClientHandler ClientHandler { get; private set; }

        /// <summary>
        /// Default cookie container for HttpClient singleton.
        /// </summary>
        public CookieContainer ClientCookies { get; private set; }

        /// <summary>
        /// Default cookies usage status for HttpClient singleton.
        /// </summary>
        public bool UseCookies { get; private set; }

        /// <summary>
        /// Default list of request headers to be used for HttpClient singleton.
        /// </summary>
        public List<MediaTypeWithQualityHeaderValue> DefaultRequestHeaders { get; private set; }

        /// <summary>
        /// Default HTTP base address to be used for HttpClient singleton.
        /// </summary>
        public string ClientBaseAddress { get; private set; }

        /// <summary>
        /// Main singleton constructor for BaseHttpClient. Calls <see cref="GetHttpClient"/> method to instantiate a HttpClient only if an instance does not exist.
        /// </summary>
        /// <param name="useCookies">Cookies usage status to be setted in <see cref="UseCookies"/>.</param>
        /// <param name="clientCookies">Cookies list to be setted in <see cref="ClientCookies"/>.</param>
        /// <param name="clientHandler">HttpClient handler to be setted in <see cref="ClientHandler"/>.</param>
        /// <param name="clientDefaultRequestHeaders">Default HttpClient request headers to be setted in <see cref="DefaultRequestHeaders"/>.</param>
        /// <param name="clientBaseAddress">Base HttpClient address to be setted in <see cref="ClientBaseAddress"/>.</param>
        public BaseHttpClient(bool useCookies = false, CookieContainer clientCookies = null, HttpClientHandler clientHandler = null, List<MediaTypeWithQualityHeaderValue> clientDefaultRequestHeaders = null, string clientBaseAddress = null)
        {
            GetHttpClient(useCookies, clientCookies, clientHandler, clientDefaultRequestHeaders, clientBaseAddress);
        }

        /// <summary>
        /// Singleton creation or getter method depending on instance existence.
        /// </summary>
        /// <param name="useCookies">Cookies usage status to be setted in <see cref="UseCookies"/>.</param>
        /// <param name="clientCookies">Cookies list to be setted in <see cref="ClientCookies"/>.</param>
        /// <param name="clientHandler">HttpClient handler to be setted in <see cref="ClientHandler"/>.</param>
        /// <param name="clientDefaultRequestHeaders">Default HttpClient request headers to be setted in <see cref="DefaultRequestHeaders"/>.</param>
        /// <param name="clientBaseAddress">Base HttpClient address to be setted in <see cref="ClientBaseAddress"/>.</param>
        /// <returns>HttpClient singleton.</returns>
        private HttpClient GetHttpClient(bool useCookies = false, CookieContainer clientCookies = null, HttpClientHandler clientHandler = null, List<MediaTypeWithQualityHeaderValue> clientDefaultRequestHeaders = null, string clientBaseAddress = null)
        {
            if (this.Client == null)
            {
                if (clientCookies == null) this.ClientCookies = new CookieContainer();
                else this.ClientCookies = clientCookies;

                this.UseCookies = useCookies;
                if (ClientHandler == null)
                {
                    this.ClientHandler = new HttpClientHandler
                    {
                        UseCookies = useCookies,
                        CookieContainer = ClientCookies
                    };
                }
                else this.ClientHandler = clientHandler;

                this.Client = new HttpClient(this.ClientHandler);
                if (clientBaseAddress != null)
                {
                    this.Client.BaseAddress = new Uri(clientBaseAddress);
                    this.ClientBaseAddress = clientBaseAddress;
                }

                if (clientDefaultRequestHeaders != null && clientDefaultRequestHeaders.Count > 0)
                {
                    this.Client.DefaultRequestHeaders.Clear();
                    this.DefaultRequestHeaders = clientDefaultRequestHeaders;
                    foreach (MediaTypeWithQualityHeaderValue requestHeader in clientDefaultRequestHeaders)
                    {
                        try
                        {
                            this.Client.DefaultRequestHeaders.Accept.Add(requestHeader);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{DateTime.Now} [ERROR] Exception details: {ex.ToString()}");
                        }
                    }
                }
            }

            return this.Client;
        }

        /// <summary>
        /// HttpClient singleton release with all class properties.
        /// </summary>
        public void DisposeClient()
        {
            this.Client.Dispose();
            this.ClientHandler.Dispose();
            this.Client = null;
            this.ClientHandler = null;
            this.ClientCookies = null;
            this.ClientBaseAddress = null;
            this.DefaultRequestHeaders = null;
        }

        /// <summary>
        /// Release HttpClient singleton and update inner properties, such as the HttpClient handler.
        /// </summary>
        /// <param name="useCookies">Cookies usage status to be setted in <see cref="UseCookies"/>.</param>
        /// <param name="clientCookieList"><see cref="Cookie"/> list to be iterated and setted in <see cref="ClientCookies"/>.</param>
        /// <param name="clientDefaultRequestHeaders">Default HttpClient request headers to be setted in <see cref="DefaultRequestHeaders"/>.</param>
        /// <param name="clientBaseAddress">Base HttpClient address to be setted in <see cref="ClientBaseAddress"/>.</param>
        /// <returns>HttpClient singleton.</returns>
        public BaseHttpClient UpdateHttpClientHandler(bool useCookies = false, List<Cookie> clientCookieList = null, List<MediaTypeWithQualityHeaderValue> clientDefaultRequestHeaders = null, string clientBaseAddress = null)
        {
            DisposeClient();
            this.ClientCookies = new CookieContainer();
            this.ClientHandler = new HttpClientHandler
            {
                UseCookies = useCookies,
                CookieContainer = ClientCookies
            };
            foreach (Cookie cookie in clientCookieList) ClientHandler.CookieContainer.Add(cookie);
            this.Client = GetHttpClient(useCookies, this.ClientCookies, this.ClientHandler, clientDefaultRequestHeaders, clientBaseAddress);
            return this;
        }

        /// <summary>
        /// Release HttpClient singleton and update inner properties, such as the HttpClient handler.
        /// </summary>
        /// <param name="useCookies">Cookies usage status to be setted in <see cref="UseCookies"/>.</param>
        /// <param name="clientCookies">Cookies' list to be setted in <see cref="ClientCookies"/>.</param>
        /// <param name="clientDefaultRequestHeaders">Default HttpClient request headers to be setted in <see cref="DefaultRequestHeaders"/>.</param>
        /// <param name="clientBaseAddress">Base HttpClient address to be setted in <see cref="ClientBaseAddress"/>.</param>
        /// <returns>HttpClient singleton.</returns>
        public BaseHttpClient UpdateHttpClientHandler(bool useCookies = false, CookieContainer clientCookies = null, List<MediaTypeWithQualityHeaderValue> clientDefaultRequestHeaders = null, string clientBaseAddress = null)
        {
            DisposeClient();
            this.Client = GetHttpClient(useCookies, clientCookies, null, clientDefaultRequestHeaders, clientBaseAddress);
            return this;
        }

        /// <summary>
        /// Request asynchronously a response as HttpContent through a GET request.
        /// </summary>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="onlySuccessResponses">Enable or not a trigger to throw an exception if the response is not a success.</param>
        /// <returns>The final response <see cref="HttpResponseMessage"/> in a async Task.</returns>
        public async Task<HttpResponseMessage> GetAsyncFullResponse(string path, bool onlySuccessResponses = true)
        {
            HttpResponseMessage resp = await this.Client.GetAsync(path);
            HttpContent respContent = resp.Content;
            if (onlySuccessResponses) resp.EnsureSuccessStatusCode();
            return resp;
        }

        /// <summary>
        /// Request asynchronously a response content as HttpContent through a GET request.
        /// </summary>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="onlySuccessResponses">Enable or not a trigger to throw an exception if the response is not a success.</param>
        /// <returns>The final response HttpContent in a async Task.</returns>
        public async Task<HttpContent> GetAsyncHttpContent(string path, bool onlySuccessResponses = true)
        {
            HttpResponseMessage resp = await this.Client.GetAsync(path);
            HttpContent respContent = resp.Content;
            if (onlySuccessResponses) resp.EnsureSuccessStatusCode();
            return respContent;
        }

        /// <summary>
        /// Request asynchronously a response content as string through a GET request.
        /// </summary>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="onlySuccessResponses">Enable or not a trigger to throw an exception if the response is not a success.</param>
        /// <returns>The final response HttpContent in a async Task.</returns>
        public async Task<string> GetAsyncContentStr(string path, bool onlySuccessResponses = true)
        {
            HttpResponseMessage resp = await this.Client.GetAsync(path);
            string respContentStr = await resp.Content.ReadAsStringAsync();
            if (onlySuccessResponses) resp.EnsureSuccessStatusCode();
            return respContentStr;
        }

        /// <summary>
        /// Request asynchronously a response from JSON to a dynamic &lt;T&gt; object through a POST request.
        /// </summary>
        /// <typeparam name="T">Dynamic type param to be used as a response object.</typeparam>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="srEnc">(Optional, default as null) <see cref="Encoding"/> type to deserialize an object from JSON.</param>
        /// <param name="onlySuccessResponses">(Optional, default as true) Enable or not a trigger to throw an exception if the response is not a success.</param>
        /// <returns>HttpContent response deserialized as dynamic &lt;T&gt; object.</returns>
        public async Task<T> GetAsyncFromJSONToObject<T>(string path, Encoding srEnc = null, bool onlySuccessResponses = true)
        {
            HttpResponseMessage resp = await this.Client.GetAsync(path);
            string respContentStr = await resp.Content.ReadAsStringAsync();
            if (onlySuccessResponses) resp.EnsureSuccessStatusCode();
            return SerializationService.DeserializeObjectFromJSON<T>(respContentStr, srEnc);
        }

        /// <summary>
        /// Request asynchronously a file through a GET method and download the file to a specific drive path.
        /// </summary>
        /// <param name="requestUri">URI path to execute the HTTP file request.</param>
        /// <param name="drivePath">Drive path to save the downloaded file.</param>
        /// <param name="backupFileName">(Optional, default as null) Set the file as a backup name in case the downloaded file does not contain a <see cref="HttpResponseMessage.Content"/> Headers.ContentDisposition.FileName.</param>
        /// <param name="overrideFileName">(Optional, default as null) Ignore <see cref="HttpResponseMessage.Content"/> Headers.ContentDisposition.FileName and set manually the downloaded file name.</param>
        /// <param name="overrideFileExtension">(Optional, default as null) Ignore <see cref="HttpResponseMessage.Content"/> Headers.ContentDisposition.FileName extension and set manually the downloaded file extension.</param>
        /// <returns>Full drive path which was saved the downloaded file (or null, if the file was not saved).</returns>
        public async Task<string> GetAsyncDownloadFile(string requestUri, string drivePath, string backupFileName, string overrideFileName = null, string overrideFileExtension = null)
        {
            if (drivePath.ElementAt(drivePath.Length - 1).Equals("/") || drivePath.ElementAt(drivePath.Length - 1).Equals("\\")) { }
            else drivePath = $"{drivePath}\\";

            using (HttpResponseMessage result = await this.Client.GetAsync(requestUri))
            {
                if (result.IsSuccessStatusCode)
                {
                    string fileName = backupFileName;

                    if (overrideFileName != null)
                    {
                        fileName = overrideFileName;
                        if (overrideFileExtension != null) fileName = overrideFileExtension.Contains(".") ? $"{fileName}{overrideFileExtension}" : $"{fileName}.{overrideFileExtension}";
                        else fileName = $"{fileName}{IOService.GetContentTypeExtension(result.Content.Headers.ContentType.MediaType, result.Content.Headers.ContentDisposition.FileName)}";
                    }
                    else if (result.Content.Headers?.ContentDisposition?.FileName != null) fileName = result.Content.Headers.ContentDisposition.FileName;

                    if (IOService.SaveByteArrayToFile($"{drivePath}{fileName}", await result.Content.ReadAsByteArrayAsync())) return $"{drivePath}{fileName}";
                    else return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Request asynchronously a <see cref="HttpResponseMessage"/> through a POST request passing FormData basic info.
        /// </summary>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="postParameters"><see cref="Dictionary{TKey, TValue}"/> of double strings with parameters to be send as a POST request.</param>
        /// <returns>Asynchronous <see cref="HttpResponseMessage"/> directly from POST request.</returns>
        public async Task<HttpResponseMessage> PostAsyncFormData(string path, Dictionary<string, string> postParameters)
        {
            return await this.Client.PostAsync(path, new FormUrlEncodedContent(postParameters));
        }

        /// <summary>
        /// Request asynchronously a <see cref="HttpResponseMessage"/> through a POST request passing FormUrlContent as FormData basic info.
        /// </summary>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="postParameters"><see cref="Dictionary{TKey, TValue}"/> of string and object with parameters to be send as a POST request.</param>
        /// <param name="encoding">(Optional, default as null) <see cref="Encoding"/> type to encode the <see cref="Dictionary{TKey, TValue}" of values./>.</param>
        /// <returns>Asynchronous <see cref="HttpResponseMessage"/> directly from POST request.</returns>
        public async Task<HttpResponseMessage> PostAsyncFormData(string path, Dictionary<string, object> postParameters, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, path);
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formDataCompiled = BaseHttpWebRequest.GetMultipartFormData(postParameters, formDataBoundary, encoding);
            msg.Content = new ByteArrayContent(formDataCompiled);
            msg.Content.Headers.ContentLength = formDataCompiled.Length;
            msg.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNamesHelper.Web.FormData);

            return await this.Client.SendAsync(msg);
        }

        /// <summary>
        /// Request asynchronously a <see cref="HttpResponseMessage"/> through a POST request passing a serialized dynamic &lt;T&gt; object.
        /// </summary>
        /// <typeparam name="T">Dynamic type param to be used as a response object.</typeparam>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="bodyObj">Dynamic &lt;T&gt; object to be sent as a body request.</param>
        /// <param name="srEnc">(Optional, default as null) <see cref="Encoding"/> type to deserialize an object from JSON.</param>
        /// <param name="mediaTypeQualityHeader">(Optional, default as null) <see cref="StringContent"/> headers content-type to be used as a <see cref="HttpRequestMessage"/> and <see cref="StringContent"/> media type content headers.</param>
        /// <param name="mediaTypeCharset">(Optional, default as null) <see cref="StringContent"/> headers content-type charset to be used as a <see cref="HttpRequestMessage"/> and <see cref="StringContent"/> charset content headers.</param>
        /// <returns>Asynchronous <see cref="HttpResponseMessage"/> directly from POST request.</returns>
        public async Task<HttpResponseMessage> PostAsyncJSON<T>(string path, T bodyObj, Encoding srEnc = null, string mediaTypeQualityHeader = null, string mediaTypeCharset = null)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, path);

            string bodyObjAsJSON = SerializationService.SerializeObjectToJSON(bodyObj, typeof(T));
            StringContent stringContent;

            if (srEnc == null) srEnc = Encoding.UTF8;
            if (mediaTypeQualityHeader == null) stringContent = new StringContent(bodyObjAsJSON, Encoding.UTF8);
            else
            {
                stringContent = new StringContent(bodyObjAsJSON, srEnc, mediaTypeQualityHeader);
                MediaTypeWithQualityHeaderValue mediaTypeContentType = new MediaTypeWithQualityHeaderValue(mediaTypeQualityHeader);

                if (mediaTypeCharset != null)
                {
                    mediaTypeCharset = WebService.ValidateContentTypeCharset(mediaTypeCharset) ? mediaTypeCharset : MediaTypeNamesHelper.Encoding.UTF8;
                    mediaTypeContentType.CharSet = mediaTypeCharset;
                    stringContent.Headers.ContentType.CharSet = mediaTypeCharset;
                }
                else stringContent.Headers.ContentType = mediaTypeContentType;

                msg.Headers.Accept.Add(mediaTypeContentType);
            }

            msg.Content = stringContent;
            return await this.Client.SendAsync(msg);
        }

        /// <summary>
        /// Request asynchronously a <see cref="HttpResponseMessage"/> through a POST request passing a serialized dynamic &lt;T&gt; object and receiving a deserialized &lt;U&gt; dynamic object.
        /// </summary>
        /// <typeparam name="T">Dynamic type param to be used as a request object.</typeparam>
        /// <typeparam name="U">Dynamic type param to be used as a response object.</typeparam>
        /// <param name="path">URI path to asynchronously execute a GET request.</param>
        /// <param name="bodyObj">Dynamic &lt;T&gt; object to be sent as a body request.</param>
        /// <param name="srEnc">(Optional, default as null) <see cref="Encoding"/> type to deserialize an object from JSON.</param>
        /// <param name="mediaTypeQualityHeader">(Optional, default as null) <see cref="StringContent"/> headers content-type to be used as a <see cref="HttpRequestMessage"/> and <see cref="StringContent"/> media type content headers.</param>
        /// <param name="mediaTypeCharset">(Optional, default as null) <see cref="StringContent"/> headers content-type charset to be used as a <see cref="HttpRequestMessage"/> and <see cref="StringContent"/> charset content headers.</param>
        /// <param name="onlySuccessResponses">(Optional, default as true) Enable or not a trigger to throw an exception if the response is not a success.</param>
        /// <returns>Asynchronous serialized &lt;U&gt; dynamic object directly from POST request.</returns>
        public async Task<U> PostAsyncFromJSONToObject<T, U>(string path, T bodyObj, Encoding srEnc = null, string mediaTypeQualityHeader = null, string mediaTypeCharset = null, bool onlySuccessResponses = true)
        {

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, path);

            string bodyObjAsJSON = SerializationService.SerializeObjectToJSON(bodyObj, typeof(T));
            StringContent stringContent;

            if (srEnc == null) srEnc = Encoding.UTF8;
            if (mediaTypeQualityHeader == null) stringContent = new StringContent(bodyObjAsJSON, Encoding.UTF8);
            else
            {
                stringContent = new StringContent(bodyObjAsJSON, srEnc, mediaTypeQualityHeader);
                MediaTypeWithQualityHeaderValue mediaTypeContentType = new MediaTypeWithQualityHeaderValue(mediaTypeQualityHeader);

                if (mediaTypeCharset != null)
                {
                    mediaTypeCharset = WebService.ValidateContentTypeCharset(mediaTypeCharset) ? mediaTypeCharset : MediaTypeNamesHelper.Encoding.UTF8;
                    mediaTypeContentType.CharSet = mediaTypeCharset;
                    stringContent.Headers.ContentType.CharSet = mediaTypeCharset;
                }
                else stringContent.Headers.ContentType = mediaTypeContentType;

                msg.Headers.Accept.Add(mediaTypeContentType);
            }

            msg.Content = stringContent;
            HttpResponseMessage resp = await this.Client.SendAsync(msg);
            string respContentStr = await resp.Content.ReadAsStringAsync();
            if (onlySuccessResponses) resp.EnsureSuccessStatusCode();
            return SerializationService.DeserializeObjectFromJSON<U>(respContentStr, srEnc);
        }

        /// <summary>
        /// Get properties or <see cref="DataMemberAttribute"/> (and their respective values) of an object and parameterize them as path search arguments in a GET method request.
        /// </summary>
        /// <typeparam name="T">Dynamic type param to be extracted its respective properties.</typeparam>
        /// <param name="subPath">Main raw URI path without the search params.</param>
        /// <param name="obj">Dynamic &lt;T&gt; object to be extracted its respective properties.</param>
        /// <param name="customModuleNameSpace">Specific module name space used to get a project assembly info through <see cref="AppDomain.CurrentDomain"/> Load function. Check <see cref="System.Reflection"/> pattern to understand assembly method calls.</param>
        /// <param name="customModuleNameFn">Specific module name function used to get a project assembly info through <see cref="Type.GetMethod(string)"/>. Check <see cref="System.Reflection"/> pattern to understand assembly method calls.</param>
        /// <param name="paramsFromDataMember">(Optional, default false) Definition to whether get the params attribute and value from a <see cref="DataMemberAttribute"/> or not.</param>
        /// <param name="generateTimeSpanAttr">(Optional, default null) Attribute name if an extra <see cref="TimeSpan"/> parameter has to be added to the search path.</param>
        /// <param name="generateTimeSpanValue">(Optional, default null) Attribute value if an extra <see cref="TimeSpan"/> parameter has to be added to the search path.</param>
        /// <returns>Constructed GET method URL path with the search params added and escaped (check <see cref="Uri.EscapeDataString(string)"/> for more info.</returns>
        public string ConstructGETPathRequestWithParamsFromObject<T>(string subPath, T obj, string customModuleNameSpace, string customModuleNameFn, bool paramsFromDataMember = false, string generateTimeSpanAttr = null, string generateTimeSpanValue = null)
        {
            string propertyName;
            string propertyStrVal;
            object propertyObjVal;
            PropertyInfo[] properties = typeof(T).GetProperties();

            Type propertyObjValType;
            DataMemberAttribute DMAttrProp;

            try
            {
                if (properties.Length > 0)
                {
                    subPath = $"{subPath}?";
                    foreach (PropertyInfo property in properties)
                    {
                        propertyObjVal = property.GetValue(obj);
                        if (propertyObjVal == null) continue;

                        if (paramsFromDataMember)
                        {
                            // Getting property name to be outputed in the path
                            DMAttrProp = property.GetCustomAttribute(typeof(DataMemberAttribute), true) as DataMemberAttribute;
                            propertyName = DMAttrProp.Name;
                        }
                        else propertyName = property.Name;

                        // Getting the property value to be outputed in the path according to object's property value from type
                        propertyObjValType = propertyObjVal.GetType();
                        if (propertyObjValType == typeof(string) || propertyObjValType == typeof(int) || propertyObjValType == typeof(long))
                        {
                            propertyStrVal = propertyObjVal.ToString();
                        }
                        else
                        {
                            // Through reflection, get the value calling a dynamic function from a dynamic namespace assembly
                            var assemblyObjType = AppDomain.CurrentDomain.Load(customModuleNameSpace);
                            Type customObjType = assemblyObjType.GetType(propertyObjValType.ToString(), true);
                            MethodInfo customObjValueFn = customObjType.GetMethod(customModuleNameFn);
                            try
                            {
                                propertyStrVal = customObjValueFn.Invoke(propertyObjVal, null).ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{DateTime.Now} [ERROR] It wasn't possible to get the value the dynamic object function. Exception details: {ex.ToString()}");
                                continue;
                            }
                        }

                        subPath = $"{subPath}{propertyName}={Uri.EscapeDataString(propertyStrVal)}&";
                    }
                }
                if (generateTimeSpanAttr != null && generateTimeSpanValue != null) subPath = $"{subPath}{generateTimeSpanAttr}={Uri.EscapeDataString(generateTimeSpanValue)}";
                return subPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] It wasn't possible to construct the URI path with params from object. Exception details: {ex.ToString()}");
                throw ex;
            }
        }

        /// <summary>
        /// Static method to set a <see cref="MediaTypeWithQualityHeaderValue"/> charset value.
        /// </summary>
        /// <param name="mediaType"><see cref="MediaTypeWithQualityHeaderValue"/> instance to be changed.</param>
        /// <param name="charsetVal">Charset value to be assigned to a <see cref="MediaTypeWithQualityHeaderValue"/> instance.</param>
        /// <returns><see cref="MediaTypeWithQualityHeaderValue"/> instance changed with a charset value.</returns>
        public static MediaTypeWithQualityHeaderValue SetMediaTypeHeaderCharset(MediaTypeWithQualityHeaderValue mediaType, string charsetVal)
        {
            mediaType.CharSet = WebService.ValidateContentTypeCharset(charsetVal) ? charsetVal : MediaTypeNamesHelper.Encoding.UTF8;
            return mediaType;
        }
    }
}
