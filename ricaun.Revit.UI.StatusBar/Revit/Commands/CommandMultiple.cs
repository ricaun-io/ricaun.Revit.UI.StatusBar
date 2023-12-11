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

            using (new RevitProgressBar(true).Run(300000, (i) =>
            {
                if (i == 180000)
                {
                    using (new RevitProgressBar(true).SetCurrentOperation("Internal").Run(100000, (i) =>
                    {

                    })) { }
                }
                if (i == 190000)
                {
                    using (new RevitProgressBar(true).SetCurrentOperation("Internal 2").Run(100000, (i) =>
                    {

                    })) { }
                }
            })) { }

            return Result.Succeeded;
        }
    }

}
