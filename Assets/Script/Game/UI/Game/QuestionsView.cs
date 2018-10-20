using System;
using System.Collections.Generic;
using Core;
using Game.Services;
using UnityEngine;

namespace Game.UI.Game
{
    public class QuestionsView : BaseMonoBehaviour
    {
        public event Action OnPlayerAnswer;

        [SerializeField]
        private Transform _content;

        [SerializeField]
        private AnswerView _prefab;

        private List<AnswerView> _items = new List<AnswerView>();

        public void RefreshView(QuizData data)
        {
            Clean();

            for (int i = 0; i < data.Answers.Length; i++)
            {
                AnswerView view = Instantiate(_prefab, _content);
                view.Init(data.Answers[i]);

                view.OnClick += OnItemClick;

                _items.Add(view);
            }
        }

        private void OnItemClick(AnswerView view)
        {
            if(OnPlayerAnswer != null)
            {
                OnPlayerAnswer();
            }
        }

        private void Clean()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                _items[i].OnClick -= OnItemClick;
                Destroy(_items[i].gameObject);
            }

            _items.Clear();
        }
    }
}

