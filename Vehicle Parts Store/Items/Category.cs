using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Parts_Store.HR;

namespace Vehicle_Parts_Store.Items
{
    public class Category<T> : Repository<T> where T : Part
    {
        public void ShowAscendingByPrice()
        {
            List<T> asc = repository.OrderByDescending(x => x.Price).ToList();
            for (int i = asc.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(asc[i] + "\n");
            }
        }

        public void ShowDescendingByPrice()
        {
            List<T> asc = repository.OrderByDescending(x => x.Price).ToList();
            foreach (T item in asc)
            {
                Console.WriteLine(item + "\n") ;
            }
        }
    }
}
