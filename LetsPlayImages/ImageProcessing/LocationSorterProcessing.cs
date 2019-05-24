using LetsPlayImages.DataExtractor;
using LetsPlayImages.FolderCreators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages.ImageProcessing
{
    public class LocationSorterProcessing : ProcessingAlgorithm<IFolderCreator>
    {
        protected override string CommandName => "SortByPlaces";

        public LocationSorterProcessing(IFolderCreator creator)
            : base(creator)
        {

        }

        protected override void IsSuccessNotify()
        {
            
        }

        protected override void SpecificProcessing()
        {
            IExtractor<Image> extractor = ExtractorFactory.CreateExtractor("coordinates");
            foreach (var item in _filesInfo)
            {
                using (Image img = Image.FromFile(item))
                {
                    //var data = extractor.Extract(img, 0).Split(' ');

                    ExifLib.ExifReader reader = new ExifLib.ExifReader(item);
                    double lat;
                        reader.GetTagValue(ExifLib.ExifTags.GPSLatitude, out lat);
                    double lon;
                    reader.GetTagValue(ExifLib.ExifTags.GPSLongitude, out lon);

                    //double alt = Double.Parse( data[0]);
                    //double lon = Double.Parse(data[1]);
                }
            }
            
        }
    }
}
