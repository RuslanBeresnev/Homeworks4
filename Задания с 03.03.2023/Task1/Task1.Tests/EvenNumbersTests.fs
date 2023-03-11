module EvenNumbers.Tests

open NUnit.Framework
open EvenNumbers

let implementationsAreEquivalent (numbers:List<int>) =
    mapRealization numbers = filterRealization numbers && filterRealization numbers = foldRealization numbers

FsCheck.Check.Quick implementationsAreEquivalent

[<Test>]
let emptyListCaseTest () =
    Assert.True(mapRealization [] = 0)

[<Test>]
let defaultListCaseTest () =
    Assert.True(mapRealization [1..10] = 5)

[<Test>]
let listOnlyWithOddNumbersTest () =
    Assert.True(mapRealization [3; 5; 7; 9; 11] = 0)