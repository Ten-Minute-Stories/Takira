using System.Windows;
using System.Windows.Controls;

namespace Takira.Handlers
{
    public static class ButtonClickHandler
    {
        /// <summary>
        /// Меняет страницу квеста на другую из кнопки.<br/>
        /// Button tags:<br/>
        /// [0] - объект квестового окна<br/>
        /// [1] - название(header) страницы<br/>
        /// [2] - переименование варианта ответа. Нужно для работоспособности кнопки "назад"
        /// </summary>
        public static void SwitchPage(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            object[] tags = button.Tag as object[];
            MainWindow window = tags[0] as MainWindow;
            if ((string) tags[2] == "Назад") PageSwitchingHandler.PageHistory.Pop();
            else PageSwitchingHandler.PageHistory.Push(window.CurrentPageHeader);
            PageSwitchingHandler.SetPage(window, (string)tags[1]);
        }
        
    }
}