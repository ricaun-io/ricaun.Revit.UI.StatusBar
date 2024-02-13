using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace ricaun.Revit.UI.StatusBar.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandCopy : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            var elements = selection.GetElementIds().Select(id => document.GetElement(id));

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Copy Elements");

                if (elements.Any())
                {
                    RevitProgressBarUtils.Run("Copy Elements", 100, (i) =>
                    {
                        ElementTransformUtils.CopyElements(document, elements.Select(e => e.Id).ToList(), XYZ.BasisX * (i + 1));
                    });
                }

                transaction.Commit();
            }

            return Result.Succeeded;
        }
    }

}
