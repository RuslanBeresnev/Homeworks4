open System

let searchNumber list number =
    let rec searching list number currentIndex =
        try 
            if List.item currentIndex list = number then currentIndex
            else searching (list) number (currentIndex + 1)
        with
            | :? System.ArgumentException -> -1
    searching list number 0

let examplaryList = [1; 2; 3; 4; 5]
printfn "Examplary list: %A" examplaryList
printfn "Enter the desired number:"
let number = Console.ReadLine() |> int
let firstOccurance = searchNumber examplaryList number
if firstOccurance = -1 then printfn "List doesn't contains the number %d" number
else printfn "Index of the first occurrence of a number in the list: %d" firstOccurance