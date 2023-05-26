module ThreadSafeLockLazy

open ILazy

/// Thread safe multi threaded implementation of lazy calculation
type ThreadSafeLockLazy<'a> (supplier : unit -> 'a) =
    [<VolatileField>]
    let mutable result = None
    let lockObject = Some()

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then
                lock lockObject (fun () -> if result.IsNone then result <- Some(supplier()))
            result.Value