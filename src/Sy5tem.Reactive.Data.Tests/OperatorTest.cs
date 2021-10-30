using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Data.Linq;
using NUnit.Framework;

namespace Sy5tem.Reactive.Data.Tests {
    public class OperatorTest {
        [Test]
        public void AsUnitObservable() {
            CollectionAssert.AreEqual(
                new[] { Unit.Default, Unit.Default, Unit.Default },
                Observable.Range(1, 3).AsUnitObservable().ToArrayWait());
        }

        [Test]
        public void AsSingleUnitObservable() {
            using var subject = new Subject<int>();

            var done = false;
            subject.AsSingleUnitObservable().Subscribe(_ => done = true);

            subject.OnNext(1);
            Assert.False(done);
            subject.OnNext(100);
            Assert.False(done);
            subject.OnCompleted();
            Assert.True(done);
        }

        [Test]
        public void Pairwise() {
            var xs = Observable.Range(1, 5).Pairwise().ToArrayWait();
            Assert.AreEqual(1, xs[0].Previous);
            Assert.AreEqual(2, xs[0].Current);
            Assert.AreEqual(2, xs[1].Previous);
            Assert.AreEqual(3, xs[1].Current);
            Assert.AreEqual(3, xs[2].Previous);
            Assert.AreEqual(4, xs[2].Current);
            Assert.AreEqual(4, xs[3].Previous);
            Assert.AreEqual(5, xs[3].Current);
        }
    }
}
