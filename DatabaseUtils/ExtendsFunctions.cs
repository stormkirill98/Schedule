using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class ExtendsFunctions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            items.ToList().ForEach(item =>
            {
                if (!collection.Contains(item))
                {
                    collection.Add(item);
                }
            });
        }
    }
}
