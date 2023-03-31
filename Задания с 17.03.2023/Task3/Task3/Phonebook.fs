module Phonebook

open System.IO
open System

let printCommands =
    printfn "Commands by numbers:"
    printfn "Exit: 1"
    printfn "Add new entry (phone and name): 2"
    printfn "Get name by phone: 3"
    printfn "Get phone by name: 4"
    printfn "Print all entries: 5"
    printfn "Save data to file: 6"
    printfn "Read data from file: 7"

let addPhoneAndName phonebook name phone = (name, phone) :: phonebook 

let getPhoneByName phonebook name =
    phonebook |> List.filter(fun (currentName, _) -> name = currentName) |> List.map(snd)

let getNameByPhone phonebook phone =
    phonebook |> List.filter(fun (_, currentPhone) -> phone = currentPhone) |> List.map(fst)

let rec printPhonebook phonebook =
    match phonebook with
    | [] -> printf ""
    | head :: tail ->
        printfn "%A" head
        printPhonebook tail

let writeDataToFile phonebook path =
    let newList = phonebook |> List.map(fun (name, phone) -> name + " " + phone)
    File.WriteAllLines(path, newList)

let readDataFromFile path =
    let file = Seq.map (fun (str: string) -> (str.Split(" ")[0], str.Split(" ")[1])) (File.ReadAllLines(path))
    Seq.toList file

let rec userInterface phonebook = 
    printCommands
    let command = Console.ReadLine()

    match command with
    | "1" -> ignore
    | "2" ->
        printfn "Enter name and phone"
        let name = Console.ReadLine()
        let phone = Console.ReadLine()
        printfn "New entry added"
        userInterface (addPhoneAndName phonebook name phone)
    | "3" ->
        printfn "Enter phone"
        let phone = Console.ReadLine()
        printfn "Found name: %A" (getNameByPhone phonebook phone)
        userInterface phonebook
    | "4" ->
        printfn "Enter name"
        let name = Console.ReadLine()
        printfn "Found phone: %A" (getPhoneByName phonebook name)
        userInterface phonebook
    | "5" ->
        printfn "All entries:"
        printPhonebook phonebook
        userInterface phonebook
    | "6" ->
        printfn "Enter path to file"
        let path = Console.ReadLine();
        writeDataToFile phonebook path
        userInterface phonebook
    | "7" ->
        printfn "Enter path to file"
        let path = Console.ReadLine();
        let fileData = readDataFromFile path
        userInterface fileData
    | _ ->
        printfn "Incorrect command ..."
        userInterface phonebook