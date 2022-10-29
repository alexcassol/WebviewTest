using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.IO;


namespace WebviewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);

            string script = File.ReadAllText("Mouse.js");
            await webView21.CoreWebView2.ExecuteScriptAsync(script);

            webView21.WebMessageReceived += WebView21_WebMessageReceived;
            webView21.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
        }

        private void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            string script = File.ReadAllText("Mouse.js");
            webView21.CoreWebView2.ExecuteScriptAsync(script);
        }

        private void WebView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            JsonObject jsonObject = JsonConvert.DeserializeObject<JsonObject>(e.WebMessageAsJson);
            switch (jsonObject.Key)
            {
                case "click":
                    MessageBox.Show(jsonObject.Value);
                    break;

            }
        }

        public struct JsonObject
        {
            public string Key;
            public string Value;
        }
          
    }
}
