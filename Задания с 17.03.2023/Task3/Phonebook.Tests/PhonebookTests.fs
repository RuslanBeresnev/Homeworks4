module Phonebook.Tests

open NUnit.Framework
open Phonebook

[<Test>]
let addPhoneAndNameFunctionTest () =
    let result = addPhoneAndName [] "User" "89999999999"
    Assert.AreEqual([("User", "89999999999")], result)

[<Test>]
let getPhoneByNameFunctionTest () =
    let result = getPhoneByName [("User", "89999999999")] "User"
    Assert.AreEqual("89999999999", result.Head)

[<Test>]
let getNameByPhoneFunctionTest () =
    let result = getNameByPhone [("User", "89999999999")] "89999999999"
    Assert.AreEqual("User", result.Head)

[<Test>]
let writeDataToFileAndReadDataFromFileFunctionsTest () =
    writeDataToFile [("User", "89999999999")] "../../../Phonebook.txt"
    let result = readDataFromFile "../../../Phonebook.txt"
    Assert.AreEqual([("User", "89999999999")], result)