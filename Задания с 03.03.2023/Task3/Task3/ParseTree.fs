module ParseTree

type ParseTree =
    | Operand of float
    | Addition of ParseTree * ParseTree
    | Subtraction of ParseTree * ParseTree
    | Multiplication of ParseTree * ParseTree
    | Division of ParseTree * ParseTree

let rec evaluateParseTree (tree: ParseTree) =
    match tree with
    | Operand value -> value
    | Addition(v1, v2) -> evaluateParseTree v1 + evaluateParseTree v2
    | Subtraction(v1, v2) -> evaluateParseTree v1 - evaluateParseTree v2
    | Multiplication(v1, v2) -> evaluateParseTree v1 * evaluateParseTree v2
    | Division(v1, v2) -> evaluateParseTree v1 / evaluateParseTree v2