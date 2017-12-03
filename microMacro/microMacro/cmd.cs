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
using Flux.SDK;
using Flux.SDK.Types;
using Flux.SDK.DataTableAPI.DatatableTypes;
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

        public FluxSDK SDK;
        public List<string> ProjectNames;                                   // list of project names
        public Project FluxProject;                                         // selected Flux project
        public List<Project> FluxProjects;                                  // list of Flux projects
        public List<Flux.SDK.DataTableAPI.CellInfo> FluxDataKeys;           // List of Data Keys

        public cls_Image_Data imgData;
        #endregion

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // initialize revit variables
            RvtUiApp = commandData.Application;
            RvtApp = RvtUiApp.Application;
            RvtUiDoc = RvtUiApp.ActiveUIDocument;
            RvtDoc = RvtUiDoc.Document;

            // initilaize flux variables
            ProjectNames = new List<string>();
            FluxProjects = new List<Project>();
            FluxDataKeys = new List<Flux.SDK.DataTableAPI.CellInfo>();

            imgData = new cls_Image_Data();

            /*
             * ---open 3d view
             * ---Log into flux
             * ---get project location
             * ---get image geo location
             * translate geo location to xy coordinates
             * ---Get the data from Flux
             * ---deseralize data to cls image data
             * do a search for revit categories based on image data
             * get the revit categories from cls image data
             * find the location of the revit elements by xy coordinates
             * get element parameters
             * change element parameter value  
             */

            Open3DView();                               // open a default 3d view
            imgData = new cls_Image_Data("34:34:34", "34:34:34", new List<string> { "charlie", "bob" });
            string temp = Newtonsoft.Json.JsonConvert.SerializeObject(imgData);
            FluxLogin();                                // log into flux

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

        #region FLUX STUFF
        public void FluxLogin()
        {
            SDK = new FluxSDK("8431c3aa-d216-4bdc-a0eb-93e40e5951f8");      // set SDK to the Client ID
            SDK.OnUserLogin += SDK_OnUserLogin;                             // login event
            SDK.OnUserLogout += SDK_UserLogout;                             // logout event

            if (SDK.CurrentUser == null)
            {
                try
                {
                    // set client secret, and URL for the site that appears after login
                    SDK.Login("a6cfc8a6-41e2-4eb2-8311-f8256a2cea33", "https://flux.io/");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message.ToString());
                }
            }
            // if a user is loggedin, then log them out.
            else if (SDK.CurrentUser != null)
            {
                // SDK.Logout();
            }
        }

        /// <summary>
        /// Flux Login Event
        /// </summary>
        /// <param name="user"></param>
        private void SDK_OnUserLogin(User user)
        {
            Debug.Print("User Name: " + SDK.CurrentUser.FullName + "   |   Email: " + SDK.CurrentUser.Email);
            GetDataFromFlux("microMACRO");              // get the data from the data key
            cls_Helper.GetLocation(RvtDoc, imgData);
            
            // look up revit elements
        }

        /// <summary>
        /// Flux logout event
        /// </summary>
        private void SDK_UserLogout()
        {
            Debug.Print("USER LOGGED OUT");
        }
        #endregion

        /// <summary>
        /// Get the data from Flux
        /// </summary>
        /// <param name="projectName"></param>
        public void GetDataFromFlux(string projectName)
        {
            // get list of flux projects
            FluxProjects = SDK.CurrentUser.Projects;                        // get list of projects

            var tempNames = from project in FluxProjects
                            where !string.IsNullOrEmpty(project.Name)
                            select project.Name;                            // get project names

            ProjectNames = tempNames.ToList();

            // get flux project
            int index = ProjectNames.FindIndex(x => x == projectName);       // find the project index
            FluxProject = FluxProjects[index];                              // get the Flux Project

            // get list of data keys
            FluxDataKeys = FluxProject.DataTable.Cells;                     // get data keys

            // get data key
            Cell cell = FluxProject.DataTable.GetCell(FluxDataKeys[0].CellId);              // get the cell

            // get data from flux
            CellValue value = cell.Value;
            string tempString = Flux.Serialization.DataSerializer.Serialize(imgData);
            
            string CellValue = Flux.SDK.StreamUtils.GetStringFromStream(value.Stream);          // stream the vall value
            imgData = Flux.Serialization.DataSerializer.Deserialize<cls_Image_Data>(CellValue);
        }

    }       // ---- CLOSE CLASS ----
}           // ---- CLOSE NAMESPACE ----
