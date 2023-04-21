let createRow rhombusSideLength starAmount =
    let rec addSymbol (currentRow : string) (symbolIndex : int) =
        if symbolIndex = rhombusSideLength * 2 - 1 then currentRow
        else
            let indentLength = ((rhombusSideLength * 2 - 1) - starAmount / 2) |> int
            if [0..indentLength - 1] |> List.contains symbolIndex then
                addSymbol (currentRow + " ") (symbolIndex + 1)
            else if [indentLength..indentLength + starAmount - 1] |> List.contains symbolIndex then
                addSymbol (currentRow + "*") (symbolIndex + 1)
            else
                addSymbol (currentRow + " ") (symbolIndex + 1)
    addSymbol "" 0

let getRhombusByRowsOfSymbols n =
    let rec addRow starAmount listWithRows iteration =
        if starAmount = n * 2 - 1 then listWithRows
        else
            addRow (starAmount + 2) (listWithRows @ [createRow n starAmount]) (iteration + 1)
    addRow 1 [] 0

let printRhombus n =
    let rhombusRows = getRhombusByRowsOfSymbols n
    let rec printRow rowIndex =
        if rowIndex < n * 2 - 1 then 
            let correctRowIndex = if rowIndex >= n then n - (rowIndex % n) - 1 else rowIndex
            printfn "%s" (List.nth rhombusRows correctRowIndex)
            printRow (rowIndex + 1)
    printRow 0

printRhombus 3