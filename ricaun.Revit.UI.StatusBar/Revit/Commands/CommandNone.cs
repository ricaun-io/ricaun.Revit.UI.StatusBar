using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandNone : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.SetCurrentOperation(uiapp.Application.VersionName);
                revitProgressBar.SetHasCancelButton(true);
                revitProgressBar.Run(1000000, (i) =>
                {
                    //revitProgressBar.SetIsIndeterminate(false);
                    //revitProgressBar.SetCurrentOperation($"{uiapp.Application.VersionName} [{i}]");
                });
            }

            System.Console.WriteLine($"Stopwatch: {stopwatch.ElapsedMilliseconds}");

            return Result.Succeeded;
        }
    }

}
