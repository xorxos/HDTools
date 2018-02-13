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
    }
}
