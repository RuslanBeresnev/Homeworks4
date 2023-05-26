module ThreadSafeStack

open System.Threading

/// Thread safe stack structure realization
type ThreadSafeStack<'T>() =
    let mutable stack : 'T list = []
    let lockObj = Some()

    /// Add value to stack
    member this.Push (newItem : 'T) =
        Monitor.Enter lockObj
        stack <- newItem :: stack
        Monitor.Exit lockObj

    /// Try to get top value from stack
    member this.TryPop () =
        lock lockObj (fun () ->
        match stack with
        | head :: tail ->
            stack <- tail
            Some(head)
        | [] -> None
        )