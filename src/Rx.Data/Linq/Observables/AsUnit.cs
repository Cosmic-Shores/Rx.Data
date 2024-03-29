﻿using System;
using System.Reactive;
using Rx.Extendibility.Observables;

namespace Rx.Data.Linq.Observables {
    internal sealed class AsUnit<T> : ObservableProducer<Unit, AsUnit<T>._> {
        private readonly IObservable<T> _source;

        public AsUnit(IObservable<T> source) => _source = source;

        protected override _ CreateSink(IObserver<Unit> observer) => new _(observer);

        protected override void Run(_ sink) => sink.Run(_source);

        internal sealed class _ : OperatorSink<T, Unit> {
            public _(IObserver<Unit> observer) : base(observer) { }

            public override void OnNext(T value) => ForwardOnNext(Unit.Default);
        }
    }
}
