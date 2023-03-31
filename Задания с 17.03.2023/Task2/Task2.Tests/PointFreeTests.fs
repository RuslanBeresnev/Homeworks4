module PointFree.Tests

open NUnit.Framework
open PointFree
open FsCheck

[<Test>]
let funcTest1 () =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func1 x l))
    
[<Test>]
let funcTest2 () =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func2 x l))

[<Test>]
let funcTest3 () =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func3 x l))

[<Test>]
let funcTest4 () =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func4 x l))