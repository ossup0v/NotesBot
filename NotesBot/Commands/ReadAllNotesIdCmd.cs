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
  public class ReadAllNotesCmd : ICommand
  {
    public string Description { get; set; }
    public List<string> CommandsName { get; set; }
    public bool IsAdmin { get; }

    public async Task Do(ITurnContext<IMessageActivity> turnContext)
    {
      var activity = turnContext.Activity as Activity;
      if (activity != null && !String.IsNullOrEmpty(activity.Text))
      {
        var allNamesAndIds = DataModel.Notes?.Where(x => x?.UserId == activity?.From?.Id)?.Select(x => new Tuple<string, int>(x.EntryName, x.Id))?.ToList();
        if (allNamesAndIds == null || allNamesAndIds?.Count == 0)
          await turnContext.SendActivityAsync(MessageFactory.Text("Have zero notes at this moment.."));
        else
        {
          var builder = new StringBuilder();
          builder.Append($"Finded {allNamesAndIds.Count()} entries.\n\r");
          foreach (var nameAndId in allNamesAndIds)
          {
            builder.Append($"Entry name: {nameAndId.Item1} id: {nameAndId.Item2}.\n\r");
          }
          await turnContext.SendActivityAsync(MessageFactory.Text(builder.ToString()));

        }
      }
      else
        await turnContext.SendActivityAsync(MessageFactory.Text("Current command is not implement."));
    }

    public ReadAllNotesCmd()
    {
      CommandsName = new List<string> { "/readallnotes", "/ran" };
      Description = "Return all notes names with id";
    }
  }
}
