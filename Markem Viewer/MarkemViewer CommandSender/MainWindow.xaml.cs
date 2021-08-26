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
        ///  contains last response from markem instrument
        /// </summary>
        public string LastInstrumentResponse { get; set; }

        /// <summary>
        ///  contains path to file with markem commands
        /// </summary>
        public string pathForJsonFileWithNgpcl { get; private set; } = "../MarkemNgpclCommands.json";
       
        private int _maximumResponseTime = 1000;
        private readonly IResponseTranslator _responseTranslator;
        private readonly INgpclJsonCommandsExtractor _commandReceiver;




        public MainWindow()
        {
            InitializeComponent();

            _responseTranslator = new ResponseTranslator();
            _commandReceiver = new NgpclJsonFileCommandExtractor( new JsonSerializer());

            try
            {
                // loading up list of markem commands 
                ngcplCommands.ItemsSource = _commandReceiver.GetListOfCommands(pathForJsonFileWithNgpcl).Commands;
                 
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
                LastInstrumentResponse = TcpIpConnector.Instance.startCommunication(ipAddresses.Text, 21000, selectedCommand.NGPCL, _maximumResponseTime);
                ngpclCommandResult.Text = convertResponseDictionaryToString(_responseTranslator.TranslateResponse(LastInstrumentResponse));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, something went wrong. Send this message to application provider \n\n " + ex.Message);
            }

        }

        private void ipAddresses_TextChanged(object sender, TextChangedEventArgs e)=> EnableOrDistableControlObject(submitButton, ()=> ipAddresses.Text.ValidateIpAddress() && (ngcplCommands.SelectedItem != null));

        private void maxResponseTime_TextChanged(object sender, TextChangedEventArgs e) => EnableOrDistableControlObject(setAdvancedConfiguration, () => maxResponseTime.Text.ValidatePositiveNumber());

        private void ngcplCommands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableOrDistableControlObject(submitButton, () => IpAddressValidator.ValidateIpAddress(ipAddresses.Text) && (ngcplCommands.SelectedItem != null));

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

        private void setAdvancedConfiguration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _maximumResponseTime = (maxResponseTime.Text.ValidatePositiveNumber())? Convert.ToInt32(maxResponseTime.Text): _maximumResponseTime;

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

        private string convertResponseDictionaryToString(Dictionary<string, string> dict)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in dict)
                strBuilder.Append(item.Key).Append(": ").Append(item.Value).Append("\n");

            return strBuilder.ToString();
        }

        private void startCyclicRequestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stopCyclicRequestButton_Click(object sender, RoutedEventArgs e)
        {

        }





        private void EnableOrDistableControlObject(Control buttonRef, Func<bool> func) => buttonRef.IsEnabled = func.Invoke();

    }
}
