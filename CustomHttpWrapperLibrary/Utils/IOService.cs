using System;
using System.IO;
using CustomHttpWrapperLibrary.Helpers;

namespace CustomHttpWrapperLibrary.Utils
{
    /// <summary>
    /// I/O service methods.
    /// </summary>
    public class IOService
    {
        /// <summary>
        /// Get file name extension from <see cref="System.Net.Http.HttpResponseMessage"/> with or without a dot annotation according to the content type.
        /// </summary>
        /// <param name="contentTypeMediaType">Content type media type in a string format.</param>
        /// <param name="contentDispositionFileName">(Optional) <see cref="System.Net.Http.HttpResponseMessage.Content"/> Headers.ContentDisposition.FileName to be extracted extension for <see cref="MediaTypeNamesHelper.Application.Octet"/> cases.</param>
        /// <param name="withDotAnnotation">(Optional) Status that indicates the usage of a dot in the extension.</param>
        /// <returns>File name extension extracted or empty if not found.</returns>
        public static string GetContentTypeExtension(string contentTypeMediaType, string contentDispositionFileName = null, bool withDotAnnotation = true)
        {
            switch (contentTypeMediaType)
            {
                case MediaTypeNamesHelper.Application.Octet:
                    if (contentDispositionFileName == null) return "";

                    string extractedExtension = "";
                    string[] fileNameSplitted;
                    fileNameSplitted = contentDispositionFileName.Split(".");
                    if (fileNameSplitted.Length > 0) extractedExtension = fileNameSplitted[fileNameSplitted.Length];

                    return withDotAnnotation ? $".{extractedExtension}" : extractedExtension;
                case MediaTypeNamesHelper.Application.Json:
                    return withDotAnnotation ? ".json" : "json";
                case MediaTypeNamesHelper.Application.Pdf:
                    return withDotAnnotation ? ".pdf" : "pdf";
                case MediaTypeNamesHelper.Application.Rtf:
                    return withDotAnnotation ? ".rtf" : "rtf";
                case MediaTypeNamesHelper.Application.Soap:
                    return withDotAnnotation ? ".xml" : "xml";
                case MediaTypeNamesHelper.Application.Xml:
                    return withDotAnnotation ? ".xml" : "xml";
                case MediaTypeNamesHelper.Application.Zip:
                    return withDotAnnotation ? ".zip" : "zip";
                case MediaTypeNamesHelper.Application.Exe:
                    return withDotAnnotation ? ".exe" : "exe";
                case MediaTypeNamesHelper.Application.Rar:
                    return withDotAnnotation ? ".rar" : "rar";
                case MediaTypeNamesHelper.Application.SevenZip:
                    return withDotAnnotation ? ".7z" : "7z";
                case MediaTypeNamesHelper.Application.ExcelXlsx:
                    return withDotAnnotation ? ".xlsx" : "xlsx";
                case MediaTypeNamesHelper.Application.ExcelXls:
                    return withDotAnnotation ? ".xls" : "xls";
                case MediaTypeNamesHelper.Application.ExcelXlsm:
                    return withDotAnnotation ? ".xlsm" : "xlsm";
                case MediaTypeNamesHelper.Application.WordDoc:
                    return withDotAnnotation ? ".doc" : "doc";
                case MediaTypeNamesHelper.Application.WordDocx:
                    return withDotAnnotation ? ".docx" : "docx";
                case MediaTypeNamesHelper.Application.WordDocm:
                    return withDotAnnotation ? ".docm" : "docm";
                case MediaTypeNamesHelper.Application.VisioVsd:
                    return withDotAnnotation ? ".vsd" : "vsd";
                case MediaTypeNamesHelper.Application.VisioVsdx:
                    return withDotAnnotation ? ".vsdx" : "vsdx";
                case MediaTypeNamesHelper.Application.Jar:
                    return withDotAnnotation ? ".jar" : "jar";
                case MediaTypeNamesHelper.Application.Jnlp:
                    return withDotAnnotation ? ".jnlp" : "jnlp";
                case MediaTypeNamesHelper.Application.Javascript:
                    return withDotAnnotation ? ".js" : "js";

                case MediaTypeNamesHelper.Image.Gif:
                    return withDotAnnotation ? ".gif" : "gif";
                case MediaTypeNamesHelper.Image.Jpeg:
                    return withDotAnnotation ? ".jpeg" : "jpeg";
                case MediaTypeNamesHelper.Image.Tiff:
                    return withDotAnnotation ? ".tiff" : "tiff";
                case MediaTypeNamesHelper.Image.Png:
                    return withDotAnnotation ? ".png" : "png";
                case MediaTypeNamesHelper.Image.PngCitrix:
                    return withDotAnnotation ? ".png" : "png";
                case MediaTypeNamesHelper.Image.PngX:
                    return withDotAnnotation ? ".png" : "png";
                case MediaTypeNamesHelper.Image.Bmp:
                    return withDotAnnotation ? ".bmp" : "bmp";

                case MediaTypeNamesHelper.Text.Css:
                    return withDotAnnotation ? ".css" : "css";
                case MediaTypeNamesHelper.Text.Csv:
                    return withDotAnnotation ? ".csv" : "csv";
                case MediaTypeNamesHelper.Text.Html:
                    return withDotAnnotation ? ".html" : "html";
                case MediaTypeNamesHelper.Text.Plain:
                    return withDotAnnotation ? ".txt" : "txt";
                case MediaTypeNamesHelper.Text.RichText:
                    return withDotAnnotation ? ".rtf" : "rtf";
                case MediaTypeNamesHelper.Text.Xml:
                    return withDotAnnotation ? ".xml" : "xml";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Receive a file in a array of bytes format and save in a file full name path.
        /// </summary>
        /// <param name="fileNameFullPath">File path with drive, filename and extension to indicate where the file needs to be saved.</param>
        /// <param name="byteArray">File in array of bytes' format.</param>
        /// <returns>Status of the save.</returns>
        public static bool SaveByteArrayToFile(string fileNameFullPath, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileNameFullPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] It wasn't possible to save the array to file. Exception details: {ex.ToString()}");
                return false;
            }
        }
    }
}
