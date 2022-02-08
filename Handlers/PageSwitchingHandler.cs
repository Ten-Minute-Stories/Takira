using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Takira.Objects;

namespace Takira.Handlers
{
    public static class PageSwitchingHandler
    {
        private static Stack<string> pageHistory = new Stack<string>();
        /// <summary>
        /// Меняет страницу квеста на другую из кнопки.<br/>
        /// Button tags:<br/>
        /// [0] - объект квестового окна<br/>
        /// [1] - название(header) страницы
        /// [2] - переименование варианта ответа. Нужно для работоспособности кнопки "назад"
        /// </summary>
        public static void SwitchPage(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            object[] tags = button.Tag as object[];
            MainWindow window = tags[0] as MainWindow;
            if ((string) tags[2] == "Назад") pageHistory.Pop();
            else pageHistory.Push(window.CurrentPageHeader);
            SetPage(window, (string)tags[1]);
        }

        /// <summary>
        /// Меняет страницу квеста на другую по названию страницы.
        /// </summary>
        /// <param name="window">Объект квестового окна</param>
        /// <param name="header">Название страницы</param>
        public static void SetPage(MainWindow window, string header)
        {
            QuestPage page = window.Story[header];
            window.SetButtons(page);
            window.SetText(page.text);
            if (pageHistory.Count != 0)
                window.AddButton(pageHistory.Peek(), "Назад");
            window.CurrentPageHeader = header;
        }
    }
}