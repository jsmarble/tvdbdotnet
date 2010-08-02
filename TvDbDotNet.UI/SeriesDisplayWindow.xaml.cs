using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TvDbDotNet.UI
{
    /// <summary>
    /// Interaction logic for SeriesDisplayWindow.xaml
    /// </summary>
    public partial class SeriesDisplayWindow : Window
    {
        private TvDbSeries series;

        public SeriesDisplayWindow(TvDbSeries series)
        {
            this.series = series;
            InitializeComponent();
            this.searchResultsListBox.ItemsSource = new List<TvDbSeries> { series };
        }
    }
}
