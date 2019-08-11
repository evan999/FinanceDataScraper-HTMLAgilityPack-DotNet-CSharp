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
                    string changeValue = tableRow.SelectSingleNode("td[4]").InnerText.Replace("&nbsp;", "").Replace("&#9660;", " ");
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
                    // Console.WriteLine("Change value: " + changeValue);
                    // int changeLength = changeValue.Length;
                    string highIndexValue = tableRow.SelectSingleNode("td[5]").InnerText;
                    Console.WriteLine("High Index Value: " + highIndexValue);
                    string lowIndexValue = tableRow.SelectSingleNode("td[6]").InnerText;
                    Console.WriteLine("Low Index: " + lowIndexValue);

                    DateTime timeStampValue = DateTime.Now;
                    Console.WriteLine("Timestamp: " + timeStampValue);

                    
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
                    
                    // Console.WriteLine("change length: " + changeLength);





                    //int cutString = 4;

                    // string change = changeValue.Substring(0, cutString);
                    // Console.WriteLine("change: " + change);






                    //string change = changeValue.Substring(0, 5).Trim();

                    // Console.WriteLine("change: " + change);

                    // HtmlNodeCollection cells = node.SelectNodes("td");
                    /*
                    int cellsCount = cells.Count;

                    foreach (HtmlNode cell in cells)
                    {
                        // Console.WriteLine("cell: " + cell.InnerText);
                    }
                    */
                    /*
                    for (int cell = 0; cell < cellsCount; cell++)
                    {
                        string symbolData = cells.ElementAt(0).InnerText;
                        Console.WriteLine("Symbol: " + symbolData);
                        string nameData= cells.ElementAt(1).InnerText;
                        Console.WriteLine("Name: " + symbolData);
                        string indexData = cells.ElementAt(2).InnerText;
                        Console.WriteLine("Index: " + symbolData);
                        /*
                        string changeData = cells[3].InnerText;
                        string netPercentData = cells[4].InnerText;
                        string highData = cells[5].InnerText;
                        string lowData = cells[6].InnerText;
                        DateTime timestampData = DateTime.Now;
                        

                    }
                    */



                    /*
                    foreach (HtmlNode cell in cells)
                    {
                        Console.WriteLine("cell: " + cell.InnerText);
                    }
                    */

                    /*
                    foreach (var cell in node.SelectNodes("//*[@id=\"left-column-div\"]/div[4]/table/tbody"))
                    {

                    }
                    */
                }
                /*
                var stockRecord = new NasdaqStock
                {
                    Symbol = symbolData,
                    LastPrice = lastPriceData,
                    Change = changeData,
                    ChangeRate = changeRateData,
                    Currency = currencyData,
                    MarketTime = marketTimeData,
                    Volume = volumeData,
                    Shares = shareData,
                    AverageVolume = averageVolumeData,
                    MarketCap = marketCapData
                };
                context.Stocks.Add(stockRecord);
                context.SaveChanges();

                */
            }
        }

    }
}   
