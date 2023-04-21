module Supermap.Tests

open NUnit.Framework
open FsUnit
open Supermap

[<Test>]
let ``Test with numbers`` () =
    [1; 2; 3] |> supermap (fun x -> [x; x * 10]) |> should equal [1; 10; 2; 20; 3; 30]

[<Test>]
let ``Test with strings`` () =
    ["a"; "b"; "c"] |> supermap (fun str -> [str; str + str]) |> should equal ["a"; "aa"; "b"; "bb"; "c"; "cc"]