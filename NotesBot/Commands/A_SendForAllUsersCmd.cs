using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Attributes;
using NotesBot.Interfaces;
using NotesBot.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  [ShowOnlyToAdmin]
  public class A_SendForAllUsersCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; set; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        if (activity.Text.IsNullOrEmptyOrWhiteSpace())
        {
          await turnContext.SendActivityAsync(MessageFactory.Text($"Type message for command '/sendforallusers'.\n\rExample /sfau This message will get all users!"));
          return;
        }
        BotService.SendForAllUsers(activity.Text);
        await turnContext.SendActivityAsync(MessageFactory.Text($"Message {activity.Text} was sended for all users."));
      }
    }

    public A_SendForAllUsersCmd()
    {
      CommandsName = new List<string> { "/sendforallusers", "/sfau" };
      Description = "Pushing message for all users";
    }
  }
}
