using System;
using System.Collections.Generic;
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
using MarkemViewer_Library.Interfaces;
using MarkemViewer_Library;
using Newtonsoft.Json;
using System.IO;

namespace MarkemViewer_CommandSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// contains downloaded markem comands
        /// </summary>
        public NgpclCommandsModel NgpclCommands { get; set; }
        /// <summary>
        ///  contains last response from markem instrument
        /// </summary>
        public string LastInstrumentResponse { get; set; }
        IResponseTranslator responseTranslator;
       
        private INgpclJsonCommandsExtractor commandReceiver;
        /// <summary>
        ///  contains path to file with markem commands
        /// </summary>
        string pathForJsonFileWithNgpcl = System.IO.Path.GetFullPath("MarkemNgpclCommands.json");
        private int MaximumResponseTime = 3000;

        public MainWindow()
        {
            InitializeComponent();

            commandReceiver = new NgpclJsonFileCommandExtractor( new JsonSerializer());
            responseTranslator = new ResponseTranslator();

            try
            {
                NgpclCommands = commandReceiver.GetListOfCommands(pathForJsonFileWithNgpcl);
                ngcplCommands.ItemsSource = NgpclCommands.Commands;
            }
            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Sorry, something went wrong. Please ensure that you have MarkemNgpclCommands.json in your Directory. \n\n " + ex.Message);

            }

        }

        private void ReceiveDataFromInstrument_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                NgpclCommandModel selectedCommand = (NgpclCommandModel)ngcplCommands.SelectedItem;
                LastInstrumentResponse = TcpIpConnector.Instance.startCommunication(ipAddresses.Text, 21000, selectedCommand.NGPCL, MaximumResponseTime);
                ngpclCommandResult.Text = convertResponseDictionaryToString(responseTranslator.TranslateResponse(LastInstrumentResponse));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, something went wrong. Send this message to application provider \n\n " + ex.Message);
            }

        }

        #region button enable/disable methods
        private void EnableOrDistableSubmitButtonCalc()
        {
            submitButton.IsEnabled= IpAddressValidator.ValidateIpAddress(ipAddresses.Text) && (ngcplCommands.SelectedItem != null);
        }

        private void EnableOrDistableSetAdvancedParametersButtonCalc()
        {
            setAdvancedConfiguration.IsEnabled = NumberValidator.ValidateNumber(maxResponseTime.Text);
        }

        #endregion
     
        private void ipAddresses_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableOrDistableSubmitButtonCalc();
        }


        private void ngcplCommands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableOrDistableSubmitButtonCalc();

            try
            {
                NgpclCommandModel commandModel = (NgpclCommandModel)ngcplCommands.SelectedItem;
                commandDescription.Text = commandModel.Description;
                ngpclCommandDescription.Text = commandModel.Units;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sorry, something went wrong. Send this message to application provider \n\n "+ ex.Message);
            }
        }

        private void maxResponseTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableOrDistableSetAdvancedParametersButtonCalc();
        }

        private void setAdvancedConfiguration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MaximumResponseTime = Int32.Parse(maxResponseTime.Text);

            }
            catch (FormatException ex)
            {
                MessageBox.Show("Entered data is invalid");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry, something went wrong. Send this message to application provider \n\n "+ ex.Message);
            }
        }
        
        private string convertResponseDictionaryToString( Dictionary<string,string> dict)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in dict)
            {
                strBuilder.Append(item.Key);
                strBuilder.Append(": ");
                strBuilder.Append(item.Value);
                strBuilder.Append("\n");
            }

            return strBuilder.ToString();
        }
    
    
    }
}
