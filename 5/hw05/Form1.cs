using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace hw05
{
    public partial class Form1 : Form
    {
        private readonly object lockObject = new object();
        private ConcurrentBag<string> crawledUrls = new ConcurrentBag<string>();
        private Dictionary<string, List<string>> phoneNumberData = new Dictionary<string, List<string>>();
        private int phoneNumbersCount = 0;

        public Form1()
        {
            InitializeComponent();
            crawledUrls = new ConcurrentBag<string>();
            phoneNumberData = new Dictionary<string, List<string>>();
        }

        private async void btn_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;

            if (!string.IsNullOrEmpty(keyword))
            {
                crawledUrls = new ConcurrentBag<string>();
                phoneNumberData.Clear();
                phoneNumbersCount = 0;

                await Task.Run(() => Crawl(keyword));

                DisplayCrawledUrls();
            }
            else
            {
                MessageBox.Show("请输入搜索关键字。");
            }
        }

        private void Crawl(string keyword)
        {
            int page = 1;
            while (phoneNumbersCount < 50)
            {
                string searchUrl = $"https://www.bing.com/search?q={keyword}&first={((page - 1) * 10) + 1}";

                try
                {
                    string htmlContent = DownloadHtml(searchUrl);

                    // 提取搜索结果页面的网址
                    List<string> resultUrls = ExtractResultUrls(htmlContent);

                    // 访问每个搜索结果页面以查找电话号码
                    foreach (string resultUrl in resultUrls)
                    {
                        string resultHtmlContent = DownloadHtml(resultUrl);
                        ExtractPhoneNumbers(resultHtmlContent, resultUrl);
                    }

                    page++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"爬取第 {page} 页时发生错误：{ex.Message}");
                    break;
                }
            }
        }

        private string DownloadHtml(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync(url).Result;
            }
        }

        private List<string> ExtractResultUrls(string htmlContent)
        {
            List<string> resultUrls = new List<string>();

            // 使用 HtmlAgilityPack 解析 HTML 内容
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlContent);

            // 提取搜索结果的网址
            foreach (HtmlNode linkNode in doc.DocumentNode.SelectNodes("//h2/a"))
            {
                string url = linkNode.GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(url) && !url.Contains("bing.com"))
                {
                    resultUrls.Add(url);
                }
            }

            return resultUrls;
        }

        private void ExtractPhoneNumbers(string htmlContent, string url)
        {
            string pattern = @"1\d{10}";
            MatchCollection matches = Regex.Matches(htmlContent, pattern);

            foreach (Match match in matches)
            {
                string phoneNumber = match.Value;

                if (!phoneNumberData.ContainsKey(phoneNumber))
                {
                    lock (lockObject)
                    {
                        phoneNumberData[phoneNumber] = new List<string> { url };
                        crawledUrls.Add(url);
                        phoneNumbersCount++;
                    }

                    if (phoneNumbersCount >= 20)
                        return;
                }
                else
                {
                    lock (lockObject)
                    {
                        phoneNumberData[phoneNumber].Add(url);
                        crawledUrls.Add(url);
                    }
                }
            }
        }

        private void DisplayCrawledUrls()
        {
            listBox1.Items.Clear();

            foreach (var kvp in phoneNumberData)
            {
                string phoneNumber = kvp.Key;
                List<string> urls = kvp.Value;

                string displayText = $"{phoneNumber}: {string.Join(", ", urls)}";
                listBox1.Items.Add(displayText);
            }
        }
    }
}
