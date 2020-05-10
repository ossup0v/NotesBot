using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Attributes;
using NotesBot.Interfaces;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  [ShowOnlyToAdmin]
  public class A_ShowUserIdsCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; } = true;

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Conversation != null)
      {
        var userInfos = DataModel.Users.Select(x => new Tuple<int, string, bool>(x.Id, x.UserName, x.IsAdmin));
        var message = new StringBuilder();
        foreach (var userInfo in userInfos)
        {
          message.Append($"Id: {userInfo.Item1} Name: {userInfo.Item2} IsAdmin: {userInfo.Item3}.\n\r");
        }
        await turnContext.SendActivityAsync(MessageFactory.Text(message.ToString()));
      }
    }

    public A_ShowUserIdsCmd()
    {
      CommandsName = new List<string> { "/showuserids", "/sui" };
      Description = "Return ids of all users";
    }
  }
}
