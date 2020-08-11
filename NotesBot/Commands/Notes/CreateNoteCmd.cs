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
    private const string _help = "Type text for command '/createnote'.\n\rFirst word after '/cn' is name of note, all the rest is content of note.\n\rExample: /createnote BuyList milk, bread, eggs.";

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && activity.Text != null)
      {
        var NoteNameAndEntry = activity.Text.GetFirstWord();
        if (NoteNameAndEntry.Item1.IsNullOrWhiteSpace() || NoteNameAndEntry.Item2.IsNullOrWhiteSpace())
        {
          await turnContext.SendActivityAsync(MessageFactory.Text(_help));
          return;
        }

        if (NoteNameAndEntry.Item2.Length >= 4000)
        {
          await turnContext.SendActivityAsync(MessageFactory.Text("Your note content is too long.."));
          return;
        }

        var noteId = DataModel.RememberNote(activity, NoteNameAndEntry.Item1, NoteNameAndEntry.Item2, turnContext: turnContext);
        var message = new StringBuilder();
        message.Append("Note was created\n\r");
        message.Append($"Id: {noteId} - Name: {NoteNameAndEntry.Item1}\n\r");
        message.Append($"\n\r");
        message.Append($"Entry it self\n\r");
        message.Append($"{NoteNameAndEntry.Item2}");
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
