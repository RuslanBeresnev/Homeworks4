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

[<Test>]
let tryToGetDataMissingInPhonebookTest () =
    let phonebook = addPhoneAndName [] "User" "89999999999"
    let missingName = getNameByPhone phonebook "111-111-111"
    let missingPhone = getPhoneByName phonebook "Anonim"
    Assert.IsEmpty(missingName)
    Assert.IsEmpty(missingPhone)

[<Test>]
let tryToAddNameThatAlreadyInPhonebookTest () =
    let phonebook = [("User", "111-111-111")]
    let result = addPhoneAndName phonebook "User" "222-222-222"
    Assert.AreEqual(phonebook, result)

[<Test>]
let tryToAddPhoneThatAlreadyInPhonebookTest () =
    let phonebook = [("User", "111-111-111")]
    let result = addPhoneAndName phonebook "User2" "111-111-111"
    Assert.AreEqual(phonebook, result)

[<Test>]
let tryToGetPhonebookFromEmptyFileTest () =
    let data = readDataFromFile "../../../Empty File.txt"
    Assert.IsEmpty(data)