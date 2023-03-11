module MapFunctionForTress.Tests

open NUnit.Framework
open MapFunctionForTress

let treesAreEqual tree1 tree2 =
    let rec getStringReprOfTree tree =
            match tree with
            | Tree(node, left, right) -> node.ToString() + " (" + (getStringReprOfTree left).ToString() + ", " + (getStringReprOfTree right).ToString() + ")"
            | Leaf value -> value.ToString()
    (getStringReprOfTree tree1) = (getStringReprOfTree tree2)

[<Test>]
let integerTreeMappingStandardTest () =
    let testTree = Tree(5, Tree(3, Leaf(1), Leaf(2)), Tree(7, Leaf(6), Leaf(8)))
    let mappedTree = mapFunctionForTree (fun x -> x * x) testTree
    let correctMappedTree = Tree(25, Tree(9, Leaf(1), Leaf(4)), Tree(49, Leaf(36), Leaf(64)))
    Assert.True(treesAreEqual mappedTree correctMappedTree)

[<Test>]
let stringTreeMappingStandardTest () =
    let testTree = Tree("a", Tree("b", Leaf("c"), Leaf("d")), Tree("e", Leaf("f"), Leaf("g")))
    let mappedTree = mapFunctionForTree (fun x -> x + x + x) testTree
    let correctMappedTree = Tree("aaa", Tree("bbb", Leaf("ccc"), Leaf("ddd")), Tree("eee", Leaf("fff"), Leaf("ggg")))
    Assert.True(treesAreEqual mappedTree correctMappedTree)

[<Test>]
let treeWithOneElementTest () =
    let testTree = Leaf(5)
    let mappedTree = mapFunctionForTree (fun x -> x + x) testTree
    let correctMappedTree = Leaf(10)
    Assert.True(treesAreEqual mappedTree correctMappedTree)