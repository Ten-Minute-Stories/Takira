using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Takira.Handlers;
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
        public string CurrentPageHeader;
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
            button.Tag = new object[] {this, header, rename};
            button.Click += new RoutedEventHandler(ButtonClickHandler.SwitchPage);
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

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            Story = QuestParseHandler.LoadQuestFromFile("Realmnauts.Act1.Printer.tw");
            PageSwitchingHandler.SetPage(this, Story.ElementAt(0).Key);
        }
    }
}