using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Redesign
{
    /// <summary>
    /// View model for each phrase list item in the overview phrase list
    /// </summary>
    public class PhraseListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The abbreviation for this phrase
        /// </summary>
        private string abbreviation;
        public string Abbreviation
        {
            get { return abbreviation; }
            set { abbreviation = value; OnPropertyChanged(nameof(abbreviation)); }
        }

        /// <summary>
        /// The description for this phrase
        /// </summary>
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(description)); }
        }

        /// <summary>
        /// The content for the richtextbox
        /// </summary>
        private string content = "";
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(nameof(content)); }
        }

        /// <summary>
        /// True if this item is currently selected
        /// </summary>
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(nameof(isSelected)); }
        }

        #region Commands
        /// <summary>
        /// The command to select the clicked item
        /// </summary>
        public ICommand SelectCommand { get; set; }
        #endregion


        /// <summary>
        /// The constructor for the PhraseListItemViewModel
        /// </summary>
        #region Constructor
        public PhraseListItemViewModel()
        {
            SelectCommand = new RelayCommand(() => SelectItem());
        }
        #endregion


        /// <summary>
        /// The methods needed for PhraseListItemViewModel
        /// </summary>
        #region Methods
        public void SelectItem()
        {
            PhraseListDataModel list = PhraseListDataModel.Instance;
            list.SetItemTrue(Abbreviation);
        }
        #endregion
    }
}
