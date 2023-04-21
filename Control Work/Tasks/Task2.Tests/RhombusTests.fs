module Rhombus.Tests

open NUnit.Framework
open FsUnit
open Rhombus

[<Test>]
let ``Correct rhombus building in case of length of sides equals 1`` () =
    getRhombusByRowsOfSymbols 1 |> should equal ["*"]

[<Test>]
let ``Correct rhombus building in case of length of sides equals 2`` () =
    getRhombusByRowsOfSymbols 2 |> should equal [" * "; "***"]

[<Test>]
let ``Correct rhombus building in case of length of sides equals 4`` () =
    getRhombusByRowsOfSymbols 4 |> should equal ["   *   "; "  ***  "; " ***** "; "*******"]