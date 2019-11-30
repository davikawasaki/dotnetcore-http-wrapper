using CustomHttpWrapperLibrary.Helpers;

namespace CustomHttpWrapperLibrary.Utils
{
    /// <summary>
    /// Auxiliary web service for validations.
    /// </summary>
    public class WebService
    {
        /// <summary>
        /// Receive and check if a content type value is valid for processing.
        /// </summary>
        /// <param name="contentTypeVal"><see cref="System.Net.HttpWebRequest"/> ContentType passed as string.</param>
        /// <returns>Validation status for the param content type.</returns>
        public static bool ValidateContentTypeValue(string contentTypeVal)
        {
            if (contentTypeVal == null) return false;
            if (contentTypeVal.Contains(MediaTypeNamesHelper.Web.FormData)) return true;

            switch (contentTypeVal)
            {
                case MediaTypeNamesHelper.Web.ApplicationXml:
                    return true;
                case MediaTypeNamesHelper.Web.FormData:
                    return true;
                case MediaTypeNamesHelper.Web.FormUrlEncoded:
                    return true;
                case MediaTypeNamesHelper.Web.Js:
                    return true;
                case MediaTypeNamesHelper.Web.Json:
                    return true;
                case MediaTypeNamesHelper.Web.TextHtml:
                    return true;
                case MediaTypeNamesHelper.Web.TextPlain:
                    return true;
                case MediaTypeNamesHelper.Web.TextXml:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Receive and check if a content type charset value is valid for processing.
        /// </summary>
        /// <param name="contentTypeCharset"><see cref="System.Net.HttpWebRequest"/> ContentType charset passed as string.</param>
        /// <returns>Validation status for the param content type charset.</returns>
        public static bool ValidateContentTypeCharset(string contentTypeCharset)
        {
            switch (contentTypeCharset)
            {
                case MediaTypeNamesHelper.Encoding.ASCII:
                    return true;
                case MediaTypeNamesHelper.Encoding.BigEndianUnicode:
                    return true;
                case MediaTypeNamesHelper.Encoding.ISO88591:
                    return true;
                case MediaTypeNamesHelper.Encoding.Unicode:
                    return true;
                case MediaTypeNamesHelper.Encoding.UnicodeUTF16LE:
                    return true;
                case MediaTypeNamesHelper.Encoding.UTF32:
                    return true;
                case MediaTypeNamesHelper.Encoding.UTF8:
                    return true;
                case MediaTypeNamesHelper.Encoding.UTF7:
                    return false;
                default:
                    return false;
            }
        }
    }
}
