namespace Examples.Canvas.RectDragging

module View =
    open Avalonia.Controls
    open Avalonia.Controls.Shapes
    open Avalonia.FuncUI
    open Avalonia.FuncUI.DSL

    type RectData = {
        x: float
        y: float
        width: float
        height: float
    }

    type DragData = {
        draggedRectIndex: int
        cornerOffsetX: float
        cornerOffsetY: float
    }

    type DragState =
    | Dragging of DragData
    | Idle

    let view() =
        Component( fun ctx ->
            let rectListState = ctx.useState [
                { x = 60; y = 40; width = 200; height = 40 };
                { x = 140; y = 120; width = 160; height = 60 };
                { x = 80; y = 240; width = 100; height = 120 }
            ]
            let dragState = ctx.useState Idle

            Canvas.create [
                Canvas.background "white"
                Canvas.children (
                    rectListState.Current
                    |> List.mapi (fun rectIndex rectData ->
                        let rectColor = 
                            match dragState.Current with
                            | Dragging dragData ->
                                if rectIndex = dragData.draggedRectIndex then
                                    "greenyellow"
                                else
                                    "blue"
                            | Idle -> "blue"

                        Rectangle.create [
                            Canvas.left rectData.x
                            Canvas.top rectData.y
                            Rectangle.width rectData.width
                            Rectangle.height rectData.height
                            Rectangle.fill rectColor
                            Rectangle.onPointerPressed (
                                func = (fun args ->
                                    let pointerPos = args.GetPosition ctx.control
                                    let newDragState = Dragging {
                                        draggedRectIndex = rectIndex;
                                        cornerOffsetX = rectData.x - pointerPos.X;
                                        cornerOffsetY = rectData.y - pointerPos.Y
                                    }
                                    dragState.Set newDragState
                                ),
                                subPatchOptions = SubPatchOptions.OnChangeOf rectData
                            )
                            Rectangle.onPointerMoved (fun args ->
                                let pointerPos = args.GetPosition ctx.control
                                match dragState.Current with
                                | Dragging dragData ->
                                    let newRectList =
                                        rectListState.Current
                                        |> List.mapi (fun rectIndex rectData ->
                                            if rectIndex = dragData.draggedRectIndex then
                                                let newRectData = {
                                                    x = pointerPos.X + dragData.cornerOffsetX;
                                                    y = pointerPos.Y + dragData.cornerOffsetY;
                                                    width = rectData.width;
                                                    height = rectData.height;
                                                }
                                                newRectData
                                            else
                                                rectData
                                        )
                                    rectListState.Set newRectList
                                | Idle -> ()
                            )
                            Rectangle.onPointerReleased (fun args -> dragState.Set Idle)
                        ]
                    )
                )
            ]
        )
