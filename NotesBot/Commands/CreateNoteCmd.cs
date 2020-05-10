using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Interfaces;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesBot.Commands
{
  public class CreateNoteCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; }

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Text != null)
      {
        var name = "";
        var entry = "";
        var str = activity.Text.Trim();
        var indexSpace = str.IndexOf(' ');

        if (indexSpace == -1)
        {
          name = str;
          entry = "undefined";
        }
        else
        {
          name = str.Substring(0, indexSpace);
          entry = str.Substring(indexSpace + 1, str.Length - indexSpace - 1);
        }
        if(entry.Length >= 4000)
        {
          await turnContext.SendActivityAsync(MessageFactory.Text("Your entry is very long.."));
          return;
        }
        var noteId = DataModel.RememberNote(activity, name, entry, turnContext: turnContext);
        var message = new StringBuilder();
        message.Append("Note was created\n\r");
        message.Append($"Id: {noteId} - Name: {name}\n\r");
        message.Append($"\n\r");
        message.Append($"Entry it self\n\r");
        message.Append($"{entry}");
        await turnContext.SendActivityAsync(MessageFactory.Text(message.ToString()));
      }
      else
        await turnContext.SendActivityAsync(MessageFactory.Text("Current command is not implement"));
    }

    public CreateNoteCmd()
    {
      CommandsName = new List<string> { "/createnote", "/cn" };
      Description = "Create note to save";
    }
  }
}
