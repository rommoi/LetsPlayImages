using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages.FolderCreators
{
    public class FolderCommandName : IFolderCreator
    {
        public (DirectoryInfo, bool) CreateFolder(string path, string add)
        {
            //here we create new folder in target path with name: target folder name + CommandName
            try
            {
                if (!Directory.Exists(path + add))
                {
                    DirectoryInfo dinfo = Directory.CreateDirectory(path + " " + add);
                    return (dinfo, true);
                }
            }
            catch (Exception ex)
            {
                //here we can log what happens
                string msg = ex.Message;
            }
            return (new DirectoryInfo(path), false);
        }
    }
}
