/******************************************************************************/
/*                                                                            */
/*   Program: MyColoredLogListBox                                             */
/*   An example for a colored log ListBox                                     */
/*                                                                            */
/*   29.01.2017 1.0.0.0 uhwgmxorg Start                                       */
/*                                                                            */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyColoredLogListBox
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> SystemColores { get; set; }
        public string SelectedColore { get; set; }

        #region INotifyPropertyChanged Properties
        private String logMessage;
        public String LogMessage
        {
            get { return logMessage; }
            set
            {
                if (value != LogMessage)
                {
                    logMessage = value;
                    OnPropertyChanged("LogMessage");
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            SystemColores = GettAllSystemColores();
            LogMessage = "Test Log Message";

            mlbl_ListBoxLog.ListBoxLogMessageAdd(LogMessage, System.Windows.Media.Brushes.Black);
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// button_Add_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            mlbl_ListBoxLog.ListBoxLogMessageAdd(LogMessage, StringToBrush(SelectedColore));
        }

        /// <summary>
        /// button_Clear_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Clear_Click(object sender, RoutedEventArgs e)
        {
            mlbl_ListBoxLog.Clear();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// GettAllSystemColores
        /// </summary>
        private ObservableCollection<string> GettAllSystemColores()
        {
            ObservableCollection<string> SColores = new ObservableCollection<string>();

            // Get all SolidColorBrush in the sealed Brushes class (as strings)
            var brushes = typeof(System.Windows.Media.Brushes).GetProperties().Where(pi => pi.PropertyType == typeof(SolidColorBrush)).Select(pi => pi.Name).ToList();

            // Convert the List to a ObservableCollection
            foreach (string cname in brushes)
                SColores.Add(cname);

            // Set the SelectedColore to Black
            SelectedColore = SColores.FirstOrDefault(s => s.Contains("Black"));

            return SColores;
        }

        /// <summary>
        /// StringToBrush
        /// </summary>
        /// <param name="color"></param>
        /// Convert a string to a SolidColorBrush e.g 
        /// "Balck" --> public static SolidColorBrush Black { get; }
        /// <returns></returns>
        private SolidColorBrush StringToBrush(string color)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFromString(color);
        }

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="p"></param>
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }
}
