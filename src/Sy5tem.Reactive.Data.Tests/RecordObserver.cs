using System;
using System.Collections.Generic;
using System.Reactive;

namespace Sy5tem.Reactive.Data.Tests {
    internal class RecordObserver<T> : IObserver<T> {
        readonly object _gate = new object();
        readonly IDisposable _subscription;

        public List<T> Values { get; set; }
        public List<Notification<T>> Notifications { get; set; }

        public RecordObserver(IDisposable subscription) {
            _subscription = subscription;
            Values = new List<T>();
            Notifications = new List<Notification<T>>();
        }

        public void DisposeSubscription() {
            _subscription.Dispose();
        }

        void IObserver<T>.OnNext(T value) {
            lock (_gate) {
                Values.Add(value);
                Notifications.Add(Notification.CreateOnNext<T>(value));
            }
        }

        void IObserver<T>.OnError(Exception error) {
            lock (_gate) {
                Notifications.Add(Notification.CreateOnError<T>(error));
            }
        }
        void IObserver<T>.OnCompleted() {
            lock (_gate) {
                Notifications.Add(Notification.CreateOnCompleted<T>());
            }
        }
    }
}
