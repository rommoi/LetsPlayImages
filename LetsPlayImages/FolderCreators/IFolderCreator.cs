using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages.FolderCreators
{
    public interface IFolderCreator
    {
        (DirectoryInfo, bool) CreateFolder(string path, string add);           //implementation should create folder in specific place. if creation failed returns target (path) directory info
    }

    
}
