module MiniCrawler

open System.Text.RegularExpressions
open System.IO
open System.Net

/// Finds all links matching with linkRegex
let getAllLinks (html : string) =
    [for oneMatch in (Regex("<a href\s*=\s*\"?(https?://[^\"]+)\"?\s*>", RegexOptions.Compiled).Matches(html) : MatchCollection) 
    -> oneMatch.Groups[1].Value]

/// Downloads page asynchronously by url
let fetchAsync (url : string) =
       async {
           try
               let request = WebRequest.Create(url)
               use! response = request.AsyncGetResponse()
               use stream = response.GetResponseStream()
               use reader = new StreamReader(stream)
               let html = reader.ReadToEnd()
               return Some html
           with 
               | _ -> printfn "Site unavailable"
                      return None
       }

/// Prints result of downloading
let printPageInfo url (downloaded : Option<string>) = 
    match downloaded with
    | Some html -> printfn "%s    Length: %d symbols" url html.Length
    | _ -> printfn "Bad response from %s" url

/// Downoads all pages from links in the current page
let downloadLinkedPages (url : string) =
    let mainPageContent = fetchAsync url |> Async.RunSynchronously
    match mainPageContent with
    | Some content -> let links = content |> getAllLinks
                      let downloadedPages = links |> List.map (fun link -> link |> fetchAsync) |> Async.Parallel |> Async.RunSynchronously
                      downloadedPages |>  Array.iteri (fun i (result : string option) -> printPageInfo (links.Item i) result)
    | None -> ()