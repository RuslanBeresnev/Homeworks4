module StringWorkflow

open System

type Calculation() =
    member this.Bind(s : string, f) =
        let result = Int32.TryParse(s)
        match result with
        | false, _ -> None
        | true, value -> f value

    member this.Return(v) = Some(v)