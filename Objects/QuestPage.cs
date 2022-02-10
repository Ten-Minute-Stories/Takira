using System;
using System.Windows.Controls;

/*
 * Данный блок описывает одну "страницу" квеста. Каждая переменная отвечает за свою часть.
 * header - короткое название, имя данного блока.
 * answers - варианты ответа. 0 - имя следующего блока. 1 - то, что выводим юзеру.
 * text - текст. да.
 * image - ну, блин, картинка, что же ещё.
 */
namespace Takira.Objects
{
    [Serializable]
    public struct QuestPage
    {
        public string header { get; set; }
            public string[][] answers { get; set; }
            public string text { get; set; }
            public Image image { get; set; }
    }
}