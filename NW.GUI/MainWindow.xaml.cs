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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetSize();
            LoadSavedConfiguration();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtText_KeyUp(object sender, KeyEventArgs e)
        {
            SetDisplayText();

        }

        private void SetBackGroundColor_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = (MenuItem)sender;

            SetCurrentBackgroundToHexadecimalText();
            SetBackgroundFromHexadecimal(new BrushConverter().ConvertToString(clickedItem.Background));
        }

        private void SliderWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Width = e.NewValue;

            SetSize();
        }

        private void BtnSaveConfiguration_Click(object sender, RoutedEventArgs e)
        {
            SaveDefaultConfiguration();
        }

        private void TxtHexadecimalColor_KeyUp(object sender, KeyEventArgs e)
        {
            SetBackgroundFromHexadecimal(TxtHexadecimalColor.Text.Trim());
        }

        #endregion Events

        #region Methods
        private void SaveDefaultConfiguration()
        {
            string displayText = "Default";
            string background = "#FFF";

            if (!string.IsNullOrEmpty(TxtDisplayText.Text))
                displayText = TxtDisplayText.Text;

            if (!string.IsNullOrEmpty(TxtHexadecimalColor.Text))
                background = TxtHexadecimalColor.Text;

            try {

                RepositoryContainer.GetConfigRepository().SetConfiguration(
                new WNBusiness.Models.ConfigModel() {
                    BackGroundColor = background,
                    DisplayText = displayText
                });

                lbSaveMessage.Content = "SAVED!";
                lbSaveMessage.Foreground = Brushes.Green;
            }
            catch (Exception ex) {

                lbSaveMessage.Content = "NOT SAVED!";
                lbSaveMessage.Foreground = Brushes.Red;
            }
        }

        private void LoadSavedConfiguration()
        {
            try {

                WNBusiness.Models.ConfigModel configmodel = RepositoryContainer.GetConfigRepository().GetConfiguration();

                if (configmodel == null)
                    return;

                TxtDisplayText.Text = configmodel.DisplayText;

                SetBackgroundFromHexadecimal(configmodel.BackGroundColor);
                SetCurrentBackgroundToHexadecimalText();
                SetDisplayText();
            }
            catch (Exception ex) { }
        }

        private void SetBackgroundFromHexadecimal(string inputValue)
        {
            inputValue = inputValue.Trim();

            if (string.IsNullOrEmpty(inputValue))
                return;

            if (inputValue.Length <= 1)
                return;

            if (inputValue.Substring(0, 1) != "#")
                inputValue = $"#{inputValue}";

            try {

                Color inputColor = new Color();

                inputColor = (Color)ColorConverter.ConvertFromString(inputValue);

                MainBorder.Background = new SolidColorBrush(inputColor);

            }
            catch (Exception ex) {

                MainBorder.Background = Brushes.White;
            }
        }

        private void SetCurrentBackgroundToHexadecimalText()
        {
            BrushConverter bc = new BrushConverter();

            TxtHexadecimalColor.Text = bc.ConvertToString(MainBorder.Background);
        }

        private void SetSize()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;

            this.Left = (screenWidth / 2) - (this.Width / 2);
            this.Top = 0;
            this.Topmost = true;

        }

        private void SetDisplayText()
        {
            lbText.Content = TxtDisplayText.Text;
        }

        #endregion Methods

        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            lbSaveMessage.Content = "Default configuration";
            lbSaveMessage.Foreground = Brushes.Black;

        }
    }
}
