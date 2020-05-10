using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using NotesBot.Commands;
using NotesBot.Context;
using NotesBot.Interfaces;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NotesBot.Service
{
  public class BotService
  {
    private readonly List<ICommand> _commands;
    public BotService()
    {
      _commands = new List<ICommand>();

      var commandInterface = typeof(ICommand);
      var allCommands = Assembly.GetAssembly(commandInterface)
        .GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.GetInterface(nameof(ICommand)) != null);

      foreach (var cmd in allCommands)
      {
        _commands.Add((ICommand)Activator.CreateInstance(cmd));
      }
    }

    public async Task<Activity> Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (!String.IsNullOrWhiteSpace(activity.Text))
      {
        var commandNameAndMessage = activity.Text.GetFirstWord();
        var commandStr = commandNameAndMessage.Item1;
        if (commandStr[0] != '/')
          commandStr = '/' + commandStr;

        var command = _commands.FirstOrDefault(cmd => cmd.CommandsName.Any(name => name.Equals(commandStr)));
        if (command != null)
        {
          turnContext.Activity.Text = commandNameAndMessage.Item2;

          var user = DataModel.GetUserByUserId(activity.From.Id);
          if ((command.IsAdmin && user.IsAdmin) || !command.IsAdmin)
            await command.Do(turnContext);
          else
            await new HelpCmd().Do(turnContext);
        }
        else
          await new HelpCmd().Do(turnContext);

      }
      var reply = activity.CreateReply(activity.Text);

      if (activity.Attachments != null)
        reply.Attachments = activity.Attachments;

      return reply;
    }

    public static void SendForUser(User user, string message)
    {
      if (user == null)
        return;

      var client = new ConnectorClient(new Uri(user.ServiceUrl), Configurator.BotCredentials);

      var userAccount = new ChannelAccount(user.UserId, user.UserName);
      var botAccount = new ChannelAccount(user.FromId, user.FromName);

      var activity = new Activity
      {
        Conversation = new ConversationAccount(id: user.UserId),
        ChannelId = user.ChannelId,
        From = userAccount,
        Recipient = botAccount,
        Text = message,
        Id = user.Conversation
      };

      var reply = activity.CreateReply(message);
      if (String.IsNullOrEmpty(activity.ServiceUrl))
      {
        MicrosoftAppCredentials.IsTrustedServiceUrl(activity.ServiceUrl);
      }
      MicrosoftAppCredentials.IsTrustedServiceUrl(user.ServiceUrl);
      client.Conversations.ReplyToActivity(reply);
    }

    public static void SendForAllUsers(string message)
    {
      foreach (var user in DataModel.Users)
      {
        SendForUser(user, message);
      }
    }
  }
}
