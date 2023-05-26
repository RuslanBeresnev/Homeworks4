module Rhombus

/// Create a row as a string by rhombus side length and amount of stars in row
let createRow rhombusSideLength starAmount =
    let rec addSymbol symbolIndex currentRow =
        let rowLength = rhombusSideLength * 2 - 1
        if symbolIndex = rowLength then currentRow
        else
            let indentLength = (rowLength - starAmount) / 2 |> int
            if [0..indentLength - 1] |> List.contains symbolIndex then
                addSymbol (symbolIndex + 1) (currentRow + " ")
            else if [indentLength..indentLength + starAmount - 1] |> List.contains symbolIndex then
                addSymbol (symbolIndex + 1) (currentRow + "*")
            else
                addSymbol (symbolIndex + 1) (currentRow + " ")
    addSymbol 0 ""

/// Get picture of top half of rhombus as list of string rows
let getRhombusByRowsOfSymbols n =
    let rec addRow starAmount rowIndex listWithRows =
        if starAmount = n * 2 + 1 then listWithRows
        else addRow (starAmount + 2) (rowIndex + 1) (listWithRows @ [createRow n starAmount])
    addRow 1 0 []

/// Draw a rhombus with edges of length n
let printRhombus n =
    let rhombusRows = getRhombusByRowsOfSymbols n
    let rec printRow rowIndex =
        if rowIndex < n * 2 - 1 then
            let symmetricalRowIndex = if rowIndex >= n then (n - 1) * 2 - rowIndex else rowIndex
            printfn "%s" (List.item symmetricalRowIndex rhombusRows)
            printRow (rowIndex + 1)
    printRow 0