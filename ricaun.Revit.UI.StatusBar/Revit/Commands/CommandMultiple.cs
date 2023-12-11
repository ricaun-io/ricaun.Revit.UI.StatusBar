using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Threading;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandMultiple : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            using (new RevitProgressBar(true).Run(100000, (i) =>
            {
                if (i == 50000)
                {
                    using (new RevitProgressBar(true).SetCurrentOperation("Internal").Run(100000, (i) =>
                    {

                    })) { }
                }
                if (i == 90000)
                {
                    using (new RevitProgressBar(true).SetCurrentOperation("Internal 2").Run(100000, (i) =>
                    {

                    })) { }
                }
            })) { }

            return Result.Succeeded;
        }
    }


    [Transaction(TransactionMode.Manual)]
    public class CommandDelay : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.Run("Run 10", 10, (i) =>
                {
                    Thread.Sleep(100);
                });
                revitProgressBar.Run("Running 10", 10, (i) =>
                {
                    Thread.Sleep(100);
                });
                revitProgressBar.SetIsIndeterminate(true).Run("Indeterminate", 30, (i) =>
                {
                    Thread.Sleep(100);
                });
            }

            return Result.Succeeded;
        }
    }

}
