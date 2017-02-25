using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Web
{
    public abstract class WebAnalizer
    {
        protected readonly MyWebClient _webClient;
        public Dictionary<string, List<BetMatch>> ResultDictionary { get; private set; }

        protected WebAnalizer()
        {
            _webClient = new MyWebClient();
            ResultDictionary = new Dictionary<string, List<BetMatch>>();
        }
    }
}
