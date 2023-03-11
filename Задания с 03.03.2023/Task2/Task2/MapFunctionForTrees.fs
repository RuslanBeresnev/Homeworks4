module MapFunctionForTress

type Tree<'a> =
    | Tree of 'a * Tree<'a> * Tree<'a>
    | Leaf of 'a

let rec mapFunctionForTree mapping (tree : Tree<'a>) =
    match tree with
    | Tree(node, left, right) -> Tree(mapping node, mapFunctionForTree mapping left, mapFunctionForTree mapping right)
    | Leaf value -> Leaf(mapping value)