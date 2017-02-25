using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScrapper.Web
{
    public class WilliamHillScrapper : WebAnalizer
    {
        //Ruta que nos lleva a la página de partidos de futbol del día en el nos encontramos.
        //Para ir viendo los días siguientes hay que ir sumandole +1 al 0.
        private const int DAYS = 7;
        private string filePath = @"http://sports.williamhill.es/bet_esp/es/betting/y/5/tm/{0}/F%C3%BAtbol.html";

        #region keys

        private const string KeyLigaSantander = "ip_type_338";
        private const string KeyLiga123 = "ip_type_339";
        private const string KeyPremier = "ip_type_295";
        private const string KeyChampionship = "ip_type_292";
        private const string KeyBundesliga = "ip_type_315";
        private const string KeyBundesliga2 = "ip_type_317";
        private const string KeySerieA = "ip_type_321";
        private const string KeyLigue1 = "ip_type_312";
        private const string KeyLigue2 = "ip_type_314";
        private const string KeyEredivise = "ip_type_306";
        private const string KeyTurquei = "ip_type_325";
        private const string KeyBelgium = "ip_type_180";
        private const string KeyScotlandPremierShip = "ip_type_297";
        // private const string KeyScotlandChampionship = "ip_type_2";

        #endregion


        private List<BetMatch> BetMatchList;
        private string Header;

        private readonly List<string> Keys = new List<string>
        {
            KeyLigaSantander,KeyLiga123,KeyPremier,KeyChampionship,KeyBundesliga,KeyBundesliga2,
            KeySerieA,KeyLigue1,KeyLigue2,KeyEredivise,KeyTurquei,KeyBelgium,KeyScotlandPremierShip
        };

        public WilliamHillScrapper() { }


        public void loadBets(Leagues league)
        {
            var key = getLeagueKey(league);
            getDataFromWeb(key);
        }


        private void getWebDatas()
        {
            foreach (var key in Keys)
            {
                BetMatchList = new List<BetMatch>();

                for (int day = 0; day < DAYS; day++)
                {
                    var path = string.Format(filePath, day);

                    var data = _webClient.DownloadString(path);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(data);

                    getDataForLigue(key, doc, day);
                }

                ResultDictionary.Add(Header, BetMatchList);
            }
        }


        private void getDataFromWeb(string key)
        {
            BetMatchList = new List<BetMatch>();

            for (int day = 0; day < DAYS; day++)
            {
                var path = string.Format(filePath, day);

                var data = _webClient.DownloadString(path);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(data);

                getDataForLigue(key, doc, day);
            }

            ResultDictionary.Add(Header, BetMatchList);
        }


        private void getDataForLigue(string ligueKey, HtmlDocument doc, int day)
        {
            var node = doc.DocumentNode.SelectSingleNode("//div[@id='" + ligueKey + "']");

            if (node == null) return;

            try
            {
                var cabecera = node.SelectSingleNode(".//h3").InnerText;
                var contenedorTablaPartidos = node.SelectSingleNode(".//div[@id='" + ligueKey + "_mkt_grps']");
                var tablaPartidos = contenedorTablaPartidos.SelectSingleNode(".//table");
                var partidos = tablaPartidos.SelectNodes(".//tbody/tr");

                if (!String.IsNullOrEmpty(cabecera)) Header = cabecera;
                string result = "";

                foreach (HtmlNode tr in partidos)
                {
                    var fecha = "";
                    var hora = "";

                    if (day == 0)
                    {
                        var monthName = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                        fecha = DateTime.Now.Day + " " + monthName;
                        hora = "??:??";
                        //hora = partidos[i].SelectSingleNode(".//td[2]/a[@id='ip_10647773_score']").InnerText;
                    }
                    else
                    {
                        fecha = tr.SelectSingleNode(".//td[1]/span").InnerText;
                        hora = tr.SelectSingleNode(".//td[2]/span").InnerText;
                    }

                    var partido = tr.SelectSingleNode(".//td[3]/a/span").InnerText.Replace("&nbsp;", "").Replace("â", ";").Split(';');

                    var equipoLocal = partido[0];
                    var equipoVisitante = partido[1].Substring(2);
                    var cuota1 = tr.SelectSingleNode(".//td[5]/div/div").InnerText.Replace("\n", "").Replace("\t", "");
                    var cuotaX = tr.SelectSingleNode(".//td[6]/div/div").InnerText.Replace("\n", "").Replace("\t", "");
                    var cuota2 = tr.SelectSingleNode(".//td[7]/div/div").InnerText.Replace("\n", "").Replace("\t", "");

                    var betMatch = new BetMatch(fecha, hora, equipoLocal, equipoVisitante, cuota1, cuotaX, cuota2);
                    BetMatchList.Add(betMatch);

                    result += $"{fecha} {hora} {partido} {cuota1} {cuotaX} {cuota2} \n";
                }

                // ResultDictionary.Add(cabecera, result);
            }
            catch (Exception ex)
            {
                return;
            }
        }


        private string getLeagueKey(Leagues league)
        {
            switch (league)
            {
                case Leagues.LigaSantander:
                    return KeyLigaSantander;

                case Leagues.Liga123:
                    return KeyLiga123;

                case Leagues.PremierLeague:
                    return KeyPremier;

                case Leagues.ChampionShip:
                    return KeyChampionship;

                case Leagues.Bundesliga:
                    return KeyBundesliga;

                case Leagues.Bundesliga2:
                    return KeyBundesliga2;

                case Leagues.SeriaA:
                    return KeySerieA;

                case Leagues.Ligue1:
                    return KeyLigue1;

                case Leagues.Ligue2:
                    return KeyLigue2;

                case Leagues.Eredivise:
                    return KeyEredivise;

                case Leagues.TurqueiLeague:
                    return KeyTurquei;

                case Leagues.BelgiumLeague:
                    return KeyBelgium;

                case Leagues.ScotlandPremierShip:
                    return KeyScotlandPremierShip;

                case Leagues.ScotlandChampoinShip:
                    break;
            }

            return "";
        }
    }
}
