using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Helper
    {
        public class GridSort
        {
            public string Field
            {
                get;
                set;
            }
            public string Dir
            {
                get;
                set;



            }
        }



        public static IEnumerable<T> MultiSort<T>(this IEnumerable<T> data,
       List<GridSort> gridsorts)
        {
            var sortExpressions = new List<Tuple<string,
                string>>();


            for (int i = 0; i < gridsorts.Count(); i++)
            {
                var fieldName = gridsorts[i].Field.Trim();
                var sortOrder = (gridsorts[i].Dir.Length > 1) ?
                    gridsorts[i].Dir.Trim().ToLower() : "desc";
                sortExpressions.Add(new Tuple<string, string>(fieldName, sortOrder));
            }
            // No sorting needed  
            if ((sortExpressions == null) || (sortExpressions.Count <= 0))
            {
                return data;
            }
            // Let us sort it  
            IEnumerable<T> query = from item in data select item;
            IOrderedEnumerable<T> orderedQuery = null;
            for (int i = 0; i < sortExpressions.Count; i++)
            {
                // We need to keep the loop index, not sure why it is altered by the Linq.  
                var index = i;


                Func<T, object> expression = item => item.GetType()
            .GetProperty(sortExpressions[index].Item1)
            .GetValue(item, null);

                if (sortExpressions[index].Item2 == "asc")
                {
                    orderedQuery = (index == 0) ? query.OrderBy(expression) :
                        orderedQuery.ThenBy(expression);
                }
                else
                {
                    orderedQuery = (index == 0) ? query.OrderByDescending(expression) :
                        orderedQuery.ThenByDescending(expression);
                }
            }
            query = orderedQuery;
            return query;

        }
    }

}
