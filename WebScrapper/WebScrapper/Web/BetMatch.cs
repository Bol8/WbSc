using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Web
{
    public class BetMatch
    {
        public string fecha { get; }
        public string hora { get; }
        public string LocalTeam { get; }
        public string AwayTeam { get; }
        public string LocalShare { get; }
        public string DrawShare { get; }
        public string AwayShare { get; }

        public BetMatch(string fecha, string hora, string localTeam, string awayTeam, string localShare, string drawShare, string awayShare)
        {
            this.fecha = fecha;
            this.hora = hora;
            LocalTeam = localTeam;
            AwayTeam = awayTeam;
            LocalShare = localShare;
            DrawShare = drawShare;
            AwayShare = awayShare;
        }


    }
}
