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
            var content = SkyNet.Copy("https://siasky.net/skynet/skyfile/", "clipboard", @"C:\Users\Khanimamba\Desktop\Cake\New folder\Example.txt");

            SkyNet.Paste("https://siasky.net/", content);
        }
    }
}
