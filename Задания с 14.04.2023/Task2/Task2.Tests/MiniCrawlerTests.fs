module MiniCrawler.Tests

open NUnit.Framework
open MiniCrawler
open FsUnit

[<Test>]
let CorrectURLTest () =
    let expected =
        [("https://docs.github.com/en/articles/blocking-a-user-from-your-personal-account", Some 137140);
         ("https://docs.github.com/en/articles/reporting-abuse-or-spam", Some 144255);
         ("https://docs.github.com/categories/setting-up-and-managing-your-github-profile", Some 129780)]
    let result = crawl "https://github.com/RuslanBeresnev" |> Async.RunSynchronously
    result |> should equal expected

[<Test>]
let UncorrectURLTest () =
    let result = crawl "Something" |> Async.RunSynchronously
    result |> should equal []