﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Rx.Data {
    public class ReactiveCollection<T> : Collection<T>, IReactiveCollection<T>, IDisposable
    {
        private bool _isDisposed = false;

        public ReactiveCollection() {}

        public ReactiveCollection(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public ReactiveCollection(List<T> list)
            : base(list != null ? new List<T>(list) : null)
        {
        }

        protected override void ClearItems()
        {
            var beforeCount = Count;
            base.ClearItems();

            if (collectionReset != null) collectionReset.OnNext(Unit.Default);
            if (beforeCount > 0)
            {
                if (countChanged != null) countChanged.OnNext(Count);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            if (collectionAdd != null) collectionAdd.OnNext(new CollectionAddEvent<T>(index, item));
            if (countChanged != null) countChanged.OnNext(Count);
        }

        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }

        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            T item = this[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, item);

            if (collectionMove != null) collectionMove.OnNext(new CollectionMoveEvent<T>(oldIndex, newIndex, item));
        }

        protected override void RemoveItem(int index)
        {
            T item = this[index];
            base.RemoveItem(index);

            if (collectionRemove != null) collectionRemove.OnNext(new CollectionRemoveEvent<T>(index, item));
            if (countChanged != null) countChanged.OnNext(Count);
        }

        protected override void SetItem(int index, T item)
        {
            T oldItem = this[index];
            base.SetItem(index, item);

            if (collectionReplace != null) collectionReplace.OnNext(new CollectionReplaceEvent<T>(index, oldItem, item));
        }

        Subject<int> countChanged = null;
        public IObservable<int> ObserveCountChanged(bool notifyCurrentCount = false)
        {
            if (_isDisposed) return Observable.Empty<int>();

            var subject = countChanged ?? (countChanged = new Subject<int>());
            if (notifyCurrentCount) {
                return Observable.Create<int>(observer => subject.StartWith(this.Count).Subscribe(observer));
            }
            else
            {
                return subject;
            }
        }

        Subject<Unit> collectionReset = null;
        public IObservable<Unit> ObserveReset()
        {
            if (_isDisposed) return Observable.Empty<Unit>();
            return collectionReset ?? (collectionReset = new Subject<Unit>());
        }

        Subject<CollectionAddEvent<T>> collectionAdd = null;
        public IObservable<CollectionAddEvent<T>> ObserveAdd()
        {
            if (_isDisposed) return Observable.Empty<CollectionAddEvent<T>>();
            return collectionAdd ?? (collectionAdd = new Subject<CollectionAddEvent<T>>());
        }

        Subject<CollectionMoveEvent<T>> collectionMove = null;
        public IObservable<CollectionMoveEvent<T>> ObserveMove()
        {
            if (_isDisposed) return Observable.Empty<CollectionMoveEvent<T>>();
            return collectionMove ?? (collectionMove = new Subject<CollectionMoveEvent<T>>());
        }

        Subject<CollectionRemoveEvent<T>> collectionRemove = null;
        public IObservable<CollectionRemoveEvent<T>> ObserveRemove()
        {
            if (_isDisposed) return Observable.Empty<CollectionRemoveEvent<T>>();
            return collectionRemove ?? (collectionRemove = new Subject<CollectionRemoveEvent<T>>());
        }

        Subject<CollectionReplaceEvent<T>> collectionReplace = null;
        public IObservable<CollectionReplaceEvent<T>> ObserveReplace()
        {
            if (_isDisposed) return Observable.Empty<CollectionReplaceEvent<T>>();
            return collectionReplace ?? (collectionReplace = new Subject<CollectionReplaceEvent<T>>());
        }

        void DisposeSubject<TSubject>(ref Subject<TSubject> subject)
        {
            if (subject != null)
            {
                try
                {
                    subject.OnCompleted();
                }
                finally
                {
                    subject.Dispose();
                    subject = null;
                }
            }
        }

        #region IDisposable Support

        public void Dispose() {
            if (_isDisposed)
                return;
            _isDisposed = true;
            DisposeSubject(ref collectionReset);
            DisposeSubject(ref collectionAdd);
            DisposeSubject(ref collectionMove);
            DisposeSubject(ref collectionRemove);
            DisposeSubject(ref collectionReplace);
            DisposeSubject(ref countChanged);
        }
        
        #endregion
    }
}
