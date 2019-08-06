using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FinanceScraper_HTMLAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.nasdaq.com/markets/indices/major-indices.aspx");

            HtmlNode tableNode = document.DocumentNode.SelectSingleNode("//table[@class='USMN_MarketIndices']");
            HtmlNode[] trNodes = tableNode.SelectNodes("//tr").ToArray();

            HtmlNode[] cells = tableNode.SelectNodes(".//td").ToArray();

            foreach(HtmlNode tableRow in trNodes)
            {
                Console.WriteLine("Found: " + tableNode.Id);

                
                HtmlNodeCollection tableCells = tableRow.SelectNodes("//td");

                if (tableCells == null)
                {
                    continue;
                }

                foreach(HtmlNode cell in tableCells)
                {
                    Console.WriteLine("Data: " + cell.InnerText);
                }
                
            }


            /*
            for(var item = 0; item < 11; item++)
            {
                Console.WriteLine(nodes[item].ToString());
            }
            */
           // var columns = tableRows[0].SelectNodes("//td/text()");

            // Console.WriteLine(tableNode);
            //var nodes = tableNode.DocumentNode.SelectNodes("//tr").ToArray();

            // HtmlNode fields = document.DocumentNode.SelectNodes("//tr").ToArray();

            /*
            foreach(var item in nodes)
            {
                Console.WriteLine(item.InnerText);
            }
            */

            Console.ReadLine();
        }
    }
}
