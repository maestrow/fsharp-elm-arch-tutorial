open System

type Model = Task list
and Task = { id: int; name: string; completedTime: DateTime option }

module Views = 
  let renderTask (task: Task) = 
    let timeToStr = function 
      | None -> String.Empty
      | Some (value: DateTime) -> value.ToString(" -> dd.MM.yy hh:mm:ss")
    printfn "%d. %s%s" task.id task.name (timeToStr task.completedTime)

  let renderModel (model: Model) = 
    model
    |> List.iter renderTask

let init = 
  [
     { id = 1; name = "111"; completedTime = None }
     { id = 2; name = "222"; completedTime = Some DateTime.Now }
     { id = 3; name = "333"; completedTime = None }
  ]

init |> Views.renderModel
