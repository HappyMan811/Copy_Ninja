using Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var content = SkyNet.Copy("https://siasky.net/skynet/skyfile/", "", "4a6635f5e6554692a908c7a4945ea798", "clipboard", @"C:\Users\Khanimamba\Desktop\Cake\New folder\Capture.PNG");

            SkyNet.Paste("https://siasky.net/", "", "4a6635f5e6554692a908c7a4945ea798", content.skylink);
        }
    }
}
