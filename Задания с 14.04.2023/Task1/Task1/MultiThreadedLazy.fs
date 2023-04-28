module MultiThreadedLazy

open ILazy

/// Thread safe multi threaded implementation of lazy calculation
type MultiThreadedLazy<'a> (supplier : unit -> 'a) =
    let mutable result = None
    let lockObject = Some()

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then lock lockObject (fun () -> result <- Some(supplier()))
            result.Value