module EvenNumbers

let mapRealization numbers =
    numbers |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum

let filterRealization numbers =
    numbers |> List.filter (fun x -> x % 2 = 0) |> List.length

let foldRealization numbers =
    (0, numbers) ||> List.fold(fun acc x -> if x % 2 = 0 then acc + 1 else acc)