using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace microMacro
{
    public class cls_Helper
    {
        public Category rvtCategory;
        public Dictionary<string, List<BuiltInCategory>> categories;

        // create a list of google categories
        public void CategoryComparison(cls_Image_Data imgData)
        {
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

        }
    }
}
