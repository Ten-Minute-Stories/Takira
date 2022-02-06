using System.Windows;
using System.Windows.Controls;

namespace Takira
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private void setButtons()
        {
            Button myButton = new Button();
            myButton.Content = "Answer 1";
            myButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            myButton.Height = 30;
            this.Answers.Children.Add(myButton);
        }

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            setButtons();
        }
    }
}