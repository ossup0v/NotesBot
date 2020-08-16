using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Attributes;
using NotesBot.Interfaces;
using NotesBot.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesBot.Commands.Admin
{
  [ShowOnlyToAdmin]
  public class A_StopPushingNotificationsCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; set; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        CronService.StopPushing();
        await turnContext.SendActivityAsync(MessageFactory.Text("You have successfully stop pushing the notifications."));
      }
    }

    public A_StopPushingNotificationsCmd()
    {
      CommandsName = new List<string> { "/A_StopPushingNotifications", "/stoppn" };
      Description = "Stop pushing notifications";
    }
  }
}
