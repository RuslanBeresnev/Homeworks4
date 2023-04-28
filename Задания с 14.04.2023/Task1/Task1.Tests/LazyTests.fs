module Lazy.Tests

open FsUnit
open NUnit.Framework
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks

open ILazy
open SingleThreadedLazy
open MultiThreadedLazy
open LockFreeLazy

[<Test>]
let ``Single threaded lazy Get() method returns correct result`` () =
    let lazyObj = SingleThreadedLazy<int>(fun () -> 5 * 5) :> ILazy<int>
    lazyObj.Get() |> should equal 25

[<Test>]
let ``Single threaded lazy Get() method calculates result once`` () =
    let counter = ref 0
    let lazyObj = SingleThreadedLazy<unit>(fun () -> counter.Value <- counter.Value + 1) :> ILazy<unit>
    lazyObj.Get()
    lazyObj.Get()
    counter.Value |> should equal 1

[<Test>]
let ``Multi threaded lazy Get() method returns correct result`` () =
    let lazyObj = MultiThreadedLazy<int>(fun () -> 5 * 5) :> ILazy<int>
    lazyObj.Get() |> should equal 25

[<Test>]
let ``Multi threaded lazy Get() method calculates result once`` () =
    let counter = ref 0
    let lazyObj = MultiThreadedLazy<unit>(fun () -> counter.Value <- Interlocked.Increment counter) :> ILazy<unit>
    Parallel.For(0, 8, (fun obj -> lazyObj.Get())) |> ignore
    counter.Value |> should equal 1

[<Test>]
let ``Lock-free lazy Get() method returns correct result`` () =
    let lazyObj = LockFreeLazy<int>(fun () -> 5 * 5) :> ILazy<int>
    lazyObj.Get() |> should equal 25

[<Test>]
let ``Lock-free lazy Get() method returns the same object`` () =
    let lazyObj = LockFreeLazy<List<int>>(fun () -> new List<int>()) :> ILazy<List<int>>
    let list = lazyObj.Get()
    Parallel.For(0, 8, (fun obj -> lazyObj.Get() |> should equal list)) |> ignore