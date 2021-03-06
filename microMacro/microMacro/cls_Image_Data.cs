﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Flux.Serialization;
using Autodesk.Revit.DB;
using Newtonsoft.Json;

namespace microMacro
{
    public class cls_Image_Data
    {

        #region PROPERTIES
        [JsonIgnore]
        private double longitude;

        [JsonIgnore]
        private double latitude;

        [JsonIgnore]
        private List<string> resultCatetgories;

        [JsonProperty("longitude")]
        public double Longidude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        [JsonProperty("latitude")]
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        [JsonProperty("categories")]
        public List<string> ResultCategories
        {
            get { return resultCatetgories; }
            set { resultCatetgories = value; }
        }

        [JsonIgnore]
        public Dictionary<string, List<BuiltInCategory>> categories;
        #endregion

        public cls_Image_Data()
        {
            ResultCategories = new List<string>();
        }

        public cls_Image_Data(double lon, double lat)
        {
            longitude = lon;
            latitude = lat;
            ResultCategories = new List<string>();
        }

        public cls_Image_Data(string lon, string lat, List<string> categories)
        {
            longitude = ConvertToDouble(lon);
            latitude = ConvertToDouble(lat);
            ResultCategories = new List<string>();
            ResultCategories = categories;
        }

        private double ConvertToDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            else
            {
                string[] splitVals = value.Split(':');
                double degrees = Convert.ToDouble(splitVals[0]);
                double minute = Convert.ToDouble(splitVals[1]);
                double second = Convert.ToDouble(splitVals[2]);
                double result = (degrees) + (minute) / 60 + (second) / 3600;
                return result;
            }
        }

        // create a list of google categories
        public void CategoryComparison(cls_Image_Data imgData)
        {
            #region CATEGORIES
            categories.Add("mechanical", new List<BuiltInCategory> { BuiltInCategory.OST_MechanicalEquipment });
            categories.Add("machine", new List<BuiltInCategory> { BuiltInCategory.OST_MechanicalEquipment });

            categories.Add("plumbing", new List<BuiltInCategory> { BuiltInCategory.OST_PlumbingFixtures });
            categories.Add("duct", new List<BuiltInCategory> { BuiltInCategory.OST_DuctCurves,
            BuiltInCategory.OST_DuctFitting,BuiltInCategory.OST_DuctSystem, BuiltInCategory.OST_DuctTerminal });

            categories.Add("light", new List<BuiltInCategory> { BuiltInCategory.OST_LightingDevices, BuiltInCategory.OST_LightingFixtures,
            BuiltInCategory.OST_Lights});
            categories.Add("lights", new List<BuiltInCategory> { BuiltInCategory.OST_LightingDevices, BuiltInCategory.OST_LightingFixtures,
            BuiltInCategory.OST_Lights});
            categories.Add("lighting", new List<BuiltInCategory> { BuiltInCategory.OST_LightingDevices, BuiltInCategory.OST_LightingFixtures,
            BuiltInCategory.OST_Lights});

            categories.Add("door", new List<BuiltInCategory> { BuiltInCategory.OST_Doors });
            categories.Add("window", new List<BuiltInCategory> { BuiltInCategory.OST_Windows });
            categories.Add("glass", new List<BuiltInCategory> { BuiltInCategory.OST_Windows });
            categories.Add("wall", new List<BuiltInCategory> { BuiltInCategory.OST_Walls });
            categories.Add("floor", new List<BuiltInCategory> { BuiltInCategory.OST_Floors });
            categories.Add("room", new List<BuiltInCategory> { BuiltInCategory.OST_Rooms });

            categories.Add("column", new List<BuiltInCategory> { BuiltInCategory.OST_StructuralColumns });
            categories.Add("structure", new List<BuiltInCategory> { BuiltInCategory.OST_StructuralColumns });
            categories.Add("beam", new List<BuiltInCategory> { BuiltInCategory.OST_StructuralFraming, BuiltInCategory.OST_StructuralFramingSystem });

            categories.Add("chair", new List<BuiltInCategory> { BuiltInCategory.OST_Furniture });
            categories.Add("table", new List<BuiltInCategory> { BuiltInCategory.OST_Furniture });
            categories.Add("sofa", new List<BuiltInCategory> { BuiltInCategory.OST_Furniture });
            #endregion

            List<Element> elements = new List<Element>();

            for (int i = 0; i < imgData.resultCatetgories.Count; i++)
            {
                List<BuiltInCategory> tempCategories = categories[imgData.resultCatetgories[i]];
                
                for (int j = 0; j < tempCategories.Count; i++)
                {
                    var stuff = from tempEle in 
                }
            }
        }
    }
}
