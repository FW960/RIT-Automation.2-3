using System.Collections.Generic;
using Task._1.Entities;

namespace Task._1.Db
{
    public interface IDatabase
    {
        List<MyMarker> GetAll();

        void Update(MyMarker marker);
    }
}