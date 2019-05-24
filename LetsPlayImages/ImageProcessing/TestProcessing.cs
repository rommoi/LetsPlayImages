using LetsPlayImages.FolderCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LetsPlayImages.ImageProcessing
{
    public class TestProcessing : ProcessingAlgorithm<IFolderCreator>
    {

        public TestProcessing(IFolderCreator creator) :
            base(creator)
        {

        }

        protected override void IsSuccessNotify()
        {
            
        }

        //protected override void FinishedNotify()
        //{
        //    Console.WriteLine("\nProcessing finished...");
        //}

        protected async override void SpecificProcessing()
        {
            Console.WriteLine("\nStart processing...");
            await Task.Run(() => Thread.Sleep(10000));
            //FinishedNotify();
            Console.WriteLine("\nProcessing finished...");
        }
    }
}
