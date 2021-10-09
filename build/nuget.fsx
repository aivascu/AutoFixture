#load "../.fake/build.fsx/intellisense.fsx"

open Fake.Core

type FeedProvider =
  | NuGet
  | MyGet

type NuGetFeed = { ApiKey: string; Source: string }

let getFeed key source =
  let envVar = Environment.environVarOrNone key
  match envVar with
  | None -> None
  | Some apiKey -> Some {
    ApiKey = apiKey
    Source = source
  }

let getFeed provider =
  match provider with
  | NuGet -> getFeed
              "NUGET_API_KEY"
              "https://www.nuget.org/api/v2/package"
  | MyGet -> getFeed
              "MYGET_API_KEY"
              "https://www.myget.org/F/autofixture/api/v2/package"