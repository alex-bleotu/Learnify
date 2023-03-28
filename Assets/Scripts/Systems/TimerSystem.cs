using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Diagnostics;
using System;
using UnityEngine;
using TMPro;

public class TimerSystem : MonoBehaviour
{
    public static Timer publicTimer;

    public static void TimerStartPublic(int milliseconds, Action callback)
    {
        publicTimer = new Timer(milliseconds);
        publicTimer.AutoReset = false;
        publicTimer.Elapsed += (sender, e) => callback();
        publicTimer.Start();
    }

    public static void TimerStop()
    {
        publicTimer.Stop();
    }

    public static void TimerStart(int milliseconds, Action callback)
    {
        Timer timer = new Timer(milliseconds);
        timer.AutoReset = false;
        timer.Elapsed += (sender, e) => callback();
        timer.Start();
    }

    public static void CountUpText(int start, int target, float duration, TMP_Text text, string format)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < duration)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => { text.text = String.Format(format, start + (int)(1f * (target - start) * (stopwatch.ElapsedMilliseconds / duration))); });
        }
        stopwatch.Stop();

        UnityMainThreadDispatcher.Instance().Enqueue(() => { text.text = String.Format(format, target); });

        UnityMainThreadDispatcher.Instance().Destroy();
    }

    public static Stopwatch publicStopWatch;

    public static void StartStopWatch()
    {
        publicStopWatch = new Stopwatch();
        publicStopWatch.Start();
    }

    public static void StopStopWatch()
    {
        publicStopWatch.Stop();
    }

    public static void ResumeStopWatch()
    {
        publicStopWatch.Start();
    }

    public static long GetStopWatchTime()
    {
        return publicStopWatch.ElapsedMilliseconds;
    }

    public static TimeSpan GetTime()
    {
        return publicStopWatch.Elapsed;
    }

    public static void FillUpImage(float start, float target, float duration, UnityEngine.UI.Image image)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < duration)
            UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = start + 1f * (target - start) * (stopwatch.ElapsedMilliseconds / duration); });

        stopwatch.Stop();

        UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = target; });

        UnityMainThreadDispatcher.Instance().Destroy();
    }

    public static void FillUpLoadingBar(float duration, UnityEngine.UI.Image image)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = 1f; });

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (stopwatch.ElapsedMilliseconds < duration)
            UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = 1f - 1f * (stopwatch.ElapsedMilliseconds / duration); });

        stopwatch.Stop();

        UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = 0f; });

        UnityMainThreadDispatcher.Instance().Destroy();
    }
}
