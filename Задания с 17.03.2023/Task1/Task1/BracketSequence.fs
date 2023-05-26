module BracketSequence

let checkingBracketSequenceForCorrectness (sequence : List<char>) =
    let bracketTypes = [ ('(', ')'); ('{', '}'); ('[', ']') ]
    let openingBrackets = bracketTypes |> List.map fst

    let isPair openingBracket closingBracket =
        let rec recursiveBypass currentBracketsList index =
            match currentBracketsList with
            | h :: t ->
                if fst h = openingBracket then snd h = closingBracket
                else recursiveBypass t (index + 1)
            | _ -> false
        recursiveBypass bracketTypes 0

    let rec checkElementOfSequence (currentSequence: List<char>) (stack: List<char>) =
        match currentSequence with
        | seqHead :: seqTail ->
             if openingBrackets |> List.contains seqHead then checkElementOfSequence seqTail (seqHead :: stack)
             else
                match stack with
                | stackHead :: stackTail ->
                    if isPair stackHead seqHead then checkElementOfSequence seqTail stackTail
                    else false
                | _ -> false
        | _ ->
            match stack with
            | h :: t -> false
            | _ -> true

    checkElementOfSequence sequence []