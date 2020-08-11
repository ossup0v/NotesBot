using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Attributes;
using NotesBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  [UnacticeCommand]
  public class ReadAllTagsCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; set; }

    public Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      //TODO(Osipov) return all tags to user
      throw new NotImplementedException();
    }

    public ReadAllTagsCmd()
    {
      CommandsName = new List<string> { "/readalltags", "/rag" };
      Description = "Return all tags";
    }
  }
}
