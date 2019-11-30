using System.Net.Mime;

namespace CustomHttpWrapperLibrary.Helpers
{
    /// <summary>
    /// Helper with media type names separated by modules: text, application, image, web and encoding.
    /// </summary>
    public class MediaTypeNamesHelper
    {
        /// <summary>
        /// Specifies the type of text data in an email message attachment.
        /// </summary>
        public static class Text
        {
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Text data is in plain text format.
            /// </summary>
            public const string Plain = MediaTypeNames.Text.Plain;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Text data is in HTML format.
            /// </summary>
            public const string Html = MediaTypeNames.Text.Html;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Text data is in XML format.
            /// </summary>
            public const string Xml = MediaTypeNames.Text.Xml;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Rich Text Format (RTF).
            /// </summary>
            public const string RichText = MediaTypeNames.Text.RichText;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Cascading Style Sheets (CSS).
            /// </summary>
            public const string Css = "text/css";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Comma-Seperated Values (CSV).
            /// </summary>
            public const string Csv = "text/csv";
        }

        /// <summary>
        /// Specifies the kind of application data in an email message attachment.
        /// </summary>
        public static class Application
        {
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a SOAP document.
            /// </summary>
            public const string Soap = MediaTypeNames.Application.Soap;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is not interpreted.
            /// </summary>
            public const string Octet = MediaTypeNames.Application.Octet;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Rich Text Format (RTF).
            /// </summary>
            public const string Rtf = MediaTypeNames.Application.Rtf;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Portable Document Format (PDF).
            /// </summary>
            public const string Pdf = MediaTypeNames.Application.Pdf;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is in a zip compressed file.
            /// </summary>
            public const string Zip = MediaTypeNames.Application.Zip;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Javascript Object Notation document (JSON).
            /// </summary>
            public const string Json = MediaTypeNames.Application.Json;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is an Extensible Markup Language document (XML).
            /// </summary>
            public const string Xml = MediaTypeNames.Application.Xml;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a 7zip compressed file.
            /// </summary>
            public const string SevenZip = "application/x-7z-compressed";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Java Archive File (JAR).
            /// </summary>
            public const string Jar = "application/java-archive";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Java Bytecode File (JVM class).
            /// </summary>
            public const string JavaVM = "application/java-vm";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Java Network Launching Protocol File (JNLP).
            /// </summary>
            public const string Jnlp = "application/x-java-jnlp-file";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Java Source File (JS).
            /// </summary>
            public const string Javascript = "application/javascript";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Application (EXE).
            /// </summary>
            public const string Exe = "application/x-msdownload";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Access File (MDB).
            /// </summary>
            public const string Access = "application/x-msaccess";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Excel File (XLS).
            /// </summary>
            public const string ExcelXls = "application/vnd.ms-excel";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Excel Add-in File (XLAM).
            /// </summary>
            public const string ExcelXlam = "application/vnd.ms-excel.addin.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Excel Binary Workbook File (XLSB).
            /// </summary>
            public const string ExcelXlsb = "application/vnd.ms-excel.sheet.binary.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Excel Macro-Enabled Template File (XLTM).
            /// </summary>
            public const string ExcelXltm = "application/vnd.ms-excel.template.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Excel Macro-Enabled Workbook File (XLSM).
            /// </summary>
            public const string ExcelXlsm = "application/vnd.ms-excel.sheet.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Excel Spreadsheet File (XLSX).
            /// </summary>
            public const string ExcelXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint File (PPT).
            /// </summary>
            public const string PowerpointPpt = "application/vnd.ms-powerpoint";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Add-in File (PPAM).
            /// </summary>
            public const string PowerpointPpam = "application/vnd.ms-powerpoint.addin.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Macro-Enabled Slide File (SLDM).
            /// </summary>
            public const string PowerpointSldm = "application/vnd.ms-powerpoint.slide.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Macro-Enabled Presentation File (PPTM).
            /// </summary>
            public const string PowerpointPptm = "application/vnd.ms-powerpoint.presentation.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Macro-Enabled Slideshow File (PPSM).
            /// </summary>
            public const string PowerpointPpsm = "application/vnd.ms-powerpoint.slideshow.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Macro-Enabled Template File (POTM).
            /// </summary>
            public const string PowerpointPotm = "application/vnd.ms-powerpoint.template.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Presentation File (PPTX).
            /// </summary>
            public const string PowerpointPptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Presentation Slide File (SLDX).
            /// </summary>
            public const string PowerpointSldx = "application/vnd.openxmlformats-officedocument.presentationml.slide";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Presentation Slideshow File (PPSX).
            /// </summary>
            public const string PowerpointPpsx = "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Powerpoint Presentation Presentation Template File (POTX).
            /// </summary>
            public const string PowerpointPotx = "application/vnd.openxmlformats-officedocument.presentationml.template";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Visio File (VSD).
            /// </summary>
            public const string VisioVsd = "application/vnd.vision";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Visio 2013 File (VSDX).
            /// </summary>
            public const string VisioVsdx = "application/vnd.vision2013";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Word File (DOC).
            /// </summary>
            public const string WordDoc = "application/msword";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Word Macro-Enabled Document File (docm).
            /// </summary>
            public const string WordDocm = "application/vnd.ms-word.document.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Word Macro-Enabled Template File (dotm).
            /// </summary>
            public const string WordDotm = "application/vnd.ms-word.template.macroenabled.12";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Word Document File (DOCX).
            /// </summary>
            public const string WordDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a Microsoft Word Document Template File (DOTX).
            /// </summary>
            public const string WordDotx = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Application data is a RAR compressed archive (RAR).
            /// </summary>
            public const string Rar = "application/x-rar-compressed";

        }

        /// <summary>
        /// Specifies the type of image data in an email message attachment.
        /// </summary>
        public static class Image
        {
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Graphics Interchange Format (GIF).
            /// </summary>
            public const string Gif = MediaTypeNames.Image.Gif;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Tagged Image File Format (TIFF).
            /// </summary>
            public const string Tiff = MediaTypeNames.Image.Tiff;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Joint Photographic Experts Group (JPEG) format.
            /// </summary>
            public const string Jpeg = MediaTypeNames.Image.Jpeg;
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Portable Network Graphics (PNG) format.
            /// </summary>
            public const string Png = "image/png";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Portable Network Graphics (PNG) citrix format.
            /// </summary>
            public const string PngCitrix = "image/x-citrix-png";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Portable Network Graphics (PNG) x-token format.
            /// </summary>
            public const string PngX = "image/x-png";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Windows Metafile (WMF) format.
            /// </summary>
            public const string Wmf = "image/x-wmf";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Icon (ICO) format.
            /// </summary>
            public const string Ico = "image/x-icon";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Scalable Vector Graphics (SVG) format.
            /// </summary>
            public const string Svg = "image/svg+xml";
            /// <summary>
            /// Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Bitmap Image (BMP) format.
            /// </summary>
            public const string Bmp = "image/bmp";
        }

        /// <summary>
        /// Specifies the content type that can be used for HTTP requests.
        /// </summary>
        public static class Web
        {
            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Web data is in multipart form data.
            /// </summary>
            public const string FormData = "multipart/form-data";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Web data is in application form urlencoded (as params in the URL).
            /// </summary>
            public const string FormUrlEncoded = "application/x-www-form-urlencoded";

            /// <summary>
            /// Specifies that the MediaTypeNamesHelper.Web data is in System.Net.Mime.MediaTypeNames.Text.Plain.
            /// </summary>
            public const string TextPlain = MediaTypeNames.Text.Plain;

            /// <summary>
            /// Specifies that the MediaTypeNamesHelper.Web data is in System.Net.Mime.MediaTypeNames.Application.Json.
            /// </summary>
            public const string Json = MediaTypeNames.Application.Json;

            /// <summary>
            /// Specifies that the MediaTypeNamesHelper.Web data is in MediaTypeNamesHelper.Helper.Application.Javascript.
            /// </summary>
            public const string Js = Application.Javascript;

            /// <summary>
            /// Specifies that the MediaTypeNamesHelper.Web data is in System.Net.Mime.MediaTypeNames.Application.Xml.
            /// </summary>
            public const string ApplicationXml = MediaTypeNames.Application.Xml;

            /// <summary>
            /// Specifies that the MediaTypeNamesHelper.Web data is in System.Net.Mime.MediaTypeNames.Text.Xml.
            /// </summary>
            public const string TextXml = MediaTypeNames.Text.Xml;

            /// <summary>
            /// Specifies that the MediaTypeNamesHelper.Web data is in System.Net.Mime.MediaTypeNames.Text.Html.
            /// </summary>
            public const string TextHtml = MediaTypeNames.Text.Html;
        }

        /// <summary>
        /// Specifies the content type that can be used for Encoding.
        /// </summary>
        public static class Encoding
        {
            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is UTF7.
            /// </summary>
            public const string UTF7 = "UTF-7";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is UTF8.
            /// </summary>
            public const string UTF8 = "UTF-8";
            
            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is UTF32.
            /// </summary>
            public const string UTF32 = "UTF-32";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is ASCII.
            /// </summary>
            public const string ASCII = "ascii";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is Unicode.
            /// </summary>
            public const string Unicode = "unicode";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is Unicode (UTF-16LE) for little endian byte order.
            /// </summary>
            public const string UnicodeUTF16LE = "UTF-16LE";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is BigEndianUnicode (UTF-16BE) for big endian byte order.
            /// </summary>
            public const string BigEndianUnicode = "UTF-16BE";

            /// <summary>
            /// Specifies that MediaTypeNamesHelper.Encoding data is ISO-8859-1.
            /// </summary>
            public const string ISO88591 = "ISO-8859-1";
        }
    }
}
