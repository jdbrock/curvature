using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Curvature
{
    public class SortDescription<T>
    {
        public Func<T, Object> PropertySelector { get; private set; }
        public SortDirection Direction { get; private set; }

        public SortDescription(Func<T, Object> inPropertySelector, SortDirection inDirection = SortDirection.Ascending)
        {
            PropertySelector = inPropertySelector;
            Direction = inDirection;
        }

        public IOrderedEnumerable<T> ApplyTo(IEnumerable<T> inCollection)
        {
            if (inCollection is IOrderedEnumerable<T>)
            {
                var collection = (IOrderedEnumerable<T>)inCollection;

                if (Direction == SortDirection.Ascending)
                    return collection.ThenBy(PropertySelector);

                return collection.ThenByDescending(PropertySelector);
            }
            else
            {
                if (Direction == SortDirection.Ascending)
                    return inCollection.OrderBy(PropertySelector);

                return inCollection.OrderByDescending(PropertySelector);
            }
        }
    }
}
