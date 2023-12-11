using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
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
