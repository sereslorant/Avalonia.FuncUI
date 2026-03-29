namespace Examples.Canvas.RectSelection

module View =
    open Avalonia.Controls
    open Avalonia.Controls.Shapes
    open Avalonia.FuncUI
    open Avalonia.FuncUI.DSL

    let view() =
        Component( fun ctx ->
            let state = ctx.useState None
            Canvas.create [
                Canvas.background "white"
                Canvas.children [
                    Rectangle.create [
                        Canvas.left 60
                        Canvas.top 40
                        Rectangle.width 200
                        Rectangle.height 40
                        Rectangle.fill (if state.Current = Some 0 then "greenyellow" else "blue")
                        Rectangle.onTapped (fun _ -> state.Set (Some 0))
                    ]
                    Rectangle.create [
                        Canvas.left 140
                        Canvas.top 120
                        Rectangle.width 160
                        Rectangle.height 60
                        Rectangle.fill (if state.Current = Some 1 then "greenyellow" else "blue")
                        Rectangle.onTapped (fun _ -> state.Set (Some 1))
                    ]
                    Rectangle.create [
                        Canvas.left 80
                        Canvas.top 240
                        Rectangle.width 100
                        Rectangle.height 120
                        Rectangle.fill (if state.Current = Some 2 then "greenyellow" else "blue")
                        Rectangle.onTapped (fun _ -> state.Set (Some 2))
                    ]
                ]
            ]
        )
