using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Interfaces;
using NotesBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  public class UnsubscribeForNotifyCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; }

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null)
      {
        DataModel.SetUserIsSubscribeForNotifications(activity, false, turnContext);
        await turnContext.SendActivityAsync(MessageFactory.Text("You have successfully unsubscribed to the notifications."));
      }
      else
        await turnContext.SendActivityAsync(MessageFactory.Text("Current command is not implement."));
    }

    public UnsubscribeForNotifyCmd()
    {
      CommandsName = new List<string> { "/UnsubscribeForNotify", "/usfn" };
      Description = "Unsubscribe for notifications";
    }
  }
}
