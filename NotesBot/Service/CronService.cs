using NotesBot.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotesBot.Service
{
  ///https://ru.wikipedia.org/wiki/Cron
  public class CronService
  {
    private static Timer _timer;
    private static TimeSpan _defaultPushInterval = TimeSpan.FromSeconds(30);

    #region services

    private static MatchPredictionService _marchPredictionService = new MatchPredictionService();

    #endregion

    private CronService() { }

    public static void StartPushing()
    {
      StartPushing(_defaultPushInterval);
    }

    public static void StartPushing(TimeSpan interval)
    {
      var timerCallback = new TimerCallback(StartPushingDo);
      _timer = new Timer(timerCallback, new object(), TimeSpan.FromSeconds(1), interval);
    }

    public static void StopPushing()
    {
      _timer.Dispose();
    }

    private static void StartPushingDo(object obj)
    {
      Test();
      CallMatchPrediction();
    }

    private static void Test()
    {
      BotService.SendNotifyForUsers("TestNotify");
    }

    private static void CallMatchPrediction()
    {
      _marchPredictionService.IsHaveNewCachedPrediction.ContinueWith(@is =>
      {
        if (@is.IsFaulted || @is.IsCanceled || !@is.IsCompleted)
          return;

        if (@is.Result)
        {
          var predictionToSend = _marchPredictionService.GetPrediction();
          BotService.SendNotifyForUsers(predictionToSend);
        }
      });
    }
  }
}
