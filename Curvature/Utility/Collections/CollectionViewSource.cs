using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Curvature
{
    [ImplementPropertyChanged]
    public class CollectionViewSource<T>
    {
        // ===========================================================================
        // = Public Properties
        // ===========================================================================

        public ObservableCollection<T> View { get; private set; }

        // ===========================================================================
        // = Private Fields
        // ===========================================================================

        private ObservableCollection<T> _originalCollection;
        private IList<SortDescription<T>> _sortDescriptions;
        private Func<T, Boolean> _filter;

        private Dictionary<T, Int32> _viewIndices;

        // ===========================================================================
        // = Construction
        // ===========================================================================

        public CollectionViewSource(ObservableCollection<T> inCollection, IList<SortDescription<T>> inSortDescriptions = null, Func<T, Boolean> inFilter = null)
        {
            _originalCollection = inCollection;
            _sortDescriptions = inSortDescriptions ?? new List<SortDescription<T>>();
            _filter = inFilter ?? new Func<T, Boolean>(X => true);

            View = new ObservableCollection<T>();
            _viewIndices = new Dictionary<T, Int32>();

            Refresh();

            _originalCollection.CollectionChanged += OnOriginalCollectionChanged;
        }

        // ===========================================================================
        // = Event Handling
        // ===========================================================================

        private void OnOriginalCollectionChanged(Object inSender, NotifyCollectionChangedEventArgs inArgs)
        {
            // TODO: Do a smaller refresh.
            Refresh();
        }

        // ===========================================================================
        // = Private Methods
        // ===========================================================================

        private void InsertInternal(T inObject, Int32 inIndex)
        {
            if (View.Count == inIndex)
                View.Add(inObject);
            else
                View.Insert(inIndex, inObject);

            foreach (var item in _viewIndices.Where(X => X.Value >= inIndex).ToList())
                _viewIndices[item.Key]++;

            _viewIndices.Add(inObject, inIndex);
        }

        private void DeleteInternal(T inObject)
        {
            var index = _viewIndices[inObject];
            View.RemoveAt(index);

            foreach (var item in _viewIndices.Where(X => X.Value >= index).ToList())
                _viewIndices[item.Key]--;

            _viewIndices.Remove(inObject);
        }

        // ===========================================================================
        // = Public Methods
        // ===========================================================================

        public void Refresh()
        {
            var newCollection = _originalCollection.Where(_filter);

            foreach (var sortDescription in _sortDescriptions)
                newCollection = sortDescription.ApplyTo(newCollection);

            var newCollectionRealised = newCollection.ToList();
            var newCollectionHashed = new HashSet<T>(newCollection);

            var itemsToRemove = View.Where(X => !newCollectionHashed.Contains(X)).ToList();

            foreach (var item in itemsToRemove)
                DeleteInternal(item);

            for (int i = 0; i < newCollectionRealised.Count; i++)
            {
                var item = newCollectionRealised[i];

                if (!_viewIndices.ContainsKey(item))
                    if (i == 0)
                        InsertInternal(item, 0);
                    else
                        InsertInternal(item, FindPositionFor(item, newCollectionRealised, i));
            }

            if (newCollectionRealised.Count != View.Count)
                throw new Exception("Internal error: collection mismatch.");

            for (int i = 0; i < newCollectionRealised.Count; i++)
                if (!Object.Equals(newCollectionRealised[i], View[i]))
                {
                    var item = newCollectionRealised[i];
                    DeleteInternal(item);
                    InsertInternal(item, i);
                }
        }

        private Int32 FindPositionFor(T inItem, List<T> inNewCollectionRealised, Int32 inIndex)
        {
            Int32 i;

            for (i = inIndex; i >= 0; i--)
            {
                var previousItem = inNewCollectionRealised[i];

                if (_viewIndices.ContainsKey(previousItem))
                    return _viewIndices[previousItem] + 1;
            }

            return 0;
        }
    }
}
