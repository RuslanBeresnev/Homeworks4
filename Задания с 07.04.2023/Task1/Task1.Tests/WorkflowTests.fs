module RoundingWorkflow.Tests

open NUnit.Framework
open FsUnit
open RoundingWorkflow

[<Test>]
let ``Test with three symbols after comma`` () =
    let flow = Round 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    }
    flow |> should equal 0.048

[<Test>]
let ``Test with five symbols after comma`` () =
    let flow = Round 6 {
        let! a = 3.0 * 3.11225
        let! b = 10.001
        return a / b
    }
    flow |> should equal 0.933582