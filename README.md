Updated to V1.4
## Panel layout & visibility

`ChartControl` is organized as a five-region layout: a central plotting
surface surrounded by four edge panels, each hosting an `AxisCanvas`.

| Region | Panel | Axis property | Typical role |
|---|---|---|---|
| Center | `MainContentArea` | `ChartingArea` (`Plot`) | The plot surface |
| Bottom | `BottomPanel` | `XAxis` | Primary horizontal axis |
| Right | `RightPanel` | `YAxis` | Primary vertical axis |
| Left | `LeftPanel` | `ZAxis` | Secondary vertical axis |
| Top | `TopPanel` | `WAxis` | Secondary horizontal axis |

### Visibility

Each edge panel has a corresponding boolean dependency property:

- `ShowTopPanel`
- `ShowBottomPanel`
- `ShowLeftPanel`
- `ShowRightPanel`

All four default to **`false`** — panels are `Collapsed` until explicitly
enabled, so a freshly instantiated chart shows only the central plot area.
Setting a property to `true` makes the panel `Visible`; setting it back to
`false` collapses it (the panel releases its layout space rather than
merely hiding).

Because these are dependency properties, they support XAML binding and can
be toggled at runtime:

```xml
<charts:ChartControl ShowBottomPanel="True"
                     ShowRightPanel="True"
                     ShowLeftPanel="{Binding ShowSecondaryAxis}" />
```

Visibility is independent of content: a panel can hold an `AxisCanvas`
while hidden, and toggling visibility does not detach or reset the axis.
Conversely, showing a panel with no assigned axis displays an empty strip.

### Axis content

The axis properties (`XAxis`, `YAxis`, `ZAxis`, `WAxis`) are dependency
properties of type `AxisCanvas`. Assigning one injects the canvas into its
panel; assigning `null` clears it. `ChartingArea` (`Plot`) fills the
central region and is bound directly in XAML.



## Zooming

Mouse-wheel zooming is routed through the chart's `ZoomMode` property, which
determines which axis (or combination of axes) responds to wheel input. The
chart forwards the `MouseWheelEventArgs` to each targeted axis via
`ZoomAxis(MouseWheelEventArgs e)`, and each axis applies its own zoom logic
in `OnMouseWheel`.

### ZoomMode values

| Mode | Axes affected | Use case |
|---|---|---|
| `XAxis` | X only | Horizontal-only zoom (e.g. time range) |
| `YAxis` | Y only | Vertical-only zoom on the primary value axis |
| `ZAxis` | Z only | Zoom an auxiliary/secondary axis independently |
| `WAxis` | W only | Zoom an auxiliary/secondary axis independently |
| `WZAxes` | W + Z | Zoom both auxiliary axes together |
| `Rectangulate` | X + Y | Standard 2D zoom — both primary axes scale together |
| `Triangulate` | Y + Z + W | Zoom all value axes, leaving X (domain) fixed |
| `Quadrate` | X + Y + Z + W | Global zoom — every axis scales with the wheel |

### Behavior notes

- Axis references are null-safe: if a mode targets an axis that isn't
  configured on the chart, that axis is simply skipped. `Quadrate` on a
  two-axis chart behaves like `Rectangulate`.
- All axes targeted by a composite mode receive the *same* wheel event, so
  zoom direction and magnitude stay synchronized across them.
- `ZoomMode` can be switched at runtime to change wheel behavior without
  reconfiguring axes.


  ## DOCUMENTATION IS A WORK IN PROGRESS
