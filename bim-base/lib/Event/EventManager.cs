using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventManager
{
    private readonly Dictionary<EventSubject, List<Action<EventMessage>>> _handlers
        = new Dictionary<EventSubject, List<Action<EventMessage>>>();

    private readonly object _lock = new object();

    public static readonly EventManager Instance = new EventManager();

    private EventManager() { }

    // =========================
    // Subscribe
    // =========================
    public void Subscribe(EventSubject subject, Action<EventMessage> handler)
    {
        if (handler == null)
            return;

        lock (_lock)
        {
            if (!_handlers.ContainsKey(subject))
                _handlers[subject] = new List<Action<EventMessage>>();

            _handlers[subject].Add(handler);
        }
    }

    public void Unsubscribe(EventSubject subject, Action<EventMessage> handler)
    {
        if (handler == null)
            return;

        lock (_lock)
        {
            if (!_handlers.ContainsKey(subject))
                return;

            _handlers[subject].Remove(handler);

            if (_handlers[subject].Count == 0)
                _handlers.Remove(subject);
        }
    }

    // =========================
    // Sync Publish (기본)
    // =========================
    public void Publish(EventMessage evt)
    {
        if (evt == null) return;

        List<Action<EventMessage>> handlers;

        lock (_lock)
        {
            if (!_handlers.ContainsKey(evt.Subject))
                return;

            handlers = new List<Action<EventMessage>>(_handlers[evt.Subject]);
        }

        foreach (var handler in handlers)
        {
            try
            {
                handler(evt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    // =========================
    // Async Parallel
    // =========================
    public async Task PublishAsyncParallel(EventMessage evt)
    {
        if (evt == null) return;

        List<Action<EventMessage>> handlers;

        lock (_lock)
        {
            if (!_handlers.ContainsKey(evt.Subject))
                return;

            handlers = new List<Action<EventMessage>>(_handlers[evt.Subject]);
        }

        var tasks = new List<Task>();

        foreach (var handler in handlers)
        {
            var h = handler;
            tasks.Add(Task.Run(() =>
            {
                try { h(evt); }
                catch (Exception ex) { Console.WriteLine(ex); }
            }));
        }

        await Task.WhenAll(tasks);
    }

    // =========================
    // Async Sequential
    // =========================
    public async Task PublishAsyncSequential(EventMessage evt)
    {
        if (evt == null) return;

        List<Action<EventMessage>> handlers;

        lock (_lock)
        {
            if (!_handlers.ContainsKey(evt.Subject))
                return;

            handlers = new List<Action<EventMessage>>(_handlers[evt.Subject]);
        }

        foreach (var handler in handlers)
        {
            var h = handler;

            try
            {
                await Task.Run(() => h(evt));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}