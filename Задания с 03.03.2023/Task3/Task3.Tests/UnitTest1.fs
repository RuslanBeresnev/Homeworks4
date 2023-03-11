module ParseTree.Tests

open NUnit.Framework
open ParseTree

[<Test>]
let StandardParseTreeTest () =
    Assert.AreEqual(27.5, evaluateParseTree (Multiplication(Addition(Operand(5), Operand(6)), Division(Operand(5), Operand(2)))))

[<Test>]
let ParseTreeWithOneElementTest () =
    Assert.AreEqual(1, evaluateParseTree (Operand(1)))