using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
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
                    System.Console.WriteLine($"RevitProgressBar Canceled");
                }
            }

            return Result.Succeeded;
        }
    }

}
