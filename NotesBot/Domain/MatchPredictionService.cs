using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotesBot.Domain
{
  public class MatchPredictionService
  {
    public Task<bool> IsHaveNewCachedPrediction => CheckForNewPrediction();

    public MatchPredictionService()
    {
      _client = new HttpClient();
    }

    public string GetPrediction()
    {
      return _cachedPrediction;
    }

    private async Task<bool> CheckForNewPrediction()
    {
      var html = await _client.GetStringAsync(_uri);
      var htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      var matchesDiv = htmlDocument.DocumentNode.Descendants("div")
        .Where(node => node.GetAttributeValue("class", "")
          .Contains("upcomingMatch"))
        .ToList(); 

      foreach (var matchDiv in matchesDiv)
      {

        //some logic here
      }

      _cachedPrediction = html;
      return true;
    }

    private HttpClient _client;
    private Uri _uri = new Uri(_uriString);
    private const string _uriString = "https://www.hltv.org/matches";
    private string _cachedPrediction = "empty";
  }
}
