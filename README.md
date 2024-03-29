# ricaun.Revit.UI.StatusBar

[![Revit 2019](https://img.shields.io/badge/Revit-2019+-blue.svg)](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar/actions/workflows/Build.yml/badge.svg)](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar/actions)
[![Release](https://img.shields.io/nuget/v/ricaun.Revit.UI.StatusBar?logo=nuget&label=release&color=blue)](https://www.nuget.org/packages/ricaun.Revit.UI.StatusBar)

[![ricaun.Revit.UI.StatusBar](https://raw.githubusercontent.com/ricaun-io/ricaun.Revit.UI.StatusBar/develop/assets/ricaun.Revit.UI.StatusBar.png)](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar)

Package to create a progress bar in Revit StatusBar for Revit API developers.

[![ProgressBar](https://raw.githubusercontent.com/ricaun-io/ricaun.Revit.UI.StatusBar/develop/assets/ProgressBar.gif)](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar)

This project was generated by the [ricaun.AppLoader](https://ricaun.com/AppLoader/) Revit plugin.

## RevitProgressBarUtils

The `RevitProgressBarUtils` has some utility methods to Run a loop and update the `RevitProgressBar`.

```C#
RevitProgressBarUtils.Run("Revit Elements", elements, (element) =>
{
    System.Console.WriteLine(element.Name);
});
```

```C#
int repeat = 100000;
RevitProgressBarUtils.Run("Revit Repeat", repeat, (i) =>
{
    System.Console.WriteLine(i);
});
```

## RevitProgressBar

The `RevitProgressBar` class gives more control like the cancel button, it's implements the `IDisposable` interface, so it can be used in a `using` statement.

```C#
using (var revitProgressBar = new RevitProgressBar())
{
    revitProgressBar.Run("Revit Elements", elements, (element) =>
    {
        System.Console.WriteLine(element.Name);
    }
}
```

or...

```C#
using (var revitProgressBar = new RevitProgressBar())
{
    revitProgressBar.SetCurrentOperation("Revit Elements");
    foreach (var element in elements)
    {
        revitProgressBar.Increment();
        System.Console.WriteLine(element.Name);
    }
}
```

### Cancel Button

The `RevitProgressBar` class allows to set a cancel button, in the constructor or using the `SetHasCancelButton` method.

```C#
using (var revitProgressBar = new RevitProgressBar())
{
    revitProgressBar.SetCurrentOperation("Revit Elements");
    revitProgressBar.SetHasCancelButton(true);
    revitProgressBar.Run(elements, (element) =>
    {
        System.Console.WriteLine(element.Name);
    });
    if (revitProgressBar.IsCancelling())
    {
        // RevitProgressBar Canceled
    }
}
```

or...

```C#
using (var revitProgressBar = new RevitProgressBar(true))
{
    revitProgressBar.Run("Revit Elements", elements, (element) =>
    {
        System.Console.WriteLine(element.Name);
    });
    if (revitProgressBar.IsCancelling())
    {
        // RevitProgressBar Canceled
    }
}
```

### Methods and Configurations

The `RevitProgressBar` has some methods to configurations the progress bar behavior.

`SetCurrentOperation` - Set the current operation name.
```C#
revitProgressBar.SetCurrentOperation("CurrentOperation");
```

`SetCurrentValue` - Set the current value of the progress bar.
```C#
revitProgressBar.SetCurrentValue(0);
```

`SetMinimumValue` - Set the minimum value of the progress bar.
```C#
revitProgressBar.SetMinimumValue(0);
```

`SetMaximumValue` - Set the maximum value of the progress bar.
```C#
revitProgressBar.SetMaximumValue(100);
```

`SetIsIndeterminate` - Set the progress bar to indeterminated.
```C#
revitProgressBar.SetIsIndeterminate(true);
```

`SetHasCancelButton` - Set the cancel button to show.
```C#
revitProgressBar.SetHasCancelButton(true);
```

`Cancel` - Cancel the revit progress bar requested. (`Run` methods gonna be breaked.)
```C#
revitProgressBar.Cancel();
```

`IsCancelling` - Check if the cancel button was pressed.
```C#
if (revitProgressBar.IsCancelling()) { 
    // RevitProgressBar Canceled
}
```

`InitializeMilliseconds` - `RevitProgressBar` only appears after past this property value in milliseconds.
```C#
revitProgressBar.InitializeMilliseconds = 250;
```

`RefreshMilliseconds` - `RevitProgressBar` only updates the progress bar after this property value in milliseconds.
```C#
revitProgressBar.RefreshMilliseconds = 50;
```

## Revit Commands Example

There are some code examples in the [Commands](ricaun.Revit.UI.StatusBar/Revit/Commands).

### CommandRevit
```C#
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.StatusBar;

namespace RevitAddin.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandRevit : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            RevitProgressBarUtils.Run(uiapp.Application.VersionName, 100, (i) =>
            {
                System.Threading.Thread.Sleep(i);
            });

            return Result.Succeeded;
        }
    }
}
```

### CommandRevitCancel

This command has a simple implementation with a cancel button.

```C#
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.StatusBar;

namespace RevitAddin.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandRevitCancel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            using (var revitProgressBar = new RevitProgressBar(true))
            {
                revitProgressBar.Run(uiapp.Application.VersionName, 100, (i) =>
                {
                    System.Threading.Thread.Sleep(i);
                });

                if (revitProgressBar.IsCancelling())
                {
                    // RevitProgressBar Canceled
                }
            }

            return Result.Succeeded;
        }
    }
}
```

### CommandRevitCancelTransaction

This commmand copy the selected elements 100 times with the cancel button to rollback.

```C#
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ricaun.Revit.UI.StatusBar;

namespace RevitAddin.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandRevitCancelTransaction : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            var elementIds = selection.GetElementIds();

            if (elementIds.Count == 0)
            {
                TaskDialog.Show("Revit", "Select elements to copy.");
                return Result.Failed;
            }

            using (var revitProgressBar = new RevitProgressBar(true))
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Copy Elements");

                    revitProgressBar.Run("Copy Elements", 100, (i) =>
                    {
                        ElementTransformUtils.CopyElements(document, elementIds, XYZ.BasisX * (i + 1));
                    });

                    if (revitProgressBar.IsCancelling())
                        transaction.RollBack();
                    else
                        transaction.Commit();
                }
            }

            return Result.Succeeded;
        }
    }
}
```

## Utils
### BalloonUtils

The `BalloonUtils` class has some utility methods to show a balloon in Revit UI.
```C#
BalloonUtils.Show("Message", "Title/Category");
```

## Release

* [Latest release](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar/releases/latest)

## References

This project was inspired by [OptionsBar](https://github.com/atomatiq/OptionsBar).

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar/stargazers)!
