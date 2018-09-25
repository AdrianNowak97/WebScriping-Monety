using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TinifyAPI;
using static System.Net.Mime.MediaTypeNames;

namespace WebScriping_Monety2zl
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadData();
            Console.ReadKey();
        }

        private static async void DownloadData()
        {
            String url = "https://pl.wikipedia.org/wiki/Monety_okoliczno%C5%9Bciowe_2_z%C5%82ote_(III_RP)";

            var httpClient = new HttpClient();
            List<Coin> Coins = new List<Coin>();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var yearTabels = htmlDocument.DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("style", "")
                .Equals("width: 90%; margin: auto; text-align: left")).ToList();

            int year = 1995;
            foreach (var yearTabel in yearTabels)
            {
                var coins = yearTabel.Descendants("tr").ToList();
                coins.RemoveAt(0);

                foreach (var coin in coins)
                {
                    var coinDatas = coin.Descendants("td").ToList();

                    String name = Regex.Replace(Regex.Replace(coinDatas[0].InnerHtml, "<.*?>", String.Empty), "\\[.*?\\]", String.Empty).Trim('\r', '\n', '\t');

                    var dayAndMonth = Regex.Replace(coinDatas[2].InnerHtml, "<.*?>", String.Empty).Split(' ');
                    String day = dayAndMonth[0].Trim('\r', '\n', '\t');
                    String month = dayAndMonth[1].Trim('\r', '\n', '\t');
                    String date = dateFormat(day, month, year);

                    String quantity = Regex.Replace(coinDatas[3].InnerHtml, "<.*?>", String.Empty).Trim('\r', '\n', '\t');

                    String series = Regex.Replace(Regex.Replace(coinDatas[1].InnerHtml, "<.*?>", String.Empty), "\\[.*?\\]", String.Empty).Trim('\r', '\n', '\t');
                    /*
                    var imageLinks = coinDatas[5].Descendants("a").ToList();
                    String imageHeadsLink = imageLinks[0].GetAttributeValue("href", "");
                    String imageReversLink = imageLinks[1].GetAttributeValue("href", "");

                    
                    Tinify.Key = " ";
                    var imageHead = downloadImage(imageHeadsLink);
                    var imageRever = downloadImage(imageReversLink);
                    await imageRever.ToFile(@"C:\Users\Adrian\Desktop\zdzisiu.jpg");
                    */
                    //Coin newCoin = new Coin(name, date, quantity, series, imageHead, imageRever);
                    Console.WriteLine(name);
                    Console.WriteLine(date);
                    Console.WriteLine(quantity);
                    Console.WriteLine(series);
                    Console.WriteLine();

                    //newCoin.showEverythink();

                }
                year++;
            }

        }

        private static Task<Source> downloadImage(String link)
        {
            var image = Tinify.FromUrl(link);
            var imageSmal = image.Resize(new
            {
                method = "scale",
                width = 250
            });
            return imageSmal;
        }

        private static string dateFormat(string day, string month, int year)
        {
            if (day.Length <= 1)
            {
                day = "0" + day;
            }

            switch (month)
            {
                case "stycznia": month = "01"; break;
                case "lutego": month = "02"; break;
                case "marca": month = "03"; break;
                case "kwietnia": month = "04"; break;
                case "maja": month = "05"; break;
                case "czerwca": month = "06"; break;
                case "lipca": month = "07"; break;
                case "sierpnia": month = "08"; break;
                case "wrzeœnia": month = "09"; break;
                case "paŸdziernika": month = "10"; break;
                case "listopada": month = "11"; break;
                case "grudnia": month = "12"; break;
                default: break;
            }
            String date = year + "-" + month + "-" + day;
            return date;
        }

    }
}
