using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.UI.StatusBar.Utils;
using System;

namespace ricaun.Revit.UI.StatusBar.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("StatusBar");
            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<Commands.CommandRevit>("Revit"),
                ribbonPanel.CreatePushButton<Commands.CommandNone>("None"),
                ribbonPanel.CreatePushButton<Commands.CommandNoneLong>("NoneLong"),
                ribbonPanel.CreatePushButton<Commands.CommandTheme>("Theme"),
                ribbonPanel.CreatePushButton<Commands.CommandDelayIndeterminate>("Delay"),
                ribbonPanel.CreatePushButton<Commands.CommandElements>("Elements"),
                ribbonPanel.CreatePushButton<Commands.CommandMultiple>("Multiple"),
                ribbonPanel.CreatePushButton<Commands.CommandView>("View")
                );

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}
