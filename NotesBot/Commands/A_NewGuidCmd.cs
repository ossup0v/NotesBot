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
  [ShowOnlyToAdmin]
  public class A_NewGuidCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        await turnContext.SendActivityAsync(MessageFactory.Text(Guid.NewGuid().ToString()));
      }
    }

    public A_NewGuidCmd()
    {
      CommandsName = new List<string> { "/newguid", "/ng" };
      Description = "Return bew guid";
    }
  }
}
