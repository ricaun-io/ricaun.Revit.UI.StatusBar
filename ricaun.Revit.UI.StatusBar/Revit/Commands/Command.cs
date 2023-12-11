using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.SetMaximumValue(100000);
                for (int i = 0; i <= 100000; i++)
                {
                    revitProgressBar.Increment();
                }
                revitProgressBar.SetCurrentOperation("Run").Run(100000, i =>
                {

                });
                revitProgressBar.SetCurrentOperation("Range").Run(Enumerable.Range(0, 100000), i =>
                {

                });
            }
            Console.WriteLine($"Stopwatch: {stopwatch.ElapsedMilliseconds}");

            return Result.Succeeded;
        }
    }
}
