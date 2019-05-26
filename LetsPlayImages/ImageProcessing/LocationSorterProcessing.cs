using LetsPlayImages.DataExtractor;
using LetsPlayImages.FolderCreators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LetsPlayImages.ImageProcessing
{
    public class LocationSorterProcessing : ProcessingAlgorithm<IFolderCreator>
    {
        //looks like it work. test it on some foto with GPS. it's very КОРЯВЫЙ!!! code.....


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

                    using (ExifLib.ExifReader reader = new ExifLib.ExifReader(item)){ //lib for extracting data
                        double[] lat;
                        double[] lon;
                        if (reader.GetTagValue(ExifLib.ExifTags.GPSLatitude, out lat))
                        {
                            if (reader.GetTagValue(ExifLib.ExifTags.GPSLongitude, out lon))
                            {
                                string s = $"//geocode-maps.yandex.ru/1.x/?geocode={lon[0] + lon[1] / 60f + lon[2] / 3600f},{lat[0] + lat[1] / 60f + lat[2] / 3600f}&kind=locality&results=1"; //request string

                                WebRequest req = WebRequest.Create($"https:" + s);
                                
                                WebResponse resp = req.GetResponse();

                                using (Stream stream = resp.GetResponseStream())  //get response stream
                                {

                                    XmlDocument doc = new XmlDocument();          //i not found some lib's for convert xml to object so directly forehead...
                                    doc.Load(stream);
                                    
                                    XmlNodeList l = doc.GetElementsByTagName("Locality");

                                    string location = String.Empty;

                                    if (l.Count > 0)
                                    {
                                        location = l[0] != null ? l[0].InnerText : String.Empty;
                                    }

                                    if (!String.IsNullOrWhiteSpace(location))
                                    {
                                        if (!Directory.Exists(_destinationPath.FullName + "\\" + location))
                                        {
                                            Directory.CreateDirectory(Path.Combine(_destinationPath.FullName, location));

                                        }
                                        try
                                        {
                                            File.Copy(item, _destinationPath.FullName + "\\" + location + "\\" + Path.GetFileName(item), true);
                                        }
                                        catch (Exception ex)
                                        {
                                            string errs = ex.Message;

                                        }
                                    }

                                    resp.Close();
                                }

                            }


                        } 
                    }
                    
                    //double alt = Double.Parse( data[0]);
                    //double lon = Double.Parse(data[1]);
                }
            }

            Console.WriteLine();
            Console.WriteLine($"{CommandName} finished...");
            Console.WriteLine();
            
        }
    }
}
