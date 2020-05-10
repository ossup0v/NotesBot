using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Interfaces
{
  public interface ICommand
  {
    string Description { get; set; }
    List<string> CommandsName { get; set; }
    bool IsAdmin { get; }
    Task Do(ITurnContext<IMessageActivity> turnContext);
  }
}
