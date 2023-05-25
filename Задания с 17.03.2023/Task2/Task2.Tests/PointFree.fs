module PointFree

open NUnit.Framework
open FsCheck

let func x l = List.map (fun y -> y * x) l

let func1 x = List.map (fun y -> y * x)

let func2 x = List.map (fun y -> (*) x y)

let func3 x = List.map ((*) x)

let func4 = List.map << (*)

[<Test>]
let func1Test () =
    let func1IsCorrect x l = func x l = func1 x l
    Check.QuickThrowOnFailure func1IsCorrect
    
[<Test>]
let func2Test () =
    let func2IsCorrect x l = func x l = func2 x l
    Check.QuickThrowOnFailure func2IsCorrect

[<Test>]
let func3Test () =
    let func3IsCorrect x l = func x l = func3 x l
    Check.QuickThrowOnFailure func3IsCorrect

[<Test>]
let func4Test () =
    let func4IsCorrect x l = func x l = func4 x l
    Check.QuickThrowOnFailure func4IsCorrect