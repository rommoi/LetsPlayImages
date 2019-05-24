using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlayImages.DataExtractor;

namespace LetsPlayImages.DataExtractor
{
    public static class ExtractorFactory
    {
        public static IExtractor<Image> CreateExtractor(string name)
        {
            switch (name)
            {
                case "datetime":
                    return new ImageDateExtractor();
                    
                case "coordinates":
                    return new ImageCoordinatesExtractor();
                    
                default:
                    return null;
                    
            }
        }
    }
}
