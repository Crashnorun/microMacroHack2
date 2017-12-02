#region ---- USING STATEMENTS ----
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
#endregion


namespace microMacro
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class cmd : IExternalCommand
    {

        #region ---- GLOBAL VARIABLES ----
        public Autodesk.Revit.ApplicationServices.Application RvtApp;
        public UIApplication RvtUiApp;
        public Document RvtDoc;
        public UIDocument RvtUiDoc;
        #endregion

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RvtUiApp = commandData.Application;
            RvtApp = RvtUiApp.Application;
            RvtUiDoc = RvtUiApp.ActiveUIDocument;
            RvtDoc = RvtUiDoc.Document;

            /*
             * Get the data from Flux
             * deseralize data to cls image data
             * do a search for revit categories based on image data
             * open 3d view
             * get project location
             * get image geo location 
             * translate geo location to xy coordinates
             * find the location of the revit elements by xy coordinates
             * get element parameters
             * change element parameter value  
             */

            Open3DView();
            MessageBox.Show("Hello World!");
            Debug.Print("Hello World!");
            return Result.Succeeded;
        }

        
        /// <summary>
        /// Open a default 3d view
        /// <reference>https://forums.autodesk.com/t5/revit-api-forum/generate-3d-view-programmatically/td-p/5808509</reference>
        /// </summary>
        public void Open3DView()
        {
            RevitCommandId commandId = RevitCommandId.LookupPostableCommandId(PostableCommand.Default3DView);

            if (RvtUiApp.CanPostCommand(commandId))
            {
                RvtUiApp.PostCommand(commandId);
            }
        }


        
    }       // ---- CLOSE CLASS ----
}           // ---- CLOSE NAMESPACE ----
