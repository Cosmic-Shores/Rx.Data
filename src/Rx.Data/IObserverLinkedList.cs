namespace Rx.Data {
    internal interface IObserverLinkedList<T>
    {
        void UnsubscribeNode(ObserverNode<T> node);
    }
}
