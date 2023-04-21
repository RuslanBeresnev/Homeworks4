module StringWorkflow.Tests

open NUnit.Framework
open FsUnit
open StringWorkflow

[<Test>]
let ``Correct data case test`` () =
    let flow = Calculation() {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    }
    flow |> should equal (Some(3))

[<Test>]
let ``Incorrect data case test`` () =
    let flow = Calculation() {
        let! x = "1"
        let! y = "Ú"
        let z = x + y
        return z
    }
    flow |> should equal None