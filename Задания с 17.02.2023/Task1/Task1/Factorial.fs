open System

printfn "Enter number to calculate factorial:"
let enteredNumber = Console.ReadLine() |> int

let factorial number =
    if number < 0 then -1
    else
        let rec tailRecursive i accumulator =
            if i <= 1 then accumulator
            else tailRecursive (i - 1) (i * accumulator)
        tailRecursive number 1

let result = factorial enteredNumber
if result = -1 then printfn "Factorial doesn't exist for negative numbers"
else printfn "Factorial of %d is %d" enteredNumber (factorial enteredNumber)