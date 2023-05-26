open System

let searchNumber list number =
    let rec searching currentList index =
        match currentList with
        | head :: tail -> 
            if head = number then index else searching tail (index + 1)
        | _ -> -1
    searching list 0

let examplaryList = [1; 2; 3; 4; 5]
printfn "Examplary list: %A" examplaryList
printfn "Enter the desired number:"
let number = Console.ReadLine() |> int
let firstOccurance = searchNumber examplaryList number
if firstOccurance = -1 then printfn "List doesn't contains the number %d" number
else printfn "Index of the first occurrence of a number in the list: %d" firstOccurance