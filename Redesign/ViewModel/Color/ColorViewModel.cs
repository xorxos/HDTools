using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Redesign
{
    class ColorViewModel : BaseViewModel
    {
        private Color standardColor;
        private Color highlightColor;

        /// <summary>
        /// A single instance of the data model
        /// </summary>
        #region Singleton
        private static ColorViewModel _instance;
        public static ColorViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ColorViewModel();
                }
                return _instance;
            }
        }
        #endregion

        public ColorViewModel()
        {
            this.StandardColor = Colors.Black;
            this.HighlightColor = Colors.Transparent;
        }

        public Color StandardColor
        {
            get { return this.standardColor; }

            set
            {
                this.standardColor = value;
                this.OnPropertyChanged(nameof(this.StandardColor));
            }
        }

        public Color HighlightColor
        {
            get { return this.highlightColor; }

            set
            {
                this.highlightColor = value;
                this.OnPropertyChanged(nameof(this.HighlightColor));
            }
        }
    }
}
