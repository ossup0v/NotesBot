using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Attributes;
using NotesBot.Interfaces;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  [DontShowInHelp]
  public class HelpCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; }
    public string AvailableCommands { get; set; } = "";

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null)
      {
        var user = DataModel.Users.FirstOrDefault(u => u.UserId == activity.From.Id);
        if (user != null)
          InitAvailableCommands(user.IsAdmin);
        activity.Text = AvailableCommands;
      }
      await turnContext.SendActivityAsync(MessageFactory.Text(activity.Text));
    }

    public HelpCmd()
    {
      CommandsName = new List<string> { "/help", "/h" };
      Description = "Will show you all available commands";
    }

    private void InitAvailableCommands(bool userIsAdmin)
    {
      var commandsStr = new StringBuilder();
      var commandInterface = typeof(ICommand);
      var allCommands = Assembly.GetAssembly(commandInterface)
        .GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.CustomAttributes.All(attr => attr.AttributeType != typeof(UnacticeCommandAttribute)) && t.GetInterface(nameof(ICommand)) != null);
      foreach (var cmd in allCommands)
      {
        if (!cmd.GetCustomAttributes(typeof(DontShowInHelpAttribute)).Any() && !cmd.GetCustomAttributes(typeof(ShowOnlyToAdminAttribute)).Any())
        {
          var actCmd = (ICommand)Activator.CreateInstance(cmd);
          commandsStr.Append($"{actCmd.CommandsName.ToString(" - ")} - {actCmd.Description} \n\r");
        }
      }

      if (userIsAdmin)
      {
        commandsStr.Append($"Admin commands\n\r");
        foreach (var cmd in allCommands)
        {
          if (!cmd.GetCustomAttributes(typeof(DontShowInHelpAttribute)).Any() && cmd.GetCustomAttributes(typeof(ShowOnlyToAdminAttribute)).Any())
          {
            var actCmd = (ICommand)Activator.CreateInstance(cmd);
            commandsStr.Append($"{actCmd.CommandsName.ToString(" - ")} - {actCmd.Description} \n\r");
          }
        }
        commandsStr.Append($"Admin commands\n\r");
      }
      AvailableCommands += commandsStr.ToString();
    }
  }
}
