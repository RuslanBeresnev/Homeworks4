module RoundingWorkflow

open System

type Round(accuracy: int) =
    member this.Bind (n : float, f) = f (Math.Round(n, accuracy))
    member this.Return (n : float) = Math.Round(n, accuracy)