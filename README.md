# ricaun.Revit.UI.StatusBar

[![Revit 2019](https://img.shields.io/badge/Revit-2019+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

Package to create a progress bar in Revit StatusBar for Revit API developers.

![ProgressBar](https://github.com/ricaun-io/ricaun.Revit.UI.StatusBar/assets/12437519/2d1642f8-f4fb-4fa8-8e9b-980d01127100)

This project was generated by the [ricaun.AppLoader](https://ricaun.com/AppLoader/) Revit plugin.

## RevitProgressBarUtils

The `RevitProgressBarUtils` has some utility methods to Run a loop and update the `RevitProgressBar`.

```C#
RevitProgressBarUtils.Run("Revit Elements", elements, (element) =>
{
    System.Console.WriteLine(element.Name);
});
```

## RevitProgressBar

The `RevitProgressBar` class implements the `IDisposable` interface, so it can be used in a `using` statement.

```C#
using (var progressBar = new RevitProgressBar())
{
	progressBar.Run("Revit Elements", elements, (element) =>
	{
		System.Console.WriteLine(element.Name);
	}
}
```

or...

```C#
using (var progressBar = new RevitProgressBar())
{
	progressBar.SetCurrentOperation("Revit Elements");
	foreach (var element in elements)
	{
		progressBar.Increment();
		System.Console.WriteLine(element.Name);
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

* [Latest release](../../releases/latest)

## References

This project was inspired by [OptionsBar](https://github.com/atomatiq/OptionsBar).

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!
