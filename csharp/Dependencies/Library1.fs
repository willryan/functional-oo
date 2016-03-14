namespace Dependencies

type ImmRecord = 
  {
     X : string   
     Y : int
     Z : bool
  }

type EasyAdt =
  | Foo of int
  | Bar of string
  | Baz

module Funcs =
  let gimmeAString = function
    | Foo v -> v.ToString()
    | _ -> "not a FOO"

  let allCases = function
    | Foo v -> v.ToString()
    | Bar v -> v.ToUpper()
    | Baz -> "Bazazaz"


  