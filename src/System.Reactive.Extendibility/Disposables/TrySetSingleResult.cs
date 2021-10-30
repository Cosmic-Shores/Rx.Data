// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Reactive.Extendibility.Disposables {
    internal enum TrySetSingleResult {
        Success,
        AlreadyAssigned,
        Disposed
    }
}
