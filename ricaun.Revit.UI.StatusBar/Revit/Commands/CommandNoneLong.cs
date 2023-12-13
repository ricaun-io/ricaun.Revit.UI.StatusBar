using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.StatusBar.Utils;

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
                if (revitProgressBar.IsCancelling()) BalloonUtils.Show("Cancel", "RevitProgressBar");
            }

            System.Console.WriteLine($"Stopwatch: {stopwatch.ElapsedMilliseconds}");

            return Result.Succeeded;
        }
    }

}
