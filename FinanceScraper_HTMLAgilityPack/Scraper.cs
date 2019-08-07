using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FinanceScraper_HTMLAgilityPack
{
    class Scraper
    {
        public static void RunScraper()
        {
            HtmlWeb web = new HtmlWeb();
            string url = "https://www.nasdaq.com/markets/indices/major-indices.aspx";
            HtmlDocument document = web.Load(url);

            HtmlNodeCollection tableNode = document.DocumentNode.SelectNodes("//*[@id=\"left-column-div\"]/div[4]/table/tr");

            foreach (HtmlNode node in tableNode)
            {
                Console.WriteLine(node.InnerText);
            }




            Console.ReadLine();
        }
    }
}
