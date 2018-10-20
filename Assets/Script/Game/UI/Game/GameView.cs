using System;
using Core.Binder;
using Core.ViewManager;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game
{
    public class GameView : BaseView
    {
        [SerializeField]
        private QuestionsView _questionView;

        [SerializeField]
        private Text _question;

        protected override void Start()
        {
            base.Start();

            _questionView.OnPlayerAnswer += OnPlayerAnwer;

            //if (PhotonNetwork.isMasterClient)
            //{
            //    ScheduleUpdate(1, true);
            //}

            ShowNextQuiz();
        }

        protected override void OnScheduledUpdate()
        {
            
        }

        private void ShowNextQuiz()
        {
            IQuizService quizService = BindManager.GetInstance<IQuizService>();
            QuizData data = quizService.GetQuiz();

            _questionView.RefreshView(data);
            _question.text = data.Question;
        }

        private void OnPlayerAnwer()
        {
            IQuizService quizService = BindManager.GetInstance<IQuizService>();
        }
    }
}
