// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Reactive.Extendibility.Observables {
    public abstract class ObservableProducer<TTarget, TSink> : ObservableBase<TTarget> where TSink : IDisposable {
        protected sealed override IDisposable SubscribeCore(IObserver<TTarget> observer) {
            var sink = CreateSink(observer);
            var didError = true;
            try {
                Run(sink);
                didError = false;
                return sink;
            }
            finally {
                if (didError)
                    sink.Dispose();
            }
        }

        protected abstract TSink CreateSink(IObserver<TTarget> observer);
        protected abstract void Run(TSink sink);
    }
}
