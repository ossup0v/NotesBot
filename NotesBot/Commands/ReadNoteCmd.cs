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
  public class ReadNoteCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; }

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null)
      {
        if (activity.Text.IsNullOrEmptyOrWhiteSpace())
        {
          await turnContext.SendActivityAsync(MessageFactory.Text($"Type note id for command '/readnote', example /rn 2"));
          return;
        }

        if (Int32.TryParse(activity.Text, out var id))
        {
          var note = DataModel.Notes.Where(x => x?.UserId == activity?.From?.Id)?.FirstOrDefault(x => x.Id == id);
          if (note != null)
          {
            var builder = new StringBuilder();
            builder.Append("Note..\n\r");
            builder.Append(note.EntryName + "\n\r");
            builder.Append(note.Entry + "\n\r");
            await turnContext.SendActivityAsync(MessageFactory.Text(builder.ToString()));
          }
          else
            await turnContext.SendActivityAsync(MessageFactory.Text($"Can't find note by id {id}"));
        }
        else
          await turnContext.SendActivityAsync(MessageFactory.Text($"Can't parse {activity.Text} to int"));
      }
      else
        await turnContext.SendActivityAsync(MessageFactory.Text("Current command is not implement"));
    }

    public ReadNoteCmd()
    {
      CommandsName = new List<string> { "/readnote", "/rn" };
      Description = "Return note";
    }
  }
}
