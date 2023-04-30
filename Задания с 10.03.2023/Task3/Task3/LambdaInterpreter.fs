module LambdaInterpreter

open System

/// Implementation of a lambda term
type Term<'a> =
    | Variable of 'a
    | Abstraction of 'a * Term<'a>
    | Application of Term<'a> * Term<'a>

/// Returns set of free variables in the term
let rec getFreeVariables term =
    let rec getFreeVariablesRecursive term acc =
        match term with
        | Variable name -> acc |> Set.add name
        | Application(left, right) -> getFreeVariablesRecursive left acc + getFreeVariablesRecursive right acc
        | Abstraction(variable, innerTerm) -> getFreeVariablesRecursive innerTerm acc - set[variable]
    getFreeVariablesRecursive term Set.empty

/// Generates a new value which is not contained in the set
let rec getNewValueNotFromTheSet set =
    let newValue = Guid.NewGuid()
    if set |> Set.contains newValue then getNewValueNotFromTheSet set else newValue

/// Substitutes a new term into a free variable
let rec substitute term variableToChange newTerm =
    match term with
    | Variable name when name = variableToChange -> newTerm
    | Variable _ -> term
    | Application(left, right) -> Application(substitute left variableToChange newTerm, substitute right variableToChange newTerm)
    | Abstraction(variable, innerTerm) ->
        match variable with
        | name when name = variableToChange -> term
        | _ when getFreeVariables innerTerm |> Set.contains variableToChange |> not || getFreeVariables newTerm |> Set.contains variable|> not
            -> Abstraction(variable, substitute innerTerm variableToChange newTerm)
        | _ -> let newVariable = getFreeVariables innerTerm + getFreeVariables newTerm |> getNewValueNotFromTheSet
               Abstraction(newVariable, innerTerm |> substitute (Variable newVariable) variable |> substitute newTerm variableToChange)

/// Applies beta-reduction by the normal strategy to the term
let rec reduce term = 
    match term with
    | Variable _ -> term
    | Application(left, right) ->
        match left with
        | Abstraction(variable, innerTerm) -> substitute innerTerm variable right |> reduce
        | _ -> Application(reduce left, reduce right)
    | Abstraction(variable, innerTerm) -> Abstraction(variable, reduce innerTerm)