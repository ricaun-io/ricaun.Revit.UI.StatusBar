using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandTask : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                try
                {
                    RevitProgressBarUtils.Run("Theme", 25, (i) =>
                    {
                        Thread.Sleep(100);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            return Result.Succeeded;
        }
    }

}
