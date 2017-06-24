open System

type Model = Task list
and Task = { id: int; name: string; completedTime: DateTime option }

type Action = 
  | AddTask of string
  | UpdateTask of int * string
  | CompleteTask of int
  | UncheckTask of int
  | RemoveTask of int

module Views = 
  let renderTask (task: Task) = 
    let timeToStr = function 
      | None -> String.Empty
      | Some (value: DateTime) -> value.ToString(" -> dd.MM.yy hh:mm:ss")
    printfn "%d. %s%s" task.id task.name (timeToStr task.completedTime)

  let renderTasks (model: Task list) = 
    model
    |> List.iter renderTask

  let renderModel (model: Model) = 
    model
    |> List.iter renderTask

  let renderMainMenu () = 
    [
      "1. AddTask"
      "2. UpdateTask"
      "3. CompleteTask"
      "4. UnCompleteTask"
      "5. RemoveTask"
      "0. Quit"
    ]
    |> List.iter (printfn "%s")
    let input = Console.ReadLine()
    match input with
      | "0" -> 
      | "1" -> 

let view (model: Model) = 
  printfn "Your tasks:"
  printfn "Choose action:"


let init = 
  [
     { id = 1; name = "111"; completedTime = None }
     { id = 2; name = "222"; completedTime = Some DateTime.Now }
     { id = 3; name = "333"; completedTime = None }
  ]

let update (model: Model) (action: Action) : Model =
  init



init |> view
