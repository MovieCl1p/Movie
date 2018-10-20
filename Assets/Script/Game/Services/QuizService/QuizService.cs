using System;
using System.Collections.Generic;

namespace Game.Services
{
    public class QuizService : IQuizService
    {
        private List<QuizData> _quizes;

        public void Init()
        {
            _quizes = new List<QuizData>();
            MockQuiz();
        }

        public QuizData GetQuiz()
        {
            return _quizes[0];
        }

        private void MockQuiz()
        {
            int id = -1;
            string question = "What are most popular movie 2017?";
            string[] answers = new string[] { "Tor", "Avangers", "Mission imposible", "Day1"};
            int answer = 0;
            QuizData data = new QuizData(id, question, answers, answer);

            _quizes.Add(data);
        }
    }
}
