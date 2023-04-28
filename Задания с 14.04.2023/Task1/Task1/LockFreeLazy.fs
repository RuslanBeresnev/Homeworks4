module LockFreeLazy

open System.Threading
open ILazy

/// Thread safe lock-free implementation of lazy calculation
type LockFreeLazy<'a when 'a : equality> (supplier : unit -> 'a) =
    let mutable result = None

    interface ILazy<'a> with
        member this.Get() =
            let mutable currentValue = result
            let mutable startValue = None
            let mutable desiredValue = None
            let mutable isDone = false

            if result.IsNone then
                while not isDone do
                    startValue <- currentValue
                    desiredValue <- Some(supplier())
                    currentValue <- Interlocked.CompareExchange(&result, desiredValue, startValue)
                    if currentValue = startValue then isDone <- true
            result.Value