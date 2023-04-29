module MiniCrawler.Tests

open NUnit.Framework
open FsUnit
open System.IO
open MiniCrawler

[<Test>]
let ``FetchAsync should work if url is correct`` () =
    (fetchAsync "https://github.com/RuslanBeresnev" |> Async.RunSynchronously).IsSome |> should be True

[<Test>]
let ``Fetch shouldn't do anything if url is not correct`` () =
    (fetchAsync "Something" |> Async.RunSynchronously).IsNone |> should be True

let expectedLinks = [
    "https://docs.github.com/categories/setting-up-and-managing-your-github-profile";
    "https://github.com/RuslanBeresnev";
    "https://github.com/RuslanBeresnev"
]

[<Test>]
let ``Links finder should work correctly`` () =
    File.ReadAllText("../../../github.txt") |> getAllLinks |> should equal expectedLinks