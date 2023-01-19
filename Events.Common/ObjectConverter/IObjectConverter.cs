using System;
using System.Collections.Generic;
using System.Text;

namespace InterAsia.Common.ObjectConverter
{
    public interface IObjectConverter
    {
        T Deserialize<T>(string serializeData);

        string Serialize<T>(T entity);

        dynamic Deserialize(string deSerializeData, Type t);
    }
}
