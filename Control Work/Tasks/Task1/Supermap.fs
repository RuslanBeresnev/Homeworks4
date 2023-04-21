module Supermap

let supermap mapper list =
    let rec mapping source mapped =
        match source with
        | [] -> mapped
        | h :: t -> mapping t (mapped @ (h |> mapper))
    mapping list []