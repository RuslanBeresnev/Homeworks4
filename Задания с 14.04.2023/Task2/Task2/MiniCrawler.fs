module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

// Get links from main page
let getAllLinks (html : string) =
    [for oneMatch in (Regex("<a href\s*=\s*\"?(https?://[^\"]+)\"?\s*>", RegexOptions.Compiled).Matches(html) : MatchCollection) 
    -> oneMatch.Groups[1].Value]

// Download html from url
let fetchAsync (url : string) (client : HttpClient) =
    client.GetStringAsync url |> Async.AwaitTask |> Async.Catch
 
// Get sizes list from linked pages
let getSizes pages =
    pages
    |> Seq.map (fun page ->
        match page with
        | Choice1Of2 (x : string) -> Some x.Length
        | Choice2Of2 (_ : exn) -> None)

// HttpClient creating
let client = new HttpClient()

// Get all refered links and their sizes
let crawl url =
    async {
        let! page = fetchAsync url client
        let html =
            match page with
            | Choice1Of2 result -> Some result
            | Choice2Of2 (_ : exn) -> None
        match html with
        | Some value ->
            let links = getAllLinks value
            let! pages = links |> Seq.map (fun link -> fetchAsync link client) |> Async.Parallel
            return getSizes pages |> Seq.zip links
        | None -> return List.empty
    }