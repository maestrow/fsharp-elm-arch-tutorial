(*
  Пока приложение открыто, пользователь всегда может выполнить некоторое действие.
  В консольном варианте приложения это означает, что после любого вывода данных ожидается ввод.
*)

open System

type Model = 
  { 
    tasks: Task list
    display: Display
  }
and Task = { id: int; name: string }
and Display = 
  | AddTask
  | RemoveTask
  | Tasks
  | MainMenu
  | Error of string

type Action = 
  | AddTask of string
  | RemoveTask of int

module Helpers = 
  let mutable private identity = 0
  
  let private incrementId () = 
    identity <- identity + 1
    identity

  let createTask name = 
    { id = incrementId (); name = name }

let update (model: Model) (action: Action) : Model = 
  match action with
    | Action.AddTask taskName ->
      let tasks = (Helpers.createTask taskName) :: model.tasks
      { tasks = tasks; display = Display.Tasks }
    | Action.RemoveTask id -> 
      let tasks = model.tasks |> List.filter (fun t -> t.id <> id)
      { tasks = tasks; display = Display.Tasks }

module Render = 
  
  let rec showTasks (model: Model) = 
    printfn "Your tasks:"
    model.tasks |> List.iter (fun task -> printfn "%d. %s" task.id task.name)
    main { tasks = model.tasks; display = Display.MainMenu }
  
  and mainMenu (model: Model) =
    printfn "Choose action:"
    [
      "1. AddTask"
      "2. RemoveTask"
      "0. Quit"
    ]
    |> List.iter (printfn "%s")
    let userChoice = Console.ReadLine ()
    let display = match userChoice with
      | "0" -> Environment.Exit(0); Display.Tasks
      | "1" -> Display.AddTask
      | "2" -> Display.RemoveTask
      | _ -> Display.Error "Incorrect input. Try again."
    main { tasks = model.tasks; display = display }

  and showError message (model: Model) = 
    printfn "%s" message
    mainMenu model

  and addTask (model: Model) = 
    printf "Enter task name: "
    let taskName = Console.ReadLine ()
    let newModel = update model (Action.AddTask taskName)
    main newModel

  and removeTask (model: Model) = 
    printf "Enter task id to remove: "
    let taskId = Console.ReadLine ()
    let newModel = update model (Action.RemoveTask (int taskId))
    main newModel

  and main (model: Model) = 
    match model.display with
      | Display.ShowTasks -> tasks ()
      | Display.AddTask -> addTask model
      | Display.RemoveTask -> removeTask model



let init = 
  let tasks = 
    [
       { id = 1; name = "111" }
       { id = 2; name = "222" }
       { id = 3; name = "333" }
    ]
  { tasks = tasks; display = Display.ShowTasks }

let update (model: Model) (action: Action) : Model =
  init



init |> view
