//using Microsoft.Bot.Schema;
//using NotesBot.Interfaces;
//using NotesBot.Service;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NotesBot.Commands
//{
//  public class ForAllCmd : ICommand
//  {
//    public string Description { get; set; }
//    public List<string> CommandsName { get; set; }
//    public bool IsAdmin { get; set; }

//    public Activity Do(Activity activity)
//    {
//      if (activity?.Conversation != null)
//      {
//        BotService.SendForAllUsers(activity.Text);
//        activity.Text = "message was sended to all users";
//      }
//      return activity;
//    }

//    public ForAllCmd()
//    {
//      CommandsName = new List<string> { "/forall", "/fa" };
//      Description = "Pushing message for all users";
//    }
//  }
//}
