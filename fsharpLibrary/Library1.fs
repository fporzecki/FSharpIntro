module Translations.Types

open System

type Person(name, age:string) =
    member this.Name = name
    member this.Age = age

    override this.ToString() =
        sprintf "Name: %s, Age: %s" this.Name this.Age

let ToPerson name age =
    Person(name, age)