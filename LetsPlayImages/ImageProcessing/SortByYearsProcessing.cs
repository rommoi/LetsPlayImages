using LetsPlayImages.DataExtractor;
using LetsPlayImages.FolderCreators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages.ImageProcessing
{
    public class SortByYearsProcessing : ProcessingAlgorithm<IFolderCreator>
    {
        protected override string CommandName => "SortByYear";

        protected override void IsSuccessNotify()
        {
            
        }

        protected override void SpecificProcessing()
        {
            IExtractor<Image> extractor = ExtractorFactory.CreateExtractor("datetime");

            DateTime dtextr;

            foreach (var item in _filesInfo)
            {
                using(FileStream fs = new FileStream(item, FileMode.Open))
                {
                    using(Image img = Image.FromStream(fs))
                    {
                        string data = extractor.Extract(img, 0x9003).Split(' ')[0].Replace(':', '/');   //extracting datetime get from there year/month/day and parse it to dateTime and get year
                        
                        DateTime.TryParse(data, out dtextr);
                        
                        if(dtextr == DateTime.MinValue)
                        {
                            dtextr = File.GetCreationTime(item);
                        }
                        
                        if (!Directory.Exists(_destinationPath.FullName + "\\" + dtextr.Year))
                        {
                            Directory.CreateDirectory(Path.Combine(_destinationPath.FullName, dtextr.Year + ""));
                            
                        }
                        try
                        {
                            File.Copy(item, _destinationPath.FullName +  "\\" + dtextr.Year + "\\" + Path.GetFileName(item), true);
                        }
                        catch (Exception ex)
                        {
                            string s = ex.Message;
                            
                        }
                        //File.Copy(item, _destinationPath.FullName + "\\" + dtextr.Year + "\\" + Path.GetFileName(item), true);
                        
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("SORT BY YEARS FINISHED!!!!");
            Console.WriteLine();
        }

        public SortByYearsProcessing(IFolderCreator creator)
            : base(creator)
        {

        }
    }
}
