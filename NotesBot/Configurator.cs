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
    private static bool _isUnited { get; set; } = false;
    public static void Init(IConfiguration configuration)
    {
      if (!_isUnited)
      {
        Configuration = configuration;
        _isUnited = true;
      }
    }
  }
}
