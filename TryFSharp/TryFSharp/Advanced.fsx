//##################### A little bit of currying ##############################
//let add x y = x + y

let add x y =
    printfn "the x value is %d and the y value is %d" (x) (y)
    x + y

let inc = add 1
let anotherInc x = add 1 x
printfn "%d is the same as %d" (inc 1) (anotherInc 1)

inc 2

let searchEven = Seq.filter (fun v -> v % 2 = 0)
printfn "%d even numbers in [1, 100]" ([ 1 .. 100] |> searchEven |> Seq.length)
printfn "%d even numbers in [1, 1000]" ([ 1 .. 1000] |> searchEven |> Seq.length)

// swapping arguments
let sub x y =
    printfn "x = %d     y = %d" (x) (y)
    x - y

let swapargs f x y = f y x

let dec = sub 1
//let sdec = (fun f x y -> f y x) sub 1
let sdec = swapargs sub 1
printfn "Before 10 there is %d" (dec 10)
printfn "s Before 10 there is %d" (sdec 10)

//########################### Operatr definition and overloading ##################
open System.Text.RegularExpressions

// <1 test match>
//let (^?) a b = Regex.IsMatch(a, b)
// perform a match and returns match info
//let (^!) a b = Regex.Match(a, b)
// Query the number of matches
//let (!@) (a:Match) = a.Groups.Count - 1
// Access the nth match (1 based, equivalent of $1..$n notation)
//let (^@) (a:Match) (b:int) = a.Groups.[b].Value

//let input = "F# 3.0 is a very cool programming language"
//if input ^? @"F# [\d\.]+" then
//  let m = input ^! @"F# ([\d\.]+)"
//  printfn "matched %d groups and the F# version is %s" !@m (m^@1)
// </1 test match>


let string2opt (s:string) =
    let mutable ret = RegexOptions.ECMAScript
    for c in s do
        match c with
        | 's' -> ret <- ret ||| RegexOptions.Singleline
        | 'm' -> ret <- ret ||| RegexOptions.Multiline
        | 'i' -> ret <- ret ||| RegexOptions.IgnoreCase
        | _ -> ()
    ret

// <2 test match>
let (^?) a (b, c) = Regex.IsMatch(a, b, string2opt c)
// perform a match and returns match info
let (^!) a (b, c) = Regex.Match(a, b, string2opt c)
// Query the number of matches
let (!@) (a:Match) = a.Groups.Count - 1
// Access the nth match (1 based, equivalent of $1..$n notation)
let (^@) (a:Match) (b:int) = a.Groups.[b].Value

let input = "F# 3.0 is a very cool programming language"
if input ^? (@"F# [\d\.]+", "i") then // ignore case
    let m = input ^! (@"F# ([\d\.]+)", "")
    printfn "matched %d groups and the F# version is %s" !@m (m^@1)
// </2 test match>


let (^~) a (b, c:string, d) = Regex.Replace(a, b, c, string2opt d)
printfn "%s" (input ^~ ("very", "super", ""))

// overloading an operator within a type

type Point(x:float, y:float) =
  member this.X = x
  member this.Y = y

  static member (+) (p1:Point, p2:Point) = 
      new Point(p1.X + p2.X, p1.Y+p2.Y)

let p1, p2 = new Point(0., 1.), new Point(1.,1.)
p1 + p2