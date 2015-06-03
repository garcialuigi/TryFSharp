
//####################################################################################

// hello world
let lucky = 3 + 4
let unlucky = lucky + 6

// imutable
let duplicated = "original value"
duplicated = "new value"

// mutable
let mutable modifiable = "original value"
modifiable <- "new value" // this one works
modifiable = "new value2" // does not do shit

// types
let anInt = 10
let aFloat = 20.0
let aString = "I'm a string!"

// print to console
printfn "hello world from Try F#"
printfn "the answer is %d" 42
//printfn "the answer is %d" "not an integer!" // this is an error


//######################### FUN WITH FUNCTIONAL FUNCTIONS ############################

// ===== hello world to functions
let add x y = 
    x + y

add 2 2

// This results in an error.
//let toHackerTalk phrase =
//    phrase.Replace('t', '7').Replace('o', '0')

let toHackerTalk (phrase:string) =
    phrase.Replace('t', '7').Replace('o', '0')


// ===== functions as values
let quadruple x =
    let theDouble x =
        x * 2
    theDouble(theDouble(x))

quadruple 2
quadruple(2)

//
let chrisTest test =
    test "Chris"

let isMe x =
    printfn "called the isMe function"
    if x = "Chris" then
        "it is Chris!"
    else
        "it's someone else"

let isLuigi x =
    printfn "called the isLuigi function"
    if x = "Luigi" then
        "it is Luigi!"
    else
        "it's someone else"

chrisTest isMe
chrisTest isLuigi

// lambda functions
//Lambdas are also known as anonymous functions because you aren't required to bind 
//them to a name with let.
let add2 = (fun x y -> x + y)
add2 2 2

let twoTest test =
    test 2

twoTest (fun x -> x < 0)

//############################# Chaining functions wit the forward pipe operator #####
let evens = [2; 4; 6; 8]

let firstHundred = [0..100]
let doubled = List.map (fun x -> x * 2) firstHundred
// combining functions
let doubled2 = List.map (fun x -> x * 2) (List.filter (fun x -> x % 2 = 0) firstHundred)

// introducing the forward pipe operator
List.sum (List.map (fun x -> x * 2) (List.filter (fun x -> x % 2 = 0) [0..100]))

[0..100]
|> List.filter (fun x -> x % 2 = 0)
|> List.map (fun x -> x * 2)
|> List.sum

// testing array and list
let listTest = [1;2;3]
let arrayTest = [|1;2;3|]
listTest.[1]
arrayTest.[1]

 //below is a list of comma separated stock data. Let's walk through the process of 
 //finding the day with the greatest variance between the opening and closing price. 

 //"Date,Open,High,Low,Close,Volume,Adj Close"
let stockData =
    [ 
      "2012-03-30,32.40,32.41,32.04,32.26,31749400,32.26";
      "2012-03-29,32.06,32.19,31.81,32.12,37038500,32.12";
      "2012-03-28,32.52,32.70,32.04,32.19,41344800,32.19";
      "2012-03-27,32.65,32.70,32.40,32.52,36274900,32.52";
      "2012-03-26,32.19,32.61,32.15,32.59,36758300,32.59";
      "2012-03-23,32.10,32.11,31.72,32.01,35912200,32.01";
      "2012-03-22,31.81,32.09,31.79,32.00,31749500,32.00";
      "2012-03-21,31.96,32.15,31.82,31.91,37928600,31.91";
      "2012-03-20,32.10,32.15,31.74,31.99,41566800,31.99";
      "2012-03-19,32.54,32.61,32.15,32.20,44789200,32.20";
      "2012-03-16,32.91,32.95,32.50,32.60,65626400,32.60";
      "2012-03-15,32.79,32.94,32.58,32.85,49068300,32.85";
      "2012-03-14,32.53,32.88,32.49,32.77,41986900,32.77";
      "2012-03-13,32.24,32.69,32.15,32.67,48951700,32.67";
      "2012-03-12,31.97,32.20,31.82,32.04,34073600,32.04";
      "2012-03-09,32.10,32.16,31.92,31.99,34628400,31.99";
      "2012-03-08,32.04,32.21,31.90,32.01,36747400,32.01";
      "2012-03-07,31.67,31.92,31.53,31.84,34340400,31.84";
      "2012-03-06,31.54,31.98,31.49,31.56,51932900,31.56";
      "2012-03-05,32.01,32.05,31.62,31.80,45240000,31.80";
      "2012-03-02,32.31,32.44,32.00,32.08,47314200,32.08";
      "2012-03-01,31.93,32.39,31.85,32.29,77344100,32.29";
      "2012-02-29,31.89,32.00,31.61,31.74,59323600,31.74"; ]


let splitCommas (x:string) =
    x.Split([|','|])

stockData
|> List.map splitCommas
|> List.maxBy (fun x -> abs(float x.[1] - float x.[4]))
|> (fun x -> x.[0])


//splitCommas takes a string and breaks it into pieces whenever it finds a comma. 
//String.Split is a function that splits a string based on takes an array of separator characters. 
//Arrays are another basic container type in F# that allow you to index stored elements. 
//You create arrays using a [| ... |] which is similar to the [ .. ] syntax used for lists. 
//Here,[|','|] creates a single element array containing a comma. The result of the splitCommas is also an array. 
//This array contains the individual string parts. For example, parsing the last line of the input yields 
//the array [| 2012-02-29; 31.89; 32.00; 31.61; 31.74; 59323600; 31.74 |].
//Now that you've separated the data by commas, you need to find the maximum variance between the opening and closing days. 
//You accomplish that using the List.maxBy function, which finds the maximum item in a List based a given comparison function. 
//You use a comparison function of (fun x -> abs(float x.[1] - float x.[4])) to find the maximum variance. 
//Note that abs is a function for calculating absolute value, and float is a function to parse a floating point number from a string. 
//The x.[1] and x.[4] calls get the second and fifth elements in the array, respectively (in F# array indexing is zero-based).
//Finally, you project the date from the maximum row using List.map and a projection function of (fun x -> x.[0]).

//############### Usiong data structures to create larger programs ##################
type Book = 
  { Name: string;
    AuthorName: string;
    Rating: int;
    ISBN: string }

let expertFSharp = 
  { Name = "Expert F#";
    AuthorName = "Don Syme, Adam Granicz, Antonio Cisternino";
    Rating = 5;
    ISBN = "1590598504" }

printfn "I give this book %d stars out of 5!" expertFSharp.Rating

//expertFSharp.AuthorName <- "Chris Marinos" // error, the data is not mutable

let partDeux = { expertFSharp with Name = "Expert F# 2.0" }

type VHS =
  { Name: string;
    AuthorName: string;
    Rating: string; // Videos use a different rating system.
    ISBN: string }

// problem with equal field names

let theFSharpQuizBook = 
  { Name = "The F# Quiz Book";
    AuthorName = "William Flash";
    Book.Rating = 5; // force to point to book
    ISBN = "1234123412" }

// option types

type Book2 =
  { Name: string;
    AuthorName: string;
    Rating: int option;
    ISBN: string }

let unratedEdition = 
  { Name = "Expert F#";
    AuthorName = "Don Syme, Adam Granicz, Antonio Cisternino";
    Rating = None;
    ISBN = "1590598504" }

let stingyReview = 
  { Name = "Expert F#";
    Book2.AuthorName = "Don Syme, Adam Granicz, Antonio Cisternino";
    Rating = Some 1;
    ISBN = "1590598504" }

let printRating book =
    match book.Rating with
    | Some rating -> 
        printfn "I give this book %d star(s) out of 5!" rating
    | None -> 
        printfn "I didn't review this book"

printRating stingyReview
printRating unratedEdition

//discriminated unions
type PowerUp =
| FireFlower
| Mushroom
| Star

let powerUpTest = FireFlower

match powerUpTest with
| FireFlower -> printfn "Ouch, that's hot!"
| Mushroom -> printfn "Please don't step on me..."
| Star -> printfn "Let me play some special music for you."

//

type MushroomColor =
| Red
| Green
| Purple

type PowerUp2 =
| FireFlower
| Mushroom of MushroomColor
| Star of int

let handlePowerUp powerUp =
    match powerUp with
    | FireFlower -> printfn "Ouch, that's hot!"
    | Mushroom color -> match color with
                        | Red -> printfn "Please don't step on me..."
                        | Green -> printfn "1UP!!!"
                        | Purple -> printfn "Sorry, about that!"
    | Star duration -> printfn "Let me play some special music for you for %d seconds." duration

// Test handlePowerUp.
let powerUp = Star 14
handlePowerUp powerUp

