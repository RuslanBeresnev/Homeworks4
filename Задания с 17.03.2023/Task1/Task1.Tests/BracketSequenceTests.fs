module BracketSequence.Tests

open NUnit.Framework
open BracketSequence

[<Test>]
let SimpleCorrectSequenceTest () =
    Assert.True(checkingBracketSequenceForCorrectness ['('; '('; ')'; '('; ')'; ')'])

[<Test>]
let SimpleIncorrectSequenceTest () =
    Assert.False(checkingBracketSequenceForCorrectness [')'; '('; ')'; '('])

[<Test>]
let ComplexCorrectSequenceTest () =
    Assert.True(checkingBracketSequenceForCorrectness ['{'; '['; '('; ')'; ']'; '}'])

[<Test>]
let ComplexIncorrectSequenceTest () =
    Assert.False(checkingBracketSequenceForCorrectness ['{'; '['; '}'; ']'])