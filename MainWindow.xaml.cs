using System.Windows;
using System.Windows.Controls;
using Takira.Handlers;
using Takira.Objects;

namespace Takira
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // Здесь потом будет обращение к отдельному обработчику
        private void setButtons()
        {
            Button myButton = new Button();
            myButton.Content = "Answer 1";
            myButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            myButton.Height = 30;
            this.Answers.Children.Add(myButton);
        }

        // Это чисто хардкод-затычка для теста!
        private void setText(string text)
        {
            this.QuestText.Text = QuestParseHandler.LoadQuestFromFile("Realmnauts.Act1.Printer.tw")[0].text;
        }

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            setButtons();
            setText("Make me bold!");
        }
    }
}