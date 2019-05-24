using LetsPlayImages.FolderCreators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlayImages.DataExtractor;

namespace LetsPlayImages.ImageProcessing
{
    public class AddDateTimeMarkProcessing : ProcessingAlgorithm<IFolderCreator>
    {
        protected override string CommandName => "AddDateTimeMark";

        public AddDateTimeMarkProcessing(IFolderCreator creator)
            : base(creator)
        {
            
        }

        protected override void IsSuccessNotify()
        {
            
        }

        protected override void SpecificProcessing()
        {
            IExtractor<Image> extractor = ExtractorFactory.CreateExtractor("datetime");
            foreach (var item in _filesInfo)
            {


                using (FileStream readfs = new FileStream(item, FileMode.Open)) //filestream to read image
                {
                    using (Image img = Image.FromStream(readfs))
                    {
                        
                        using (Graphics grph = Graphics.FromImage(img))
                        using (Font font = new Font(new FontFamily("Arial"), (float)(0.015*img.Height)))
                        using (SolidBrush sbrush = new SolidBrush(Color.Black))    //using graphical tools
                        {
                            
                            //need to add verifying of min image size and correct label font size and area
                            RectangleF rect = new RectangleF(new PointF((int)(img.Width - 385), (int)(img.Height * 0.001)), new SizeF(385, (int)(img.Height * .02)));  //put a rectangle in top right corner of the image
                            grph.FillRectangle(Brushes.White, rect);        //fill rectangle with white color because label will be black

                            string res = extractor.Extract(img, 0x9003);   //get information from image
                            grph.DrawString(res, font, sbrush, rect);                                         //draw label on the image
                        }

                        
                        using (FileStream writefs = new FileStream(_destinationPath.FullName + "\\" + Path.GetFileName(item), FileMode.OpenOrCreate))
                        {
                            ImageFormat imgFormat = ImageFormat.Bmp;
                            switch (Path.GetFileName(item))
                            {
                                case ".jpg":
                                    imgFormat = ImageFormat.Jpeg;
                                    break;
                                case ".jpeg":
                                    imgFormat = ImageFormat.Jpeg;
                                    break;
                                case ".png":
                                    imgFormat = ImageFormat.Png;
                                    break;
                                case ".gif":
                                    imgFormat = ImageFormat.Gif;
                                    break;
                            }
                            img.Save(writefs, imgFormat);  //save image in new folder
                        }
                    }
                }
            
            }
            Console.WriteLine();
            Console.WriteLine("ADD LABEL FINISHED!!!!");
            Console.WriteLine();
        }
    }
}
