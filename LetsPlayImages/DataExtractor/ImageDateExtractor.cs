using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages.DataExtractor
{
    public class ImageDateExtractor : IExtractor<Image>
    {
        public string Extract(Image item, int propID)
        {
            string res = string.Empty;
            try
            {
                if (item.PropertyItems.Any(x => x.Id == propID))
                {
                    PropertyItem propi = item.GetPropertyItem(propID);
                    return Encoding.UTF8.GetString(propi.Value); 
                }
            }catch(Exception ex)
            {
                string msg = ex.Message;
            }
            
            return res;
        }

        
    }
    public class ImageCoordinatesExtractor : IExtractor<Image>
    {
        public string Extract(Image item, int dataID)
        {
            string res = string.Empty;

            PropertyItem val;

            try
            {
                
                //if (item.PropertyItems.Any(x => x.Id == 0x0004))
                //{
                //    if (item.PropertyItems.Any(x => x.Id == 0x0006))
                //    {
               
                        PropertyItem propAlt = item.GetPropertyItem(0x0002);
                        PropertyItem propLon = item.GetPropertyItem(0x0004);

                        return "" + propAlt.Value + " " + propLon.Value;
                //    }
                    
                //}

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return res;
        }
    }
}
