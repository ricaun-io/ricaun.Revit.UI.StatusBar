using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            var elements = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfType<Element>()
                .ToList();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            RevitProgressBarUtils.Run("Revit Repeat", 20, (i) =>
            {
                RevitProgressBarUtils.Run($"Revit Elements {i}", elements, (element) =>
                {
                    //System.Console.WriteLine(element.Name);
                });
            });

            System.Console.WriteLine($"Stopwatch: {stopwatch.ElapsedMilliseconds}");

            return Result.Succeeded;
        }
    }
}
