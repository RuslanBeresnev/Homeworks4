module ThreadSafeStack.Tests

open NUnit.Framework
open FsUnit
open ThreadSafeStack

[<Test>]
let ``Push and Pop methods simple test`` () =
    let stack = ThreadSafeStack<int>()
    stack.Push(1)
    stack.Push(2)
    stack.Push(3)
    stack.TryPop().Value |> should equal 3
    stack.TryPop().Value |> should equal 2
    stack.TryPop().Value |> should equal 1

[<Test>]
let ``Pop from empty stack test`` () =
    let stack = ThreadSafeStack<int>()
    stack.TryPop() |> should equal None