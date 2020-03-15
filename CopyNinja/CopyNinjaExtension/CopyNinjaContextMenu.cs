using CopyNinjaApp;
using Newtonsoft.Json;
using RestSharp;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CopyNinjaExtension
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.DirectoryBackground)]
    public class CopyNinjaContextMenu : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();

            var CopyNinjaCopy = new ToolStripMenuItem { Text = "Copy File to SkyNet" };

            var CopyNinjaPaste = new ToolStripMenuItem { Text = "Paste File from SkyNet" };

            CopyNinjaCopy.Click += (sender, args) => CopyFile();

            CopyNinjaPaste.Click += (sender, args) => PasteFile();

            CopyNinjaCopy.Enabled = SelectedItemPaths.Any();

            CopyNinjaPaste.Enabled = !SelectedItemPaths.Any();

            menu.Items.Add(CopyNinjaCopy);

            menu.Items.Add(CopyNinjaPaste);

            return menu;
        }

        private void CopyFile()
        {
            try
            {
                if (!SelectedItemPaths.Any())
                    return;

                var location = new Uri(Assembly.GetExecutingAssembly().CodeBase);

                var info = new FileInfo(location.AbsolutePath).Directory;

                var json = File.ReadAllText(info.FullName + @"\config.json");

                var objConfig = (Config)JsonConvert.DeserializeObject(json, typeof(Config));

                var client = new RestClient($"{objConfig.Url}/api/clipboard")
                {
                    UserAgent = "Sia-Agent"
                };

                var request = new RestRequest()
                {
                    Method = Method.POST,
                    RequestFormat = DataFormat.Json
                };

                request.AddJsonBody(SelectedItemPaths.First());

                var response = client.Execute(request);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        MessageBox.Show("File Copied");
                        break;

                    default:
                        MessageBox.Show("Copy Failed :" + response.StatusCode);
                        break;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "ERROR");
            }
        }

        private void PasteFile()
        {
            try
            {              

                var location = new Uri(Assembly.GetExecutingAssembly().CodeBase);

                var info = new FileInfo(location.AbsolutePath).Directory;

                var json = File.ReadAllText(info.FullName + @"\config.json");

                var objConfig = (Config)JsonConvert.DeserializeObject(json, typeof(Config));

                var client = new RestClient($"{objConfig.Url}/api/clipboard")
                {
                    UserAgent = "Sia-Agent"
                };

                var request = new RestRequest()
                {
                    Method = Method.GET,
                    RequestFormat = DataFormat.Json
                };

                request.AddQueryParameter("folder", FolderPath);

                var response = client.Execute(request);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        MessageBox.Show("File Pasted");
                        break;

                    default:
                        MessageBox.Show("Paste Failed :" + response.StatusCode);
                        break;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "ERROR");
            }
        }
    }
}
