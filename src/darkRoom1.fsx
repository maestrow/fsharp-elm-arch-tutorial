(*
  Первый вариант реализации. 
*)
open System

type Model = 
  {
    hero: Location
    treasure: Location
    roomSize: Size
  }
and Location = { x: int; y: int }
and Size = { width: int; height: int }

type Action = 
  | North
  | South
  | West
  | East

let update (model: Model) (action: Action) : Model = 
  let fixValue (value: int) (max: int) = Math.Min (Math.Max(value, 1), max)
  let (dx, dy) = 
    match action with
    | North -> (0, -1)
    | South -> (0, 1)
    | West -> (-1, 0)
    | East -> (1, 0)
  let newLocation = 
    {
      x = fixValue (model.hero.x + dx) model.roomSize.width
      y = fixValue (model.hero.y + dy) model.roomSize.height
    }
  { model with hero = newLocation }

let view (model: Model) (dispatcher: Action -> unit) = 
  if model.hero = model.treasure then 
    printfn "Congratulations! You found the treasure."
  else
    printf "Your location: (%d, %d). Where to move next? (N)orth, (S)outh, (W)est, (E)ast : " model.hero.x model.hero.y
    let choice = (Console.ReadLine()).ToUpper()
    let action = 
      match choice with
      | "N" -> Some North
      | "S" -> Some South
      | "W" -> Some West
      | "E" -> Some East
      | _ -> None
    if action.IsNone then
      printfn "Incorrect direction. Valid inputs are: N, S, W, E. Try again."
    else
      dispatcher action.Value
      
let init () = 
  let random = Random ()
  let (width, height) = (4, 4)
  let getRandomLocation () = 
    { x = random.Next(1, width+1); y = random.Next (1, height+1) }
  {
    hero = getRandomLocation ()
    treasure = getRandomLocation ()
    roomSize = { width = width; height = height }
  }

let runApp (init: unit -> Model) (update: Model -> Action -> Model) (view: Model -> (Action -> unit) -> unit) = 
  let mutable model = init ()
  let rec dispatcher (action: Action) = 
    model <- update model action
    view model dispatcher
  ()
  view model dispatcher

runApp init update view