using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlayImages.FolderCreators;

namespace LetsPlayImages.ImageProcessing
{

    //I know that all off this i can do much simplier, but just wants to work with some patterns like template method 
    //console output in operations need cut and use something else, maybe events...


    public abstract class ProcessingAlgorithm<T> where T : IFolderCreator //instead off folder we can save result on server or somewhere else...i don't know...just for use generics :)
    {
        //this is base class that define algorithm. specific steps should be defined by descendents. It defines template method.

        T _creator;                                //interface field creator will create folder with some specific(add new name, datetime etc)

        protected virtual string CommandName { get { return string.Empty; } }  //property that just holds name of command

        string _targetPath;  //this path will set by user if its correct

        protected DirectoryInfo _destinationPath;  //this is path to the folder created by _creator and where we'll save our processed images

        protected List<string> _filesInfo;   //list for images paths

        public ProcessingAlgorithm(T creator) //base ctor
        {
            _creator = creator;


        }

        public async void Process(string path) //this is template method. it defines some step of algorithm. specific steps should be overriden by descendents
        {
            //check folder path and save it in _targetPath
            if (VerifyFolderPath(path))
            {
                //create new folder with name: target folder + command(class) name. save it in _destinationPath
                if (CreateFolder())
                {
                    //get all files by specified path and save information about them in filesInfo
                    //possibly here we can inject method that can get some selected part of collection
                    _filesInfo = GetFiles();

                    //run specific processing (copy with datetime, add datetime mark, sort by datetime.year, sort by gps coordinates etc.)
                    await Task.Run(() => {
                        SpecificProcessing();
                    });
                    
                    //notify that task completed

                }
                else
                {
                    //here we can notify that folder creation process failed
                }
                //IsSuccessNotify();
                return;
            }
            else
            {
                //notify about wrong path
            }
            
        }

        bool VerifyFolderPath(string path)
        {
            //this method checks is directory exists or not and return
            if (Directory.Exists(path))
            {
                _targetPath = path;
                return true;
            }
            return false;
        }

        bool CreateFolder()
        {
            var res = _creator.CreateFolder(_targetPath, CommandName);
            if (res.Item2)
            {
                _destinationPath = _creator.CreateFolder(_targetPath, CommandName).Item1;
            }
            return res.Item2;
        }

        List<string> GetFiles()
        {
            //get all images from target place and save info in list
            List<string> list = Directory.GetFiles(_targetPath, "*.*", SearchOption.TopDirectoryOnly)
                     .Where(s => new string[] { ".jpeg", ".jpg", ".png", ".bmp", ".gif", ".tiff" }
                     .Contains(Path.GetExtension(s))).ToList();
            //maybe here we can insert some additional verifying logic
            return list;
        }

        protected abstract void SpecificProcessing();

        protected abstract void IsSuccessNotify(); //if folder is not exists this method will notify user and return from algorithm maybe here we can rise event
        //protected abstract void FinishedNotify();  //this method will notify user when processing will be finished, maybe in derived class call there event and use it inside SpecificProcessing

    }
}
