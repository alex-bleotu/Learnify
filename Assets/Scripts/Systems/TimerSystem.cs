using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Diagnostics;
using System;
using UnityEngine;
using TMPro;

public class TimerSystem : MonoBehaviour
{
    public static void TimerStart(int milliseconds, Action callback) {
        Timer timer = new Timer(milliseconds);
        timer.AutoReset = false;
        timer.Elapsed += (sender, e) => callback();
        timer.Start();
    }

    public static void CountUpText(int start, int target, float duration, TMP_Text text, string format) {
        Stopwatch timer = new Stopwatch();
        timer.Start();

        while (timer.ElapsedMilliseconds < duration) {
            UnityMainThreadDispatcher.Instance().Enqueue(() => { text.text = String.Format(format, start + (int)(1f * (target - start) * (timer.ElapsedMilliseconds / duration))); });
        }
        timer.Stop();

        UnityMainThreadDispatcher.Instance().Enqueue(() => { text.text = String.Format(format, target); });

        UnityMainThreadDispatcher.Instance().Destroy();
    }

    private static Stopwatch stopWatch;

    public static void StartStopWatch() {
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    public static void StopStopWatch() {
        stopWatch.Stop();
    }

    public static string GetTime() {
        return stopWatch.Elapsed.ToString(@"m\:ss");
    }

    public static void FillUpImage(float start, float target, float duration, UnityEngine.UI.Image image) {
        Stopwatch timer = new Stopwatch();
        timer.Start();

        while (timer.ElapsedMilliseconds < duration) {
            UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = start + 1f * (target - start) * (timer.ElapsedMilliseconds / duration); });
        }

        timer.Stop();

        UnityMainThreadDispatcher.Instance().Enqueue(() => { image.fillAmount = target; });

        UnityMainThreadDispatcher.Instance().Destroy();
    }
}
