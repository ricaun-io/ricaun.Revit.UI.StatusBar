using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandNoneLong : IExternalCommand
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

                });
            }

            System.Console.WriteLine($"Stopwatch: {stopwatch.ElapsedMilliseconds}");

            return Result.Succeeded;
        }
    }

}
