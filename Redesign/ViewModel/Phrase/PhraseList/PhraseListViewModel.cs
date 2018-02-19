using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Redesign
{
    /// <summary>
    /// View model for the overview phrase list
    /// </summary>
    public class PhraseListViewModel : PhraseListItemViewModel
    {
        /// <summary>
        /// The phrase list items for the list
        /// </summary>
        public List<PhraseListItemViewModel> _items;
        public List<PhraseListItemViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(nameof(_items)); }
        }

        /// <summary>
        /// The viewable list items
        /// </summary>
        public List<PhraseListItemViewModel> _viewedItems;
        public List<PhraseListItemViewModel> ViewedItems
        {
            get { return _viewedItems; }
            set { _viewedItems = value; OnPropertyChanged(nameof(_viewedItems)); }
        }
    }
}
