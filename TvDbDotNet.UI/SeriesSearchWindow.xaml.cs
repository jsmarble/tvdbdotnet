#region License
/*
"The contents of this file are subject to the Mozilla Public License
Version 1.1 (the "License"); you may not use this file except in
compliance with the License. You may obtain a copy of the License at
http://www.mozilla.org/MPL/

Software distributed under the License is distributed on an "AS IS"
basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
License for the specific language governing rights and limitations
under the License.

The Initial Developer of the Original Code is Joshua Marble.
Portions created by Joshua Marble are Copyright (C) 2010. All Rights Reserved.

Contributor(s):

Alternatively, the contents of this file may be used under the terms
of the GPL license (the "[GPL] License"), in which case the
provisions of [GPL] License are applicable instead of those
above. If you wish to allow use of your version of this file only
under the terms of the [GPL] License and not to allow others to use
your version of this file under the MPL, indicate your decision by
deleting the provisions above and replace them with the notice and
other provisions required by the [GPL] License. If you do not delete
the provisions above, a recipient may use your version of this file
under either the MPL or the [GPL] License."
*/
#endregion

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
    public partial class SeriesSearchWindow : Window
    {
        private const string API_KEY = "AFC397F804955A85"; //Only for use in TvDbDotNet UI application.
        private bool searching;
        private DispatcherTimer searchTimer;
        private TvDb tvdb = new TvDb(API_KEY);

        public SeriesSearchWindow()
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
                matches = tvdb.GetSeries(name);
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

        private void searchResultsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var seriesBase = searchResultsListBox.SelectedItem as TvDbSeriesBase;
            var series = tvdb.GetSeries(seriesBase);
            SeriesDisplayWindow window = new SeriesDisplayWindow(series);
            window.Show();
        }
    }
}
