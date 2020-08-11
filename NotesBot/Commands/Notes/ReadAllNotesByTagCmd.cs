//using Microsoft.Bot.Schema;
//using NotesBot.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NotesBot.Commands
//{
//  public class ReadAllNotesByTagCmd : ICommand
//  {
//    public string Description        { get; set; }
//    public List<string> CommandsName { get; set; }
//    public bool IsAdmin              { get; set; }

//    public Activity Do(Activity activity)
//    {
//      //TODO(Osipov) return all notes name with same tag to user
//      return activity;
//    }

//    public ReadAllNotesByTagCmd()
//    {
//      CommandsName = new List<string> { "/readallnotesbytag", "/ranbt" };
//      Description = "Return all notes name by tag";
//    }
//  }
//}
