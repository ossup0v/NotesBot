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
  public class A_ToLowerCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        if (activity.Text.IsNullOrWhiteSpace())
        {
          await turnContext.SendActivityAsync(MessageFactory.Text($"Type message for command '/tolower'.\n\rExample: /tl NEED make this TExt In Lower REgISTeR"));
          return;
        }
        else
        {
          await turnContext.SendActivityAsync(MessageFactory.Text(activity.Text.ToLower()));
        }
      }
    }

    public A_ToLowerCmd()
    {
      CommandsName = new List<string> { "/tolower", "/tl" };
      Description = "Return text to lower";
    }
  }
}
