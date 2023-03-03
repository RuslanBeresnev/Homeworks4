open System

let reverseList list =
    let rec reversing source reversed =
        if List.isEmpty source then reversed
        else reversing (List.tail source) ((List.head source) :: reversed)
    reversing list []

printfn "Enter list length:"
let length = Console.ReadLine() |> int
     
printfn "Enter elements of the list:"
let originList = List.init length (fun _ -> Console.ReadLine() |> int)

printfn "Reversed list: %A" (reverseList originList)