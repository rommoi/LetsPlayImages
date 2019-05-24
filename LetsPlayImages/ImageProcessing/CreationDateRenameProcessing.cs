using LetsPlayImages.FolderCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Threading;
using LetsPlayImages.DataExtractor;

namespace LetsPlayImages.ImageProcessing
{
    public class CreationDateRenameProcessing : ProcessingAlgorithm<IFolderCreator>
    {

        protected override string CommandName { get { return "RenameWithCreationDateTime"; } }

        public CreationDateRenameProcessing(IFolderCreator creator)
            : base(creator)
        {

        }

        protected override void IsSuccessNotify()
        {
            
        }

        //protected override void FinishedNotify()
        //{
            
        //}

        protected override void SpecificProcessing()
        {
            IExtractor<Image> extractor = ExtractorFactory.CreateExtractor("datetime");
            foreach (var item in _filesInfo)
            {
                try
                {
                    using (Image img = Image.FromFile(item))
                    {
                        string creationTime = extractor.Extract(img, 0x9003).Replace("\0", string.Empty).Replace('/', '_').Replace(':', '_');// GetImageDate(img);
                        
                        File.Copy(item, _destinationPath.FullName + "\\" + Path.GetFileNameWithoutExtension(item) + "_"
                                + (string.IsNullOrWhiteSpace(creationTime) ? (File.GetCreationTime(item).ToString().Replace(':', '_').Replace('/', '_')) : creationTime)
                                + Path.GetExtension(item), true);
                        
                    }
                }
                catch(OutOfMemoryException ex)
                {
                    string msg = ex.Message;
                }
                catch(FileNotFoundException ex)
                {
                    string msg = ex.Message;
                }
                catch(ArgumentException ex)
                {
                    string msg = ex.Message;
                }
                //Thread.Sleep();

            }
            Console.WriteLine();
            Console.WriteLine("RENAME DATETIME FINISHED!!!!");
            Console.WriteLine();
        }

        protected string GetImageDate(Image img)
        {
            try
            {
                
                if (img.PropertyItems.Any(s => s.Id == 0x9003)) //this property contains datetime of the image
                {
                    PropertyItem pi = img.GetPropertyItem(0x9003); //get datetime from image
                    return Encoding.UTF8.GetString(pi.Value).Replace("\0", string.Empty).Replace('/', '_').Replace(':', '_');
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return string.Empty;
            }


        }
    }

    public class Console_CreationDateRenameProcessing : CreationDateRenameProcessing
    {

        public Console_CreationDateRenameProcessing(IFolderCreator creator)
            : base(creator)
        {
            
        }

        protected override void SpecificProcessing()
        {
            Console.WriteLine($"Start processing by {base.CommandName}...");
            base.SpecificProcessing();
            Console.WriteLine(Thread.CurrentThread.ThreadState);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine($"{base.CommandName} finished processing...");
        }

    }
}
