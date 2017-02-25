using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Web;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //var filePath =
            //    @"https://www.amazon.es/?tag=hydesnav-21&hvadid=155548507604&hvpos=1t1&hvnetw=g&hvrand=7294830519617503672&hvpone=&hvptwo=&hvqmt=e&hvdev=c&hvdvcmdl=&hvlocint=&hvlocphy=20272&hvtargid=kwd-10573980&ref=pd_sl_781oit2196_e";

            var filePath = @"http://sports.williamhill.es/bet_esp/es/betting/y/5/tm/F%C3%BAtbol.html";
            var filePath2 = @"http://sports.williamhill.es/bet_esp/es/betting/y/5/tm/4/F%C3%BAtbol.html";
            var filePath3 = @"http://sports.williamhill.es/bet_esp/es/betting/y/5/tm/5/F%C3%BAtbol.html";
            var filePath4 = @"https://www.betfair.es/sport/football";
            var filePath5 =
                @"https://www.betfair.es/sport/football?action=loadCompetition&modules=multipickavbId@1006&selectedTabType=COUNTRY_CODE_FOOTBALL";
            var filePath6 =
                @"https://www.betfair.es/sport/football?id=117&competitionEventId=259241&action=loadCompetition&modules=multipickavbId@1006&selectedTabType=COMPETITION";

            var webScrapper = new WilliamHillScrapper();
            webScrapper.loadBets(Leagues.PremierLeague);


            foreach (var result in webScrapper.ResultDictionary)
            {
                Console.WriteLine(result.Key);
                foreach (var betMatch in result.Value)
                {
                    var line =
                        $"{betMatch.fecha} {betMatch.hora} {betMatch.LocalTeam} {betMatch.AwayTeam} {betMatch.LocalShare} {betMatch.DrawShare} {betMatch.AwayShare}";

                    Console.WriteLine(line);
                }

                Console.WriteLine("\n");
            }



            Console.ReadLine();
        }
    }
}
