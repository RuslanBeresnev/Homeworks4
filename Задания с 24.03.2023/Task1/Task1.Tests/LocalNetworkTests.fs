namespace LocalNetwork.Tests

open LocalNetwork
open NUnit.Framework
open FsUnit
open Foq
open System.Collections.Generic

module Tests =

    // Operating system object
    let mockOS (infectionProbability: float) =
        Mock<IOS>()
            .Setup(fun os -> <@ os.InfectionProbability @>)
            .Returns(infectionProbability)
            .Create()

    /// Computer with 100% chance to be infected
    type WeaklyProtectedComputer() =
        let mutable isInfected = false

        interface IComputer with
            member this.OS = mockOS 1.0
            member this.getInfected() = isInfected <- true
            member this.IsInfected with get() = isInfected

    /// Computer with 0% chance to be infected
    type WellProtectedComputer() =
        let mutable isInfected = false

        interface IComputer with
            member this.OS = mockOS 0.0
            member this.getInfected () = ()
            member this.IsInfected with get() = isInfected

    // Infected computer object
    let mockInfectedComputer () =
        Mock<IComputer>()
            .Setup(fun computer -> <@ computer.IsInfected @>)
            .Returns(true)
            .Setup(fun computer -> <@ computer.OS @>)
            .Returns(mockOS(0.0))
            .Setup(fun computer -> <@ computer.getInfected() @>)
            .Returns(())
            .Create()
    
    // Create the network with connected computers
    let createNetwork (infectedComputer: IComputer) (uninfectedComputers: IComputer list) =

        //                (5)
        //                 |
        //                (2)
        //                 |
        // (4) - (1) - (Infected) - (3) - (6)

        let adjacency = Dictionary<IComputer, IComputer list>()

        adjacency.Add (infectedComputer, [uninfectedComputers.[0]; uninfectedComputers.[1]; uninfectedComputers.[2]])

        adjacency.Add (uninfectedComputers.[0], [uninfectedComputers.[3]; infectedComputer])
        adjacency.Add (uninfectedComputers.[1], [uninfectedComputers.[4]; infectedComputer])
        adjacency.Add (uninfectedComputers.[2], [uninfectedComputers.[5]; infectedComputer])

        adjacency.Add (uninfectedComputers.[3], [uninfectedComputers.[0]])
        adjacency.Add (uninfectedComputers.[4], [uninfectedComputers.[1]])
        adjacency.Add (uninfectedComputers.[5], [uninfectedComputers.[2]])

        let allComputers = infectedComputer :: uninfectedComputers
        Network(allComputers, adjacency)

    [<Test>]
    let ``Should behave as BFS if all computers are weakly protected`` () =

        let infectedComputer = mockInfectedComputer()
        let uninfectedComputers = List.init 6 (fun _ -> WeaklyProtectedComputer() :> IComputer)

        let network = createNetwork infectedComputer uninfectedComputers

        network.IsOver() |> should equal false
        network.Update() |> should equal [true; true; true; true; false; false; false]
        network.IsOver() |> should equal false
        network.Update() |> should equal [true; true; true; true; true; true; true]
        network.IsOver() |> should equal true

    [<Test>]
    let ``Shouldn't change state if all computers are well protected`` () =

        let infectedComputer = mockInfectedComputer ()
        let uninfectedComputers = List.init 6 (fun _ -> WellProtectedComputer() :> IComputer)

        let network = createNetwork infectedComputer uninfectedComputers

        network.IsOver() |> should equal true
        network.Update() |> should equal [true; false; false; false; false; false; false]