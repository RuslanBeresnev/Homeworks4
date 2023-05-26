module Lazy.Tests

open FsUnit
open NUnit.Framework
open System.Threading

open ILazy
open SingleThreadedLazy
open ThreadSafeLockLazy
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
let ``Thread-safe lock lazy Get() method returns correct result`` () =
    let lazyObj = ThreadSafeLockLazy<int>(fun () -> 5 * 5) :> ILazy<int>
    lazyObj.Get() |> should equal 25

[<Test>]
let ``Lock-free lazy Get() method returns correct result`` () =
    let lazyObj = LockFreeLazy<int>(fun () -> 5 * 5) :> ILazy<int>
    lazyObj.Get() |> should equal 25

let supplierWasCalledOnce (lazyObject : ILazy<obj>) (manualResetEvent : ManualResetEvent) =
    for i = 0 to 8 do
        manualResetEvent.Reset() |> ignore

        let tasksArray = Seq.init 100 (fun _ -> async { return lazyObject.Get() })

        let resultAsync = tasksArray |> Async.Parallel
        manualResetEvent.Set() |> ignore
        let taskResults = resultAsync |> Async.RunSynchronously

        let goldenObj = Seq.item 0 taskResults

        taskResults
        |> Seq.forall (fun object -> obj.ReferenceEquals(object, goldenObj))
        |> should be True

[<Test>]
let ThreadSafeLockLazyTest () =
    let mre = new ManualResetEvent(false)
    let counter = ref 0

    let supplier () =
        mre.WaitOne() |> ignore
        Interlocked.Increment counter |> ignore
        obj ()

    let lazyObject = ThreadSafeLockLazy<obj>(supplier) :> ILazy<obj>
    supplierWasCalledOnce lazyObject mre

    counter.Value |> should equal 1

[<Test>]
let LockFreeLazyTest () =
    let mre = new ManualResetEvent(false)
    let counter = ref 0

    let supplier () =
        mre.WaitOne() |> ignore
        Interlocked.Increment counter |> ignore
        obj ()

    let lazyObject = LockFreeLazy<obj>(supplier) :> ILazy<obj>
    supplierWasCalledOnce lazyObject mre