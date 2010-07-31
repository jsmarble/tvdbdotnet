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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TvDbDotNet.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string API_KEY = "AFC397F804955A85"; //Only for use in TvDbDotNet UI application.
        private bool searching;
        private DispatcherTimer searchTimer;

        public MainWindow()
        {
            InitializeComponent();

            searchTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.ContextIdle, new EventHandler(searchTimer_Tick), Application.Current.Dispatcher);
            searchTimer.Stop();
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            SearchSeries(seriesSearchTextBox.Text);
        }

        private void seriesSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTimer != null)
            {
                searchTimer.Stop();
                string searchText = seriesSearchTextBox.Text;
                if (string.IsNullOrEmpty(searchText) || searchText == "Search ...")
                {
                    if (searchResultsListBox != null)
                        searchResultsListBox.ItemsSource = null;
                }
                else
                    searchTimer.Start();
            }
        }

        private void seriesSearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(seriesSearchTextBox.Text))
                seriesSearchTextBox.Text = "Search ...";

            if (seriesSearchTextBox.Text == "Search ...")
                seriesSearchTextBox.Foreground = Brushes.DarkGray;
        }

        private void seriesSearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (seriesSearchTextBox.Text == "Search ...")
                seriesSearchTextBox.Clear();

            seriesSearchTextBox.Foreground = Brushes.Black;
        }

        private void SearchSeries(string name)
        {
            if (searching)
                return;

            if (string.IsNullOrEmpty(name))
                return;

            searching = true;
            IEnumerable<TvDbSeriesBase> matches = null;
            Action del = new Action(delegate
            {
                TvDb db = new TvDb(API_KEY);
                matches = db.GetSeries(name);
            });
            del.BeginInvoke(new AsyncCallback(delegate(IAsyncResult iar)
            {
                searching = false;
                del.EndInvoke(iar);
                Application.Current.Dispatcher.Invoke(new Action<IEnumerable<TvDbSeriesBase>>(DisplaySeriesSearchResults), matches);
            }), null);
        }

        private void DisplaySeriesSearchResults(IEnumerable<TvDbSeriesBase> series)
        {
            searchResultsListBox.ItemsSource = series;
        }
    }
}
