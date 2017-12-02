using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microMacro
{
    class cls_Image_Data
    {

        #region PROPERTIES
        private double longitude;
        private double latitude;
        private List<string> resultCatetgories;

        public double Longidude{
            get{ return longitude; }
            set{ longitude = value; }
            }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public List<string> ResultCategories
        {
            get { return ResultCategories; }
            set { resultCatetgories = value; }
        }
        #endregion

        cls_Image_Data()
        {
            ResultCategories = new List<string>();
        }

    }
}
