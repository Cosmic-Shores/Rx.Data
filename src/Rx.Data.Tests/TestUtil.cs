using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Rx.Data.Tests {
    internal static class TestUtil {
        public static T[] ToArrayWait<T>(this IObservable<T> source) {
            return source.ToArray().Wait();
        }

        public static RecordObserver<T> Record<T>(this IObservable<T> source) {
            var d = new SingleAssignmentDisposable();
            var observer = new RecordObserver<T>(d);
            d.Disposable = source.Subscribe(observer);

            return observer;
        }
    }
}
