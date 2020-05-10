using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot
{
  public static class Configurator
  {
    public static IConfiguration Configuration { get; private set; }
    public static MicrosoftAppCredentials BotCredentials { get; private set; }
    public static string SqlConnectionString { get; private set; }
    private static bool _isUnited { get; set; } = false;
    public static void Init(IConfiguration configuration)
    {
      if (!_isUnited)
      {
        Configuration = configuration;

        var appId = ConfigurationBinder.GetValue<string>(Configuration, "MicrosoftAppId");
        var password = ConfigurationBinder.GetValue<string>(Configuration, "MicrosoftAppPassword");
        BotCredentials = new MicrosoftAppCredentials(appId, password);

        SqlConnectionString = ConfigurationBinder.GetValue<string>(Configuration, "ConnectionStrings:SqlConnection");

        _isUnited = true;
      }
    }
  }
}
