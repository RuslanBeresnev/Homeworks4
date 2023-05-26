module InfinitePrimes

let isPrime n =
    let nSqrt = (float >> sqrt >> int) n
    [ 2 .. nSqrt ] |> List.forall (fun x -> n % x <> 0)

let allPrimes() =
    let rec allPrimesCalculation n =
        seq {
        if isPrime n then yield n
        yield! allPrimesCalculation (n + 1)
        }
    allPrimesCalculation 2