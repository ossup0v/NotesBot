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
  public class A_ToUpperCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        if (activity.Text.IsNullOrEmptyOrWhiteSpace())
        {
          await turnContext.SendActivityAsync(MessageFactory.Text($"Type message for command '/toupper'.\n\rExample: /tu NEED make this TExt In Upper REgISTeR"));
          return;
        }
        else
        {
          await turnContext.SendActivityAsync(MessageFactory.Text(activity.Text.ToUpper()));
        }
      }
    }

    public A_ToUpperCmd()
    {
      CommandsName = new List<string> { "/toupper", "/tu" };
      Description = "Return text to upper";
    }
  }
}
