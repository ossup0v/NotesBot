﻿using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Models
{
  [Serializable]
  [Table("Users")]
  public class User
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FromId { get; set; }
    public string FromName { get; set; }
    public string ChannelId { get; set; }
    public string ServiceUrl { get; set; }
    public string Conversation { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSubscribedToNotifications { get; set; }

    public User() { }

    public User(Activity activity)
    {
      if (activity != null)
      {
        UserId = activity.From.Id;
        UserName = activity.From.Name;
        FromId = activity.Recipient.Id;
        FromName = activity.Recipient.Name;
        ChannelId = activity.ChannelId;
        ServiceUrl = activity.ServiceUrl;
        Conversation = activity.Id;
      }
    }
  }
}
