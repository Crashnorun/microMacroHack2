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
        public static XYZ locationPoint;
        
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

        public static void GetElementLocation(Element rvtElement)
        {
            Location loc = rvtElement.Location;
            if (loc.GetType() == typeof(LocationCurve))
            {
                LocationCurve locCrv = (LocationCurve)loc;
                locationPoint = new XYZ(((locCrv.Curve.GetEndPoint(0).X + locCrv.Curve.GetEndPoint(1).X) / 2), ((locCrv.Curve.GetEndPoint(0).Y + locCrv.Curve.GetEndPoint(1).Y) / 2), ((locCrv.Curve.GetEndPoint(0).Z + locCrv.Curve.GetEndPoint(1).Z) / 2));
            }
            else if (loc.GetType() ==typeof( LocationPoint))
            {
                LocationPoint locpt = (LocationPoint)loc;
                locationPoint = new XYZ(locpt.Point.X, locpt.Point.Y, locpt.Point.Z);
            }
        }

        public static void ChangePhase(Element element)
        {
            //element.Parameters.p
        }

        
    }
}
