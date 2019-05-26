using LetsPlayImages.FolderCreators;
using LetsPlayImages.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages
{
    class Program
    {
        static void Main(string[] args)
        {
            //two lines below just for test web request
            ProcessingAlgorithm<IFolderCreator> pa = new LocationSorterProcessing(new FolderCommandName());
            pa.Process(@"C:\Users\Roma\Desktop\Foto GPS");


            while (true)
            {
                Console.Clear();
                PrintActions();
                
                switch (Console.ReadLine().ToLower())
                {
                    case "rename":
                        Console.WriteLine("Enter folder path:");
                        new CreationDateRenameProcessing(new FolderCommandName()).Process(Console.ReadLine());
                        break;
                    case "add label":
                        Console.WriteLine("Enter folder path:");
                        new AddDateTimeMarkProcessing(new FolderCommandName()).Process(Console.ReadLine());
                        break;
                    case "year sort":
                        Console.WriteLine("Enter folder path:");
                        new SortByYearsProcessing(new FolderCommandName()).Process(Console.ReadLine());
                        break;
                    case "places sort":
                        Console.WriteLine("Enter folder path:");
                        new LocationSorterProcessing(new FolderCommandName()).Process(Console.ReadLine());
                        break;
                    case "quit":
                        return;
                    default:
                        Console.WriteLine("Unknown command...");
                        break;
                }
            }
            
        }
        static void say(string s)
        {
            if (s.Contains("start")) new TestProcessing(new FolderCommandName()).Process(string.Empty);
            Console.WriteLine("Hello! " + s);
        }

        static void PrintActions()
        {
            Console.WriteLine("Available Actions:");
            Console.WriteLine("Rename images, add creation date. Enter \"rename\"");
            Console.WriteLine("Add creation date mark: place date to image. Enter \"add label\"");
            Console.WriteLine("Sort images by years: Enter \"year sort\"");
            Console.WriteLine("Sort by places. Enter \"plaeces sort\"");
            Console.WriteLine("Quit. Enter \"quit\"");
            Console.WriteLine("Input Next command:");
        }
    }
}
