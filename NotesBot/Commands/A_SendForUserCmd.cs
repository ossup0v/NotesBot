using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Attributes;
using NotesBot.Interfaces;
using NotesBot.Models;
using NotesBot.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  [ShowOnlyToAdmin]
  public class A_SendForUsersCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; set; } = true;
    private const string _help = "Type text for command '/sendforuser'.\n\rFirst word after '/sendforuser' is user id or name, all the rest is message.\n\rExample /forall This message will get all users!"; 

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        var userInfoAndMessage = activity.Text.GetFirstWord();
        var userInfo = userInfoAndMessage.Item1;
        var message = userInfoAndMessage.Item2;

        if (activity.Text.IsNullOrEmptyOrWhiteSpace() || userInfo.IsNullOrEmptyOrWhiteSpace() || message.IsNullOrEmptyOrWhiteSpace())
        {
          await turnContext.SendActivityAsync(MessageFactory.Text(_help));
          return;
        }

        var isFindedById = false;

        var user = default(User);

        if (int.TryParse(userInfo, out var id))
        {
          user = DataModel.GetUserByUserId(id.ToString());
          isFindedById = true;
        }
        else
          user = DataModel.Users.FirstOrDefault(v => v.UserName == userInfo);

        var msgFindedBy = isFindedById ? $"by id {userInfo}" : $"by user name {userInfo}";
        if (user != null)
        {
          BotService.SendForUser(user, activity.Text);
          await turnContext.SendActivityAsync(MessageFactory.Text($"Message {activity.Text} for user {msgFindedBy}."));
        }
        else
          await turnContext.SendActivityAsync(MessageFactory.Text($"Can't find user {msgFindedBy}."));
      }
    }

    public A_SendForUsersCmd()
    {
      CommandsName = new List<string> { "/sendforuser", "/sfu" };
      Description = "Pushing message for all users";
    }
  }
}
