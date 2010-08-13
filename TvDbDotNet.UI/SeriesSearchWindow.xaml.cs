#region License
/*
Copyright © 2010, The TvDbDotNet Project
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.

3. Neither the name of the nor the
names of its contributors may be used to endorse or promote products
derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS “AS IS” AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
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
