using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class UnityMainThreadDispatcher : MonoBehaviour
{

    private static readonly Queue<Action> executionQueue = new Queue<Action>();

    public void Update()
    {
        try {
        lock (executionQueue)
        {
            while (executionQueue.Count > 0)
            {
                executionQueue.Dequeue().Invoke();
            }
        } } catch {}
    }

    public void Enqueue(IEnumerator action)
    {
        try {
        lock (executionQueue)
        {
            executionQueue.Enqueue(() =>
            {
                StartCoroutine(action);
            });
        } } catch {}
    }

    public void Enqueue(Action action)
    {
        try {
        Enqueue(ActionWrapper(action));
        } catch {}
    }

    public Task EnqueueAsync(Action action)
    {
        try {
        var tcs = new TaskCompletionSource<bool>();

        void WrappedAction()
        {
            try
            {
                action();
                tcs.TrySetResult(true);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
        }

        Enqueue(ActionWrapper(WrappedAction));
        return tcs.Task;
        } catch { return null; }
    }


    IEnumerator ActionWrapper(Action action)
    {
        action();
        yield return null;
    }


    private static UnityMainThreadDispatcher instance = null;

    public static bool Exists()
    {
        return instance != null;
    }

    public static UnityMainThreadDispatcher Instance()
    {
        if (!Exists())
            throw new Exception("UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object.");
        return instance;
    }


    void Awake()
    {
        try {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } } catch {}
    }

    public void Destroy()
    {
        try {
        Destroy(this.gameObject);
        } catch {}
    }

    void OnDestroy()
    {
        instance = null;
        SaveSystem.SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData();
    }
}