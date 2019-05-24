using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayImages.DataExtractor
{
    public interface IExtractor<T>
    {
        string Extract(T item, int dataID);
    }



}
