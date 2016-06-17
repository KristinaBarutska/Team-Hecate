using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hecate
{
    class MakeQuestions
    {
        //Стара концепция. Не се ползва, но нека остане, за всеки случай.
        public Question[] questColection =
                {
                new Question("Къде е роден Тодор Живков?", "Варна", "Правец", "Москва", "Истамбул","B"),
                new Question("Коя е столицата на Монголия?", "Банкок", "Куала Лумпур", "Улан Батор", "Бишкек","C"),

            };
        public void DisplayQuestCollection()
        {
            foreach (var question in questColection)
            {
                Console.WriteLine(question.ToString());
            }         
        }
    }
}
