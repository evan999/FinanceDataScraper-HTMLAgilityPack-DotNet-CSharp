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

            using (var context = new FinanceContext())
            {
                foreach (var tableRow in tableNode)
                {
                    //Console.WriteLine(node.InnerText);
                    string stockSymbol = tableRow.SelectSingleNode("td/h3/a").InnerText;
                    Console.WriteLine("Symbol: " + stockSymbol);
                    string nameValue = tableRow.SelectSingleNode("td[2]/a").InnerText;
                    Console.WriteLine("Name: " + nameValue);
                    string indexValue = tableRow.SelectSingleNode("td[3]").InnerText;
                    Console.WriteLine("Index value: " + indexValue);
                    string changeValue = tableRow.SelectSingleNode("td[4]").InnerText.Replace("&nbsp;", "").Replace("&#9660;", " ").Replace("&#9650;", " ");
                    string[] changeString = changeValue.Split(' ');

                    string changeRate = changeString[0];
                    Console.WriteLine("Change rate: " + changeRate);

                    string netPercentValue;
                    if (changeString.Length == 2)
                    {
                        netPercentValue = changeString[1];
                        Console.WriteLine("Net Percent Rate: " + netPercentValue);
                    }
                    else
                    {
                        netPercentValue = "-";
                        Console.WriteLine("Net Percent Rate: " + netPercentValue);
                    }
                    string highIndexValue = tableRow.SelectSingleNode("td[5]").InnerText;
                    Console.WriteLine("High Index Value: " + highIndexValue);
                    string lowIndexValue = tableRow.SelectSingleNode("td[6]").InnerText;
                    Console.WriteLine("Low Index: " + lowIndexValue);

                    DateTime timeStampValue = DateTime.Now;
                    Console.WriteLine("Timestamp: " + timeStampValue);

                    Console.WriteLine(" ");
                    
                    var stockRecord = new NasdaqStock
                    {
                        Symbol = stockSymbol,
                        Name = nameValue,
                        IndexValue = indexValue,
                        Change = changeRate,
                        NetPercent = netPercentValue,
                        High = highIndexValue,
                        Low = lowIndexValue,
                        Timestamp = timeStampValue
                    };
                    context.NasdaqStocks.Add(stockRecord);
                    context.SaveChanges();                   
                }
            }
        }

    }
}   
