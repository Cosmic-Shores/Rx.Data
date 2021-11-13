# Rx.Data
Useful types and extensions for mixing reactive and non reactive code, build on rx.net.

Mostly made for use in [Rx.Unity](https://github.com/Cosmic-Shores/Rx.Unity).
This library contains the non unity specific parts to replace UniRx by building extensions on top of [System.Reactive](https://github.com/dotnet/reactive).

[![CI Publish](https://github.com/Cosmic-Shores/Rx.Data/actions/workflows/publish.yml/badge.svg)](https://github.com/Cosmic-Shores/Rx.Data/actions/workflows/publish.yml)
[![NuGet version](https://badgen.net/nuget/v/Rx.Data/latest)](https://nuget.org/packages/Rx.Data)
[![License](https://badgen.net/github/license/Naereen/Strapdown.js)](https://github.com/Cosmic-Shores/Rx.Data/blob/main/LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

### Provided classes:
- ReactiveProperty
- ReactiveCollection
- ReactiveDictionary
(each with their readonly counterparts)

### Additional Static Methods:
- DataObservable.ReturnUnit()

### Additional Observable\<T\> extensions:
- .AsUnitObservable()
- .AsSingleUnitObservable()
- .Pairwise() ([may be moved at some point](https://github.com/dotnet/reactive/issues/1629))
- .Share()
- .Concat(params IObservable\<TSource\>[] seconds) (just a fancier overload)
- .Merge(params IObservable\<T\>[] seconds) (just a fancier overload)

## Rx.Extendibility [![NuGet version](https://badgen.net/nuget/v/Rx.Extendibility/latest)](https://nuget.org/packages/Rx.Extendibility)
A helper library for extending [System.Reactive](https://github.com/dotnet/reactive) using 3rd party libraries more easily.
