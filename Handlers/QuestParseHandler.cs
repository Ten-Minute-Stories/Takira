﻿using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Takira.Objects;

namespace Takira.Handlers
{
    public static class QuestParseHandler
    {
        // Не особо знаю где эта переменная пригодится и правильно ли она вообще тут стоит, но похер
        // Потом придумаем :D
        public static string StoryTitle;
        // Благодаря Dictionary мы можем обращаться к блокам квестов сразу по имени, а не искать среди массива
        public static Dictionary<string, QuestPage> LoadQuestFromFile(string filename)
        {
            string story = readFile(filename);
            return initPages(story);
        }

        // TODO: Сделать отлов ошибки о том, что файл не найден
        private static string readFile(string filename)
        {
            string path = Directory.GetCurrentDirectory() + "/Quests/" + filename;
            StreamReader reader = new StreamReader(path);
            string text = reader.ReadToEnd();
            reader.Close();
            return text;
        }

        private static Dictionary<string, QuestPage> initPages(string story)
        {
            Dictionary<string, QuestPage> pages = new Dictionary<string, QuestPage>();
            // Это регулярное выражение делит блоки следующим образом:
            // Если в тексте встретилось ::, оно берёт первую строчку в первую группу
            // Всё, что идёт в следующих строчках, ловится во вторую группу
            // Будет ловиться вообще весь текст, пока оно не наткнётся на строку, начинающуюся с ::
            Regex pageRegex = new Regex(@"(?::: (.+)\n([\s\S]+?(?=\n::|\z)))");
            // А это ищет все совпадения, в которых указаны ссылки на другие блоки квестов.
            // Примеры:
            // [[Осмотреться]]
            // [[- Ну ты глупая жестянка->Нагрубить]]
            Regex answersRegex = new Regex(@"\[\[([^->]+?)\]\]|\[\[(.+)->(.+)\]\]");
            MatchCollection pageMatches = pageRegex.Matches(story);
            // Первая строка всегда StoryTitle. Остальные два "заголовка" - лишние строчки. 
            StoryTitle = pageMatches[0].Groups[2].Value.Trim();
            for (int i = 3; i < pageMatches.Count; i++)
            {
                MatchCollection answerMatches = answersRegex.Matches(pageMatches[i].Groups[2].Value);
                string[][] answers = new string[answerMatches.Count][];
                int j = 0;
                string pageText = pageMatches[i].Groups[2].Value;
                foreach (Match answerMatch in answerMatches)
                {
                    string[] answer = new string[2];
                    // Если ссылка на другой блок не имеет переименования(т.е. выглядит как [[Осмотреться]]),
                    // Значит regex поймал только группу №1. Группы №2 и №3 остаются пустыми.
                    // В этом случае вариант, который мы показываем пользователю идентичен названию блока.
                    if (answerMatch.Groups[2].Value.Equals(""))
                        answer[0] = answer[1] = answerMatch.Groups[1].Value;
                    // Если переименование есть, то группа №1 пуста, а №2 и №3 содержат переименование и название соответственно.
                    else
                    {
                        answer[0] = answerMatch.Groups[3].Value;
                        answer[1] = answerMatch.Groups[2].Value;
                    }
                    // Добавляем один "ответ" в список всех ответов
                    answers[j] = answer;
                    j++;
                    
                    // Удаляем из текста ссылки на другие блоки
                    // Делается это потому, что этот кастрированный текст уже будет выводиться пользователю
                    pageText = pageText.Replace(answerMatch.Value, "");
                }
                // В принципе, остаётся вопрос: "зачем нужен header внутри структуры тоже?"
                // Вопрос хороший, и потенциально можно его из структуры вырезать, но пока не будем
                pages.Add(pageMatches[i].Groups[1].Value.Trim(),
                    new QuestPage()
                    {
                        header = pageMatches[i].Groups[1].Value.Trim(), 
                        answers = answers,
                        text = pageText
                    });
            }

            return pages;
        }
    }
}