using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Takira.Helpers
{
    public static class FormatHelper
    {
        /// <summary>
        /// Превращает форматирование(жирный, курсив) текста из синтаксиса MarkDown в синтаксис XAML.
        /// </summary>
        /// <param name="text">строка, имеющая форматирование MarkDowb(*курсив*, **жирный**)</param>
        /// <returns>Inline текстового блока(TextBlock), готовый к отображению</returns>
        /// <example>Пример применения форматирования. Обратите внимание, что присваивание текста происходит сразу же, поэтому менять Content текст-блока не нужно
        /// <code lang="C#">
        /// TextBlock myTextBlock = new TextBlock();
        /// var inline = QuestParseHandler.ApplyFormatting("*Курсивный текст*");
        /// myTextBlock.Inlines.AddRange(inline);
        /// </code></example>
        public static IEnumerable<Inline> ApplyFormatting(string text)
        {
            var textBlock = (TextBlock) XamlReader.Parse(
                "<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">"
                + parse(text) + "</TextBlock>");
            return textBlock.Inlines.ToList();
        }

        private static string parse(string text)
        {
            text = text.Replace("\n", "<LineBreak/>");
            
            Regex regex = new Regex(@"(?<!\*|\\)\*([^\*\n][\s\S]+?[^\*|\\])\*(?!\*)");
            foreach (Match match in regex.Matches(text))
            {
                string result = match.Result("<Italic>$1</Italic>");
                text = text.Replace(match.Value, result);
            }
            
            regex = new Regex(@"(?<!\*|\\\*)\*{2,2}([^\*\n][\s\S]+?[^\*])\*{2,2}(?!\*|\\)");
            foreach (Match match in regex.Matches(text))
            {
                string result = match.Result("<Bold>$1</Bold>");
                text = text.Replace(match.Value, result);
            }

            regex = new Regex(@"(?<!\*|\\)\*{3,3}([^\*\n][\s\S]+?[^\*|\\])\*{3,3}(?!\*)");
            foreach (Match match in regex.Matches(text))
            {
                string result = match.Result("<Bold><Italic>$1</Italic></Bold>");
                text = text.Replace(match.Value, result);
            }
            return text;
        }
    }
}