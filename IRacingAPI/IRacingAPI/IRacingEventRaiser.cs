﻿using IRacingAPI.Models.Enumerations;

namespace IRacingAPI;
internal class IRacingEventRaiser
{
    public EventRaiseTypes EventRaiseType { get; set; }

    private readonly SynchronizationContext _context;

    public IRacingEventRaiser()
    {
        _context = SynchronizationContext.Current ?? new();
    }

    internal void RaiseEvent<T>(Action<T> del, T e)
    where T : EventArgs
    {
        SendOrPostCallback callback = new(obj =>
        {
            if (obj is T eventArg)
            {
                del(eventArg);
            }
        });

        if (_context != null && _context == SynchronizationContext.Current && EventRaiseType == EventRaiseTypes.CurrentThread)
        {
            // Post the event method on the thread context, this raises the event on the thread on which the SdkWrapper object was created
            _context.Post(callback, e);
        }
        else
        {
            // Simply invoke the method, this raises the event on the background thread that the SdkWrapper created
            // Care must be taken by the user to avoid cross-thread operations
            callback.Invoke(e);
        }
    }
}
