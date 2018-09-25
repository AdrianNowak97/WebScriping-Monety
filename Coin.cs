using System;
using System.Threading.Tasks;
using TinifyAPI;

namespace WebScriping_Monety2zl
{
    internal class Coin
    {
        public string name { get; set; }
        public string date { get; set; }
        public string quantity { get; set; }
        public string series { get; set; }
        public Task<Source> imageHeadsSmal { get; set; }
        public Task<Source> imageReversSmal { get; set; }
       
        public Coin(string name, string date, string quantity, string series, Task<Source> imageHeadsSmal, Task<Source> imageReversSmal)
        {
            this.name = name;
            this.date = date;
            this.quantity = quantity;
            this.series = series;
            this.imageHeadsSmal = imageHeadsSmal;
            this.imageReversSmal = imageReversSmal;
        }

        public void showName()
        {
            Console.WriteLine(name);
        }

        public void showDate()
        {
            Console.WriteLine(date);
        }

        public void showQuantity()
        {
            Console.WriteLine(quantity);
        }

        public void showSeries()
        {
            Console.WriteLine(series);
        }

        public void showEverythink()
        {
            showName();
            showQuantity();
            showDate();
            showSeries();
            Console.WriteLine();
        }
    }
}