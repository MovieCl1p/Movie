using System;

namespace Game.Services
{
    public interface IQuizService
    {
        QuizData GetQuiz();

        void Init();
    }
}
