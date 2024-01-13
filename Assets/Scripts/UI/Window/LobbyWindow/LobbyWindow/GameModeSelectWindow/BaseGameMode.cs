using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public abstract class BaseGameMode : MonoBehaviour
    {
        public E_GAMEMODE GameMode { get; protected set; } = E_GAMEMODE.Unknown;
        protected bool _init = false;

        private void Awake()
        {
            Init();
        }

        public virtual bool Init()
        {
            if (_init) return false;

            _init = true;
            return true;
        }

        public virtual void Open(Transform parent = null)
        {
            if (parent != null)
            {
                this.gameObject.SetActive(false);

                RectTransform rectTr = this.GetComponent<RectTransform>();
                rectTr.SetParent(parent);
                rectTr.offsetMin = new Vector2(0, 0);
                rectTr.offsetMax = new Vector2(0, 0);
                rectTr.localScale = Vector3.one;
            }

            this.gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}