using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Takira.Handlers;
using Takira.Helpers;
using Takira.Objects;

namespace Takira
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // Надо будет переименовать этот класс
    // Но когда-нибудь потом... когда я сделаю переключение между окнами
    public partial class MainWindow
    {
        public Dictionary<string, QuestPage> Story;
        private string currentPageHeader;
        private Stack<string> pageHistory = new Stack<string>();
        private Stack<string> inversePageHistory = new Stack<string>();
        
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            Story = QuestParseHandler.LoadQuestFromFile("Realmnauts.Act1.Printer.tw");
            SetPage(Story.ElementAt(0).Key);
            Save.Source = ImageHelper.ConvertImage(Icons.Save, 35, 35);
            Load.Source = ImageHelper.ConvertImage(Icons.Load, 35, 30);
            ArrowLeft.Source = ImageHelper.ConvertImage(Icons.ArrowLeft, 35, 35);
            ArrowRight.Source = ImageHelper.ConvertImage(Icons.ArrowRight, 35, 35);
        }
        
        public void SetButtons(QuestPage page)
        {
            this.Answers.Children.Clear();
            foreach (string[] answer in page.answers)
                AddButton(answer[0], answer[1]);
        }

        public void AddButton(string header, string rename)
        {
            Button button = new Button();
            TextBlock buttonText = new TextBlock();
            // Применяем "стиль"
            buttonText.TextWrapping = TextWrapping.Wrap;
            button.HorizontalContentAlignment = HorizontalAlignment.Left;
            button.MinHeight = 30;
            // Устанавливаем текст кнопки и сразу применяем форматирование
            buttonText.Inlines.AddRange(QuestParseHandler.ApplyFormatting(rename));
            button.Content = buttonText;
            // Связываем кнопку со страницей, на которую она ссылается
            button.Tag = header;
            button.Click += new RoutedEventHandler(SwitchPage_OnClick);
            this.Answers.Children.Add(button);
        }

        /// <summary>
        /// Изменяет текст в блоке повествования независимо от других элементов
        /// </summary>
        public void SetText(string text)
        {
            this.QuestText.Text = "";
            this.QuestText.Inlines.AddRange(QuestParseHandler.ApplyFormatting(text));
        }
        
        /// <summary>
        /// [НЕ ГОТОВО] Устанавливает картинку на данной странице квеста
        /// </summary>
        public void SetIllustration(Image image)
        {
            
        }

        /// <summary>
        /// [НЕ ГОТОВО] Устанавливает состояние персонажа на данной странице квеста
        /// </summary>
        public void SetStatus(string status)
        {
            
        }

        /// <summary>
        /// Меняет страницу квеста на другую по названию страницы.
        /// </summary>
        /// <param name="window">Объект квестового окна</param>
        /// <param name="header">Название страницы</param>
        ///
        public void SetPage(string header)
        {
            QuestPage page = Story[header];
            SetButtons(page);
            SetText(page.text);
            currentPageHeader = header;
        }

        private void SwitchPage_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            pageHistory.Push(currentPageHeader);
            Forward.IsEnabled = false;
            Back.IsEnabled = true;
            inversePageHistory.Clear();
            SetPage((string) button.Tag);
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            SerializationHandler.Save(Story);
        }
        
        private void Load_OnClick(object sender, RoutedEventArgs e)
        {
            // Пока хардкод, ибо для теста
            // TODO: Имплементировать загрузку квеста из файла. Возможно, через "обзор".
            SerializationHandler.Load("Realmnauts.Act1.Printer.cqt");
        }

        private void Left_OnClick(object sender, RoutedEventArgs e)
        {
            if (pageHistory.Count != 0)
            {
                inversePageHistory.Push(currentPageHeader);
                Forward.IsEnabled = true;
                SetPage(pageHistory.Pop());
            }
            if (pageHistory.Count == 0)
            {
                Button button = (Button)sender;
                button.IsEnabled = false;
            }
        }

        private void Right_OnClick(object sender, RoutedEventArgs e)
        {
            if (inversePageHistory.Count != 0)
            {
                pageHistory.Push(currentPageHeader);
                Back.IsEnabled = true;
                SetPage(inversePageHistory.Pop());
            }
            if (inversePageHistory.Count == 0)
            {
                Button button = (Button)sender;
                button.IsEnabled = false;
            }
        }
    }
}