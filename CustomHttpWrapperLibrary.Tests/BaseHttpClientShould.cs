using CustomHttpWrapperLibrary.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomHttpWrapperLibrary.Tests
{
    public class BaseHttpClientShould
    {
        private readonly string UNIT_TESTING_DRIVER_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [Test]
        [TestCase(true, "https://qa.website.com/")]
        [TestCase(true, "https://website.com/")]
        public void HaveConstructorProperlyInstantiated(bool useCookies, string clientBaseAddress)
        {
            var stu = new BaseHttpClient(useCookies, null, null, null, clientBaseAddress);
            Assert.That(stu.ClientBaseAddress, Is.Not.Null);
            Assert.That(stu.DefaultRequestHeaders, Is.Null);
        }

        [Test]
        [TestCase(true, "https://qa.website.com/")]
        [TestCase(true, "https://website.com/")]
        public void HaveClientProperlyUpdated(bool useCookies, string clientBaseAddress)
        {
            var stu = new BaseHttpClient(useCookies, null, null, null, clientBaseAddress);
            Assert.That(stu.ClientBaseAddress, Is.Not.Null);
            Assert.That(stu.DefaultRequestHeaders, Is.Null);

            stu.UpdateHttpClientHandler(!useCookies, new CookieContainer(), new List<MediaTypeWithQualityHeaderValue> { new MediaTypeWithQualityHeaderValue(MediaTypeNamesHelper.Web.Json) }, clientBaseAddress);
            Assert.That(stu.UseCookies, Is.EqualTo(!useCookies));
            Assert.That(stu.ClientBaseAddress, Is.Not.Null);
            Assert.That(stu.ClientCookies, Is.Not.Null);
            Assert.That(stu.DefaultRequestHeaders, Is.Not.Null);
        }

        [Test]
        [TestCase(true, "https://qa.website.com/", "staticresources/favicon.ico", "favicon.ico")]
        public async Task DownloadFileToReleaseTestFolder(bool useCookies, string clientBaseAddress, string fileEndpoint, string fileName)
        {
            string finalDownloadedPathExpected = $"{UNIT_TESTING_DRIVER_PATH}\\{fileName}";
            var stu = new BaseHttpClient(useCookies, null, null, null, clientBaseAddress);
            string downloadedPath = await stu.GetAsyncDownloadFile(fileEndpoint, UNIT_TESTING_DRIVER_PATH, fileName);
            Assert.That(downloadedPath, Is.Not.Null);
            Assert.That(downloadedPath, Is.EqualTo(finalDownloadedPathExpected));
            Assert.That(File.Exists(finalDownloadedPathExpected), Is.True);
            File.Delete(finalDownloadedPathExpected);
            Assert.That(File.Exists(finalDownloadedPathExpected), Is.False);
        }

        [Test]
        [TestCase(true, "http://webiste.com/", "fullpdf/", "file.pdf")]
        [TestCase(true, "https://qa.website.com/", "filedownload/123", "file.zip")]
        public async Task DownloadFileSpecificToReleaseTestFolder(bool useCookies, string clientBaseAddress, string fileEndpoint, string fileName)
        {
            string finalDownloadedPathExpected = $"{UNIT_TESTING_DRIVER_PATH}\\{fileName}";
            var stu = new BaseHttpClient(useCookies, null, null, null, clientBaseAddress);
            string downloadedPath = await stu.GetAsyncDownloadFile(fileEndpoint, UNIT_TESTING_DRIVER_PATH, fileName);
            Assert.That(downloadedPath, Is.Not.Null);
            Assert.That(downloadedPath, Is.EqualTo(finalDownloadedPathExpected));
            Assert.That(File.Exists(finalDownloadedPathExpected), Is.True);
            File.Delete(finalDownloadedPathExpected);
            await Task.Delay(2);
            Assert.That(File.Exists(finalDownloadedPathExpected), Is.False);
        }
    }
}
