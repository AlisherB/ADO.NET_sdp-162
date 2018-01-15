using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interface
{
    public interface IDAO<T> where T : class
    {
        string Create(T record);
        string Update(T record);
        void Delete(int id);
        T Read(int id);
        ICollection<T> Read();
    }
}
