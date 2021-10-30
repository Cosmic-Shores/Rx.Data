using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;

namespace Rx.Data.Tests {
    public class ReactivePropertyTest {
        [Test]
        public void ValueType() {
            {
                var rp = new ReactiveProperty<int>(); // 0

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                rp.Value = 0;
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                rp.Value = 10;
                CollectionAssert.AreEqual(new[] { 0, 10 }, result.Values);

                rp.Value = 100;
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);

                rp.Value = 100;
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);
            }
            {
                var rp = new ReactiveProperty<int>(20);

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { 20 }, result.Values);

                rp.Value = 0;
                CollectionAssert.AreEqual(new[] { 20, 0 }, result.Values);

                rp.Value = 10;
                CollectionAssert.AreEqual(new[] { 20, 0, 10 }, result.Values);

                rp.Value = 100;
                CollectionAssert.AreEqual(new[] { 20, 0, 10, 100 }, result.Values);

                rp.Value = 100;
                CollectionAssert.AreEqual(new[] { 20, 0, 10, 100 }, result.Values);
            }
        }

        [Test]
        public void ClassType() {
            {
                var rp = new ReactiveProperty<string>(); // null

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { default(string) }, result.Values);

                rp.Value = null;
                CollectionAssert.AreEqual(new[] { default(string) }, result.Values);

                rp.Value = "a";
                CollectionAssert.AreEqual(new[] { default(string), "a" }, result.Values);

                rp.Value = "b";
                CollectionAssert.AreEqual(new[] { default(string), "a", "b" }, result.Values);

                rp.Value = "b";
                CollectionAssert.AreEqual(new[] { default(string), "a", "b" }, result.Values);
            }
            {
                var rp = new ReactiveProperty<string>("z");

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { "z" }, result.Values);

                rp.Value = "z";
                CollectionAssert.AreEqual(new[] { "z" }, result.Values);

                rp.Value = "a";
                CollectionAssert.AreEqual(new[] { "z", "a" }, result.Values);

                rp.Value = "b";
                CollectionAssert.AreEqual(new[] { "z", "a", "b" }, result.Values);

                rp.Value = "b";
                CollectionAssert.AreEqual(new[] { "z", "a", "b" }, result.Values);

                rp.Value = null;
                CollectionAssert.AreEqual(new[] { "z", "a", "b", null }, result.Values);
            }
        }

        [Test]
        public void ToReadOnlyReactivePropertyValueType() {
            {
                var source = new Subject<int>();
                var rp = source.ToReadOnlyReactiveProperty();

                var result = rp.Record();
                Assert.AreEqual(0, result.Values.Count);

                source.OnNext(0);
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                source.OnNext(10);
                CollectionAssert.AreEqual(new[] { 0, 10 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);
            }
            {
                var source = new Subject<int>();
                var rp = source.ToSequentialReadOnlyReactiveProperty();

                var result = rp.Record();
                Assert.AreEqual(0, result.Values.Count);

                source.OnNext(0);
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                source.OnNext(10);
                CollectionAssert.AreEqual(new[] { 0, 10 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 10, 100, 100 }, result.Values);
            }
            {
                var source = new Subject<int>();
                var rp = source.ToReadOnlyReactiveProperty(0);

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                source.OnNext(0);
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                source.OnNext(10);
                CollectionAssert.AreEqual(new[] { 0, 10 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 10, 100 }, result.Values);
            }
            {
                var source = new Subject<int>();
                var rp = source.ToSequentialReadOnlyReactiveProperty(0);

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { 0 }, result.Values);

                source.OnNext(0);
                CollectionAssert.AreEqual(new[] { 0, 0 }, result.Values);

                source.OnNext(10);
                CollectionAssert.AreEqual(new[] { 0, 0, 10 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 0, 10, 100 }, result.Values);

                source.OnNext(100);
                CollectionAssert.AreEqual(new[] { 0, 0, 10, 100, 100 }, result.Values);
            }
        }

        [Test]
        public void ToReadOnlyReactivePropertyClassType() {
            {
                var source = new Subject<string>();
                var rp = source.ToReadOnlyReactiveProperty();

                var result = rp.Record();
                Assert.AreEqual(0, result.Values.Count);

                source.OnNext(null);
                CollectionAssert.AreEqual(new[] { default(string) }, result.Values);

                source.OnNext("a");
                CollectionAssert.AreEqual(new[] { null, "a" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { null, "a", "b" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { null, "a", "b" }, result.Values);
            }
            {
                var source = new Subject<string>();
                var rp = source.ToSequentialReadOnlyReactiveProperty();

                var result = rp.Record();
                Assert.AreEqual(0, result.Values.Count);

                source.OnNext(null);
                CollectionAssert.AreEqual(new[] { default(string) }, result.Values);

                source.OnNext("a");
                CollectionAssert.AreEqual(new[] { null, "a" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { null, "a", "b" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { null, "a", "b", "b" }, result.Values);
            }
            {
                var source = new Subject<string>();
                var rp = source.ToReadOnlyReactiveProperty("z");

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { "z" }, result.Values);

                source.OnNext("z");
                CollectionAssert.AreEqual(new[] { "z" }, result.Values);

                source.OnNext("a");
                CollectionAssert.AreEqual(new[] { "z", "a" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { "z", "a", "b" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { "z", "a", "b" }, result.Values);

                source.OnNext(null);
                CollectionAssert.AreEqual(new[] { "z", "a", "b", null }, result.Values);

                source.OnNext(null);
                CollectionAssert.AreEqual(new[] { "z", "a", "b", null }, result.Values);
            }
            {
                var source = new Subject<string>();
                var rp = source.ToSequentialReadOnlyReactiveProperty("z");

                var result = rp.Record();
                CollectionAssert.AreEqual(new[] { "z" }, result.Values);

                source.OnNext("z");
                CollectionAssert.AreEqual(new[] { "z", "z" }, result.Values);

                source.OnNext("a");
                CollectionAssert.AreEqual(new[] { "z", "z", "a" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { "z", "z", "a", "b" }, result.Values);

                source.OnNext("b");
                CollectionAssert.AreEqual(new[] { "z", "z", "a", "b", "b" }, result.Values);

                source.OnNext(null);
                CollectionAssert.AreEqual(new[] { "z", "z", "a", "b", "b", null }, result.Values);

                source.OnNext(null);
                CollectionAssert.AreEqual(new[] { "z", "z", "a", "b", "b", null, null }, result.Values);
            }
        }

        [Test]
        public void FinishedSourceToReadOnlyReactiveProperty() {
            // pattern of OnCompleted
            {
                var source = Observable.Return(9);
                var rxProp = source.ToReadOnlyReactiveProperty();

                var notifications = rxProp.Record().Notifications;
                CollectionAssert.AreEqual(new[] { Notification.CreateOnNext(9), Notification.CreateOnCompleted<int>() }, notifications);

                CollectionAssert.AreEqual(new[] { Notification.CreateOnNext(9), Notification.CreateOnCompleted<int>() }, rxProp.Record().Notifications);
            }

            // pattern of OnError
            {
                // after
                {
                    var ex = new Exception();
                    var source = Observable.Throw<int>(ex);
                    var rxProp = source.ToReadOnlyReactiveProperty();

                    var notifications = rxProp.Record().Notifications;
                    CollectionAssert.AreEqual(new[] { Notification.CreateOnError<int>(ex) }, notifications);

                    CollectionAssert.AreEqual(new[] { Notification.CreateOnError<int>(ex) }, rxProp.Record().Notifications);
                }
                // immediate
                {

                    // var ex = new Exception();
                    var source = new Subject<int>();
                    var rxProp = source.ToReadOnlyReactiveProperty();

                    var record = rxProp.Record();

                    source.OnError(new Exception());

                    var notifications = record.Notifications;
                    Assert.AreEqual(1, notifications.Count);
                    Assert.AreEqual(NotificationKind.OnError, notifications[0].Kind);

                    Assert.AreEqual(NotificationKind.OnError, rxProp.Record().Notifications[0].Kind);
                }
            }
        }
    }
}
