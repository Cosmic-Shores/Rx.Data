// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Reactive.Extendibility.Observables {
    internal sealed class NopObserver<T> : IObserver<T> {
        public static readonly IObserver<T> Instance = new NopObserver<T>();

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(T value) { }
    }
}
