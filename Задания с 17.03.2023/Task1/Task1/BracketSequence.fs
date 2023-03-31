module BracketSequence

let checkingBracketSequenceForCorrectness (sequence : List<char>) =
    let isPair openBracket closeBracket = 
        match openBracket with
        | '(' -> closeBracket = ')'
        | '{' -> closeBracket = '}'
        | '[' -> closeBracket = ']'
        | _ -> false

    let rec checkElementOfSequence (currentSequence: List<char>) (stack: List<char>) =
        if currentSequence.IsEmpty && stack.IsEmpty then true
        elif currentSequence.IsEmpty && stack <> [] then false            
        else
            let currentValue = List.head currentSequence
            if currentValue = '(' || currentValue = '{' || currentValue = '[' then
                checkElementOfSequence (List.tail currentSequence) (currentValue :: stack)
            else if (stack.IsEmpty) then false
            else if (isPair (List.head stack) currentValue) then 
                checkElementOfSequence (List.tail currentSequence) (List.tail stack)
            else false
    checkElementOfSequence sequence []