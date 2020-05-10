// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using NotesBot.Context;
using NotesBot.Models;
using NotesBot.Service;

namespace NotesBot.Bots
{
  public class EchoBot : ActivityHandler
  {
    private BotService _botService = new BotService();
    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
      try
      {
        var activity = turnContext.Activity as Activity;
        if (activity != null && turnContext?.Activity?.Text != null)
        {
          DataModel.RememberUser(activity, turnContext);
          activity = await _botService.Do(/*activity*/ turnContext);
        }
      }
      catch (Exception ex)
      {
        await turnContext.SendActivityAsync(MessageFactory.Text(ex.Message), cancellationToken);
      }
    }

    protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
      var welcomeText = "Hello and welcome!";
      foreach (var member in membersAdded)
      {
        if (member.Id != turnContext.Activity.Recipient.Id)
        {
          await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
        }
      }
    }
  }
}
