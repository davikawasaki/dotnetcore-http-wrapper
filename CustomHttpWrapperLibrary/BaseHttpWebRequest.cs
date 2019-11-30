using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using CustomHttpWrapperLibrary.Helpers;
using CustomHttpWrapperLibrary.Utils;

namespace CustomHttpWrapperLibrary
{
    /// <summary>
    /// Static class for HttpWebRequests pre-configured HTTP calls.
    /// </summary>
    public class BaseHttpWebRequest
    {
        /// <summary>
        /// Execute a GET <see cref="HttpWebRequest"/> method according to specific configurations.
        /// </summary>
        /// <param name="url">URI path to execute the HTTP file request.</param>
        /// <param name="headerAccept">(Optional) Set <see cref="HttpWebRequest.Accept"/> HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="contentTypeValue">(Optional) Set <see cref="HttpWebRequest.ContentType"/> HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="contentTypeCharset">(Optional) Set <see cref="HttpWebRequest.ContentType"/> charset HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="cookieContainer">(Optional) Associate a <see cref="CookieContainer"/> to the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="allowAutoRedirect">(Optional) Set if the <see cref="HttpWebRequest"/> should or not follow redirection responses.</param>
        /// <returns>Instance of <see cref="HttpWebResponse"/> or an exception if the GET method request wasn't succesfull.</returns>
        public static HttpWebResponse GETWebResponse(string url, string headerAccept = null, string contentTypeValue = null, string contentTypeCharset = null, CookieContainer cookieContainer = null, bool allowAutoRedirect = false)
        {
            HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(url);
            if (httpWebReq.CookieContainer == null) httpWebReq.CookieContainer = cookieContainer ?? new CookieContainer();
            if (headerAccept != null) httpWebReq.Accept = headerAccept;

            httpWebReq.ContentType = WebService.ValidateContentTypeValue(contentTypeValue) ? contentTypeValue : MediaTypeNamesHelper.Web.Json;
            if (contentTypeCharset != null)
            {
                contentTypeCharset = WebService.ValidateContentTypeCharset(contentTypeCharset) ? contentTypeCharset : MediaTypeNamesHelper.Encoding.UTF8;
                httpWebReq.ContentType = $"{httpWebReq.ContentType};charset={contentTypeCharset}";
            }

            httpWebReq.AllowAutoRedirect = allowAutoRedirect;

            try
            {
                return (HttpWebResponse)httpWebReq.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] It wasn't possible to do the GET request. Exception details: {ex.ToString()}");
                throw ex;
            }
        }

        /// <summary>
        /// Execute a POST <see cref="HttpWebResponse"/> method with a <see cref="byte"/> array content data to be sent through the request.
        /// </summary>
        /// <param name="url">URI path to execute the HTTP file request.</param>
        /// <param name="contentData">Content data in <see cref="byte"/> array format to be instanteneously written in a <see cref="Stream"/>.</param>
        /// <param name="contentTypeValue">(Optional) Set <see cref="HttpWebRequest.ContentType"/> HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="contentTypeCharset">(Optional) Set <see cref="HttpWebRequest.ContentType"/> charset HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="cookieContainer">(Optional) Associate a <see cref="CookieContainer"/> to the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="allowAutoRedirect">(Optional) Set if the <see cref="HttpWebRequest"/> should or not follow redirection responses.</param>
        /// <returns>Instance of <see cref="HttpWebResponse"/> or an exception if the POST method request wasn't succesfull.</returns>
        public static HttpWebResponse POSTWebRequest(string url, byte[] contentData, string contentTypeValue, string contentTypeCharset = null, CookieContainer cookieContainer = null, bool allowAutoRedirect = false)
        {
            HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(url);
            httpWebReq.CookieContainer = cookieContainer ?? new CookieContainer();
            httpWebReq.Method = "POST";

            httpWebReq.ContentType = WebService.ValidateContentTypeValue(contentTypeValue) ? contentTypeValue : MediaTypeNamesHelper.Web.Json;
            if (contentTypeCharset != null)
            {
                contentTypeCharset = WebService.ValidateContentTypeCharset(contentTypeCharset) ? contentTypeCharset : MediaTypeNamesHelper.Encoding.UTF8;
                httpWebReq.ContentType = $"{httpWebReq.ContentType};charset={contentTypeCharset}";
            }

            httpWebReq.ContentLength = contentData.Length;
            httpWebReq.AllowAutoRedirect = true;

            using (Stream stream = httpWebReq.GetRequestStream()) stream.Write(contentData, 0, contentData.Length);

            try
            {
                return (HttpWebResponse)httpWebReq.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] It wasn't possible to do the POST request. Exception details: {ex.ToString()}");
                throw ex;
            }
        }

        /// <summary>
        /// Execute a POST <see cref="HttpWebResponse"/> method with a string content data to be sent through the request.
        /// </summary>
        /// <param name="url">URI path to execute the HTTP file request.</param>
        /// <param name="contentData">Content data in string format. Needs to be converted to an array of bytes so that, afterwards, can be written in a <see cref="Stream"/>.</param>
        /// <param name="contentTypeValue">(Optional) Set <see cref="HttpWebRequest.ContentType"/> HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="contentTypeCharset">(Optional) Set <see cref="HttpWebRequest.ContentType"/> charset HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="enc">(Optional) Encoding chosen to transform the content data from a string format to an array of bytes.</param>
        /// <param name="cookieContainer">(Optional) Associate a <see cref="CookieContainer"/> to the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="allowAutoRedirect">(Optional) Set if the <see cref="HttpWebRequest"/> should or not follow redirection responses.</param>
        /// <returns>Instance of <see cref="HttpWebResponse"/> or an exception if the POST method request wasn't succesfull.</returns>
        public static HttpWebResponse POSTWebRequest(string url, string contentData, string contentTypeValue, string contentTypeCharset = null,
            Encoding enc = null, CookieContainer cookieContainer = null, bool allowAutoRedirect = false)
        {
            if (enc == null) enc = Encoding.UTF8;
            byte[] contentDataBytes = enc.GetBytes(contentData);

            HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(url);
            httpWebReq.CookieContainer = cookieContainer ?? new CookieContainer();
            httpWebReq.Method = "POST";

            httpWebReq.ContentType = WebService.ValidateContentTypeValue(contentTypeValue) ? contentTypeValue : MediaTypeNamesHelper.Web.Json;
            if (contentTypeCharset != null)
            {
                contentTypeCharset = WebService.ValidateContentTypeCharset(contentTypeCharset) ? contentTypeCharset : MediaTypeNamesHelper.Encoding.UTF8;
                httpWebReq.ContentType = $"{httpWebReq.ContentType};charset={contentTypeCharset}";
            }

            httpWebReq.ContentLength = contentDataBytes.Length;
            httpWebReq.AllowAutoRedirect = true;

            using (Stream stream = httpWebReq.GetRequestStream()) stream.Write(contentDataBytes, 0, contentDataBytes.Length);

            try
            {
                return (HttpWebResponse)httpWebReq.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] It wasn't possible to do the POST request. Exception details: {ex.ToString()}");
                throw ex;
            }
        }

        /// <summary>
        /// Execute a POST <see cref="HttpWebResponse"/> method with a Multipart form data to be sent through the request.
        /// </summary>
        /// <remarks>
        /// Check forum post for examples and more details
        /// https://briangrinstead.com/blog/multipart-form-post-in-c/
        /// </remarks>
        /// <param name="url">URI path to execute the HTTP file request.</param>
        /// <param name="postParameters"><see cref="Dictionary{TKey, TValue}"/> of string and object with parameters to be send as a POST request.</param>
        /// <param name="userAgent">(Optional) Request user agent used in the request.</param>
        /// <param name="cookieContainer">(Optional) Associate a <see cref="CookieContainer"/> to the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="encoding">(Optional) Encoding chosen to transform the content data from a string format to an array of bytes.</param>
        /// <param name="allowAutoRedirect">(Optional) Set if the <see cref="HttpWebRequest"/> should or not follow redirection responses.</param>
        /// <returns>Instance of <see cref="HttpWebResponse"/> or an exception if the <see cref="HttpWebRequest"/> is not a HTTP request.</returns>
        public static HttpWebResponse POSTMultipartFormDataWebRequest(string url, Dictionary<string, object> postParameters, string userAgent = null,
            CookieContainer cookieContainer = null, Encoding encoding = null, bool allowAutoRedirect = false)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            string formDataBoundary = string.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary, encoding);
            return PostForm(url, formData, contentType, userAgent, cookieContainer, allowAutoRedirect);
        }

        /// <summary>
        /// Create the POST form data and execute the <see cref="HttpWebRequest"/>.
        /// </summary>
        /// <param name="url">URI path to execute the HTTP file request.</param>
        /// <param name="formData">Form data constructed as an array of bytes from a stream writing.</param>
        /// <param name="contentType">Set <see cref="HttpWebRequest.ContentType"/> HTTP header for the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="userAgent">(Optional) Request user agent used in the request.</param>
        /// <param name="cookieContainer">(Optional) Associate a <see cref="CookieContainer"/> to the specific <see cref="HttpWebRequest"/>.</param>
        /// <param name="allowAutoRedirect">(Optional) Set if the <see cref="HttpWebRequest"/> should or not follow redirection responses.</param>
        /// <returns>Instance of <see cref="HttpWebResponse"/> or an exception if the <see cref="HttpWebRequest"/> is not a HTTP request.</returns>
        private static HttpWebResponse PostForm(string url, byte[] formData, string contentType, string userAgent = null,
            CookieContainer cookieContainer = null, bool allowAutoRedirect = false)
        {
            if (!(WebRequest.Create(url) is HttpWebRequest httpWebReq)) throw new NullReferenceException("Request is not a HTTP request.");
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());

            // Set up the request properties.
            httpWebReq.Method = "POST";
            if (userAgent != null) httpWebReq.UserAgent = userAgent;
            httpWebReq.CookieContainer = cookieContainer ?? new CookieContainer();
            httpWebReq.ContentLength = formData.Length;
            httpWebReq.AllowAutoRedirect = allowAutoRedirect;
            httpWebReq.ContentType = WebService.ValidateContentTypeValue(contentType) ? contentType : $"{MediaTypeNamesHelper.Web.FormData}; boundary={formDataBoundary}";

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

            // Send the form data to the request.
            using (Stream requestStream = httpWebReq.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return httpWebReq.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// Convert a <see cref="Dictionary{TKey, TValue}"/> of parameters for a POST request directly into a byte array of data to be sent with the right formats.
        /// </summary>
        /// <param name="postParameters"><see cref="Dictionary{TKey, TValue}"/> of string and object values to be converted.</param>
        /// <param name="boundary">Standard text with GUID that needs to be appended to a form data request.</param>
        /// <param name="encoding">Encoding used for <see cref="Stream"/> write and read commands.</param>
        /// <returns>Array of bytes with the data according to the choosen Encoding.</returns>
        public static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary, Encoding encoding)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            Stream formDataStream = new MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter fileToUpload)
                {
                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
    }
}
