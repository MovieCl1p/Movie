using System;
using Core.Binder;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.Services.Instance;
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

        private IInstanceService _instanceService;

        protected override void Start()
        {
            base.Start();

            _instanceService = BindManager.GetInstance<IInstanceService>();
            _instanceService.StartGame();

            _questionView.OnPlayerAnswer += OnPlayerAnwer;
            _instanceService.OnFinishRound += OnFinishRaund;

            ShowNextQuiz();

        }
        
        private void OnFinishRaund()
        {
            _questionView.HighlightCorrectAnswer();

            ViewManager.Instance.SetView(ViewNames.MainMenu);
        }

        private void ShowNextQuiz()
        {
            IQuizService quizService = BindManager.GetInstance<IQuizService>();
            QuizData data = quizService.GetQuiz();

            _questionView.RefreshView(data);
            _question.text = data.Question;
        }

        private void OnPlayerAnwer(AnswerView answerView)
        {
            IInstanceService network = BindManager.GetInstance<IInstanceService>();
            network.PlayerAnswer(answerView.Data);

            _questionView.OnPlayerAnswer -= OnPlayerAnwer;
        }

        protected override void OnReleaseResources()
        {
            _questionView.OnPlayerAnswer -= OnPlayerAnwer;
            _instanceService.OnFinishRound -= OnFinishRaund;
            base.OnReleaseResources();
        }
    }
}
