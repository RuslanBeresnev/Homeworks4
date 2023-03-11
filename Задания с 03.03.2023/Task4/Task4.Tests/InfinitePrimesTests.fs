module InfinitePrimes.Tests

open NUnit.Framework
open InfinitePrimes

let rec compareLists l1 l2 = 
    match l1, l2 with 
    | [], [] -> true
    | l1h :: l1t, l2h :: l2t -> l1h = l2h && compareLists l1t l2t
    | _ -> false

[<Test>]
let firstTwentyPrimesAreCorrectTest () =
    let receivedTwentyPrimes = allPrimes() |> Seq.take 20 |> List.ofSeq
    let correctTwentyPrimes = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71]
    Assert.True(compareLists receivedTwentyPrimes correctTwentyPrimes)