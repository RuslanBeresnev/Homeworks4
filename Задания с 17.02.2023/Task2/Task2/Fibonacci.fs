open System

printfn "Enter the index of fibonacci sequence member:"
let index = Console.ReadLine() |> int

let fibonacci memberIndex =
    if memberIndex < 0 then -1
    else if memberIndex = 0 then 0
    else if memberIndex = 1 then 1
    else
        let rec calculating firstNumber secondNumber current =
            if current = memberIndex then firstNumber + secondNumber
            else calculating (secondNumber) (firstNumber + secondNumber) (current + 1)
        calculating 1 1 2

let result = fibonacci index
if result = -1 then printfn "Fibonacci sequence index can't be negative"
else printfn "Index №%d of fibonacci sequence is %d" index result