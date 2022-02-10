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
        public static Stack<string> PageHistory = new Stack<string>();

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
            if (PageHistory.Count != 0)
                window.AddButton(PageHistory.Peek(), "Назад");
            window.CurrentPageHeader = header;
        }
    }
}