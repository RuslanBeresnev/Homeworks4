module LockFreeLazy

open System.Threading
open ILazy

/// Thread safe lock-free implementation of lazy calculation
type LockFreeLazy<'a> (supplier : unit -> 'a) =
    let mutable result = None

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then
                let computedValue = Some(supplier())
                Interlocked.CompareExchange(&result, computedValue, None) |> ignore
            result.Value