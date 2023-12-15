using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Threading;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandTheme : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIThemeManager.CurrentTheme = UIThemeManager.CurrentTheme == UITheme.Light ? UITheme.Dark : UITheme.Light;
            Thread.Sleep(100);

            RevitProgressBarUtils.Run("Theme", 25, (i) =>
            {
                Thread.Sleep(100);
            });

            return Result.Succeeded;
        }
    }
}
