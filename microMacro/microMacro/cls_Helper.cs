using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Autodesk.Revit.DB;

namespace microMacro
{
    public static class cls_Helper
    {

        public static XYZ offset;

        public static void GetLocation(Document document, cls_Image_Data tbdDataSet)
        {
            // Get the SiteLocation instance.
            SiteLocation site = document.SiteLocation;

            // Angles are in radians when coming from Revit API, so we 
            // convert to degrees for display
            const double angleRatio = Math.PI / 180;   // angle conversion factor

            // Format the prompt information of basepoint. 
            String prompt = "Current project's Site location information:";
            prompt += "\n\t" + "Latitude: " + site.Latitude / angleRatio + "XX";
            prompt += "\n\t" + "Longitude: " + site.Longitude / angleRatio + "YY";
            prompt += "\n\t" + "TimeZone: " + site.TimeZone;

            // Format the prompt information of basepoint offset
            double x = tbdDataSet.Latitude - site.Latitude / angleRatio;
            double y = tbdDataSet.Longidude - site.Longitude / angleRatio * -1;

            //define offset values
            offset = new XYZ(x, y, 0);

            //Format the prompt for the offset distance from basepoint
            prompt += "\n\t" + offset.X + "\n\t" + offset.Y;

            // Give the user some information.
            Debug.Print("Revit", prompt);

        }

    }
}
