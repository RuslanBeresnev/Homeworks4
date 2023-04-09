namespace LocalNetwork

open System
open System.Collections.Generic

/// The local network where computers infect each other every frame
type Network (computers: IComputer list, adjacency: IDictionary<IComputer, IComputer list>) =
    // Random for infecting
    let rnd = Random()

    // True for an infected computer that has uninfected neighbors
    let capableOfInfecting (c: IComputer) =
        c.IsInfected && adjacency.[c] |> List.exists (fun n -> not n.IsInfected && n.OS.InfectionProbability > 0.0)

    /// To update the state of the network
    member this.Update () =
        computers |> List.filter capableOfInfecting
                  |> List.map (fun c -> adjacency.[c])
                  |> List.concat
                  |> List.iter (fun c -> if rnd.NextDouble() < c.OS.InfectionProbability then c.getInfected())

        computers |> List.map (fun c -> c.IsInfected)

    /// Checks whether the network reached final state
    member this.IsOver () =
        not (computers |> List.exists capableOfInfecting)