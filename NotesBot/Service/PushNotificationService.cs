using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotesBot.Service
{
  public class PushNotificationService
  {
    private static Timer _timer;
    private static TimeSpan _defaultPushInterval = TimeSpan.FromSeconds(30);

    private PushNotificationService() { }

    public static void StartPushing()
    {
      StartPushing(_defaultPushInterval);
    }

    public static void StartPushing(TimeSpan interval)
    {
      var timerCallback = new TimerCallback(StartPushingDo);
      _timer = new Timer(timerCallback, new object(), interval, interval);
      PushNotification_Test();
    }

    public static void StopPushing()
    {
      _timer.Dispose();
    }

    private static void StartPushingDo(object obj)
    {
      PushNotification_Test();
    }

    private static void PushNotification_Test()
    {
      BotService.SendNotifyForUsers("TestNotify");
    }
  }
}
