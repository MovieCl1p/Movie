using System;
using Core;
using UnityEngine.UI;
using UnityEngine;

namespace Game.UI.Game
{
    public class AnswerView : BaseMonoBehaviour
    {
        public event Action<AnswerView> OnClick;

        [SerializeField]
        private Button _btn;

        [SerializeField]
        private Text _text;

        protected override void Start()
        {
            base.Start();

            _btn.onClick.AddListener(OnBtnClick);
        }

        public void Init(string answer)
        {
            _text.text = answer;
        }

        private void OnBtnClick()
        {
            if(OnClick != null)
            {
                OnClick(this);
            }
        }

        protected override void OnReleaseResources()
        {
            _btn.onClick.RemoveListener(OnBtnClick);
            base.OnReleaseResources();
        }
    }
}
