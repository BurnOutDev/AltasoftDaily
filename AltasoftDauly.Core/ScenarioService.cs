using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AltasoftDaily.Domain.POCO.Json;

namespace AltasoftAPI
{
    public static class ScenarioService
    {
        public static IEnumerable<Scenario> GetScenarioByLoan(int loanId)
        {
            var url = "http://172.16.48.200:15005/Ext/BusinessCreditExtensionService/scenario?id=";
            return JsonConvert.DeserializeObject<IEnumerable<Scenario>>(new WebClient().DownloadString(url + loanId));
        }

        public static IEnumerable<Scenario> GetScenarioPreviewByLoan(int loanId)
        {
            var url = "http://172.16.48.200:15005/Ext/BusinessCreditExtensionService/scenario/preview?id=";
            return JsonConvert.DeserializeObject<IEnumerable<Scenario>>(new WebClient().DownloadString(url + loanId));
        }
    }
}
