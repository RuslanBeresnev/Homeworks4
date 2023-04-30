module LambdaInterpreter.Tests

open NUnit.Framework
open FsUnit
open System
open LambdaInterpreter

[<Test>]
let ``getFreeVariables should work correctly`` () =
    let x = Guid.NewGuid()
    let y = Guid.NewGuid()
    let z = Guid.NewGuid()

    Application(Variable x, Abstraction(x, Application(Variable y, Variable z))) |> getFreeVariables |> should equal (set [x; y; z])

[<Test>]
let ``getNewValueNotFromTheSet should work correctly`` () =
    let x = Guid.NewGuid()
    let y = Guid.NewGuid()
    set [x; y] |> Set.contains (set [x; y] |> getNewValueNotFromTheSet) |> not |> should equal true

[<Test>]
let ``Substitute without name collisions should work correctly`` () =
    let x = Guid.NewGuid()
    let y = Guid.NewGuid()
    let z = Guid.NewGuid()

    substitute (Variable x) y (Application(Variable y, Variable z)) |> should equal (Variable x)
    substitute (Application(Variable x, Variable y)) y (Variable z) |> should equal (Application(Variable x, Variable z))
    substitute (Abstraction(x, Variable x)) x (Application(Variable y, Variable z)) |> should equal  (Abstraction(x, Variable x))
    substitute (Abstraction(y, Variable x)) x (Variable z) |> should equal (Abstraction(y, Variable z))

[<Test>]
let ``Substitute with name collisions should work correctly`` () =
    let x = Guid.NewGuid()
    let y = Guid.NewGuid()
    let result = substitute (Abstraction(y, Variable x)) x (Variable y)
    match result with
    | Abstraction (_, term) -> 
        match term with
        | Variable name -> name |> should equal y
        | _ -> Assert.Fail()
    | _ -> Assert.Fail()

[<Test>]
let ``Reduce should work correctly`` () =
     let x = Guid.NewGuid()
     let y = Guid.NewGuid()

     let I x = Abstraction(x, Variable x)
     let K x y = Abstraction(x, Abstraction(y, Variable x))
     let K_Star x y = Abstraction(x, Abstraction(y, Variable y))

     Application(I x, I x) |> reduce |> should equal (I x)
     Application(K x y, I x) |> reduce |> should equal (K_Star y x)
     Application(K_Star x y, Application(I x, I x)) |> reduce |> should equal (I y)
     Application(I x, K x y) |> reduce |> should equal (K x y)
     Application(I x, Application(I x, I x)) |> reduce |> should equal (I x)