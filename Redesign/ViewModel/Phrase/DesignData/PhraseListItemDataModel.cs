namespace Redesign
{

    /// <summary>
    /// Data for a <see cref="PhraseListItemViewModel"/>
    /// </summary>
    class PhraseListItemDataModel : PhraseListItemViewModel
    {

        /// <summary>
        /// A single instance of the data model
        /// </summary>
        #region Singleton
        public static PhraseListItemDataModel Instance => new PhraseListItemDataModel();
        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PhraseListItemDataModel()
        {
            Abbreviation = "#IFSACC";
            Description = "How to request access to IFS and the link to form";
        }

        #endregion
    }
}
