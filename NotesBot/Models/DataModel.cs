using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Database.MySQLRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesBot.Models
{
  public class DataModel
  {
    #region Users
    public static List<User> Users
    {
      get
      {
        var db = new SqlUserRepository();
        return db.GetAll()?.ToList() ?? new List<User>();
      }
    }

    public static void RememberUser(Activity activity
      , ITurnContext<IMessageActivity> turnContext = null)
    {
      if (activity != null)
      {
        try
        {
          var db = new SqlUserRepository();
          var hasUser = db.GetAll().Any(x => x.UserId == activity.From.Id
                               && x.UserName == activity.From.Name
                               && x.ChannelId == activity.ChannelId);

          if (!hasUser)
          {
            db.Add(new User(activity));
            db.Save();
          }
        }
        catch (Exception ex)
        {
          if (turnContext != null)
            turnContext.SendActivityAsync(MessageFactory.Text(ex.Message));
          Console.WriteLine(ex);
        }
      }
    }

    public static void SetUserIsSubscribeForNotifications(Activity activity
      , bool IsSubscribeForNotifications
      , ITurnContext<IMessageActivity> turnContext = null)
    {
      if (activity != null)
      {
        try
        {
          var db = new SqlUserRepository();
          var userId = activity.From.Id;
          var existsUser = db.GetByUserId(userId);
          if (existsUser != null)
          {
            if (IsSubscribeForNotifications != existsUser.IsSubscribedToNotifications)
            {
              existsUser.IsSubscribedToNotifications = IsSubscribeForNotifications;
              db.Update(existsUser);
              db.Save();
            }
          }
          else
          {
            if (turnContext != null)
              turnContext.SendActivityAsync(MessageFactory.Text($"{nameof(SetUserIsSubscribeForNotifications)} user with id {userId} is not founded"));
          }
        }
        catch (Exception ex)
        {
          if (turnContext != null)
            turnContext.SendActivityAsync(MessageFactory.Text(ex.Message));
          Console.WriteLine(ex);
        }
      }
    }

    public static User GetUserByUserId(string userId)
    {
      var db = new SqlUserRepository();
      return db.GetByUserId(userId);
    }
    #endregion

    #region Notes
    public static List<Note> Notes
    {
      get
      {
        var db = new SqlNoteRepository();
        return db.GetAll()?.ToList() ?? new List<Note>();
      }
    }

    public static int RememberNote(Activity activity
      , string nameNote
      , string entry
      , string tag = ""
      , ITurnContext<IMessageActivity> turnContext = null)
    {
      if (activity != null)
      {
        try
        {
          var db = new SqlUserRepository();
          var user = db.GetAll().FirstOrDefault(x => x.UserId == activity.From.Id
                               && x.UserName == activity.From.Name
                               && x.ChannelId == activity.ChannelId);
          if (user != null)
          {
            var dbNotes = new SqlNoteRepository();
            dbNotes.Add(new Note(user, nameNote, entry, tag));
            return dbNotes.Save();
          }
        }
        catch (Exception ex)
        {
          if (turnContext != null)
            turnContext.SendActivityAsync(MessageFactory.Text(ex.Message));
          Console.WriteLine(ex);
        }
      }
      return 0;
    }

    public static Note GetNoteById(int id)
    {
      var db = new SqlNoteRepository();
      return db.GetById(id);
    }
    #endregion
  }
}
