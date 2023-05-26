open System

let generatePowerRow n m =
    let rec generating list currentNumber iter =
        if iter = m + 1 then list
        else generating (currentNumber :: list) (currentNumber / 2) (iter + 1)
    generating [] ((2.0 ** float (n + m)) |> int) 0

printfn "Enter n:"
let n = Console.ReadLine() |> int
printfn "Enter m:"
let m = Console.ReadLine() |> int

printfn "Generated row of powers of 2 between powers %d and %d: %A" (n) (m + n) (generatePowerRow n m)