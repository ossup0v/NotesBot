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
  public class A_StartPushingNotificationsCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; set; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        PushNotificationService.StartPushing();
        await turnContext.SendActivityAsync(MessageFactory.Text("You have successfully started pushing the notifications."));
      }
    }

    public A_StartPushingNotificationsCmd()
    {
      CommandsName = new List<string> { "/A_StartPushingNotifications", "/startpn" };
      Description = "Start pushing notifications";
    }
  }
}
