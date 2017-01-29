using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MyColoredLogListBoxDll
{
    /// <summary>
    /// Interaktionslogik für MyListBoxLog.xaml
    /// </summary>
    public partial class MyListBoxLog : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        delegate void DMessageAdd(string message, System.Windows.Media.Brush color);
        private int _counter = 0;
        private const int MAX_MESSGAES = 2000;
        string Message { get; set; }
        private System.Windows.Media.Brush MessageColor { get; set; }
        public virtual Dispatcher DispatcherObject { get; protected set; }
        public ObservableCollection<ListBoxLogMessageString> MessageList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MyListBoxLog()
        {
            InitializeComponent();
            DataContext = this;

            DispatcherObject = Dispatcher.CurrentDispatcher;
            MessageList = new ObservableCollection<ListBoxLogMessageString>();        
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

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
        /// ListBoxLogMessageAdd
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        public void ListBoxLogMessageAdd(string message, System.Windows.Media.Brush color)
        {
            if (DispatcherObject.Thread != Thread.CurrentThread)
                DispatcherObject.Invoke(new DMessageAdd(ListBoxLogMessageAdd), DispatcherPriority.ApplicationIdle, message, color);
            else
            {
                MessageList.Add(new ListBoxLogMessageString(String.Format("{0}   {1:dd/MM/yyyy H:mm:ss.fff}   {2}", ++_counter, DateTime.Now, message), color, FontWeights.Normal,12));
                listBox_LogMessages.SelectedIndex = listBox_LogMessages.Items.Count - 1;
                listBox_LogMessages.ScrollIntoView(listBox_LogMessages.SelectedItem);
                if (MessageList.Count > MAX_MESSGAES)
                    MessageList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            MessageList.Clear();
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

    #region Help Classes
    public class ListBoxLogMessageString
    {
        private int DEFAULD_FONT_SIZE = 12;
        public string Message { get; set; }
        public System.Windows.Media.Brush MessageColor { get; set; }
        public FontWeight FontWeight { get; set; }
        public int FontSize { get; set; }
        public ListBoxLogMessageString(string message, System.Windows.Media.Brush colore)
        {
            Message = message;
            MessageColor = colore;
            FontWeight = FontWeights.Normal;
            FontSize = DEFAULD_FONT_SIZE;
        }
        public ListBoxLogMessageString(string message, System.Windows.Media.Brush colore, FontWeight fontWeight)
        {
            Message = message;
            MessageColor = colore;
            FontWeight = fontWeight;
            FontSize = DEFAULD_FONT_SIZE;
        }
        public ListBoxLogMessageString(string message, System.Windows.Media.Brush colore, FontWeight fontWeight,int fontSize)
        {
            Message = message;
            MessageColor = colore;
            FontWeight = fontWeight;
            FontSize = fontSize;
        }
    }
    #endregion
}
