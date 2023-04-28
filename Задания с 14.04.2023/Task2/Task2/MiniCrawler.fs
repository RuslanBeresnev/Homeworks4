module MiniCrawler

open System.Text.RegularExpressions

/// Finds all links matching with linkRegex
let getAllLinks (html : string) =
    [for matches in (Regex("<a href\s*=\s*\"?(https?://[^\"]+)\"?\s*>", RegexOptions.Compiled).Matches(html) : MatchCollection) 
    -> matches.Groups.[1].Value]