open System
#load "../.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open System.Text.RegularExpressions

type VersionInfo =
  {
    Major :int;
    Minor :int;
    Patch :int;
    Build :int;
    VersionSuffix :string;
    AssemblyVersion :string;
    FileVersion :string;
    InfoVersion :string;
    PackageVersion :string;
    CommitHash :string;
  }

type Version =
  {
    Major :int;
    Minor :int;
    Patch :int;
    Build :int;
    Suffix :string;
  }

type GitTag =
  {
    Tag :string;
    Sha :string;
    Time :DateTime;
  }

let (|Regex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success
        then Some(List.tail [ for g in m.Groups -> g.Value ])
        else None

let parseTag tag =
  match tag with
  | Regex @"v([0-9]+)\.([0-9]+)\.([0-9]+)(?-[a-zA-Z0-9]+)" [ major; minor; patch; suffix ] ->
    Some {
      Major = major |> int
      Minor = minor |> int
      Patch = patch |> int
      Build = 0
      Suffix = suffix
    }
  | _ -> None

let toVersionInfo version tag =
  let assemblyVersion = sprintf
                          "v%d.%d.0.0"
                          version.Major
                          version.Minor

  let fileVersion = sprintf
                      "v%d.%d.%d.%d"
                      version.Major
                      version.Minor
                      version.Patch
                      version.Build

  let infoVersion = sprintf
                      "v%d.%d.%d.%d-%s.%s"
                      version.Major
                      version.Minor
                      version.Patch
                      version.Build
                      version.Suffix
                      tag.Sha

  {
    Major = version.Major
    Minor = version.Minor
    Patch = version.Patch
    Build = version.Build
    VersionSuffix = version.Suffix
    AssemblyVersion = assemblyVersion
    FileVersion = fileVersion
    InfoVersion = infoVersion
    PackageVersion = "v%d.%d.0.0"
    CommitHash = "v%d.%d.0.0"
  }