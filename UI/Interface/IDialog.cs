using UnityEngine;
using JHC;

namespace CellBig.UI
{
	public class IDialog : MonoBehaviour
	{
		protected RectTransform _rt;
		protected string _name;
		public GameObject dialogView;
        public bool isActive;

        public bool isProjection;
        public bool isKiosk;
        int canvasIndex;

		void Awake()
		{
			if (dialogView == null)
				throw new System.NullReferenceException(string.Format("{0} dialogView Null", this.name));
		}

		public void Load()
		{
            _name = GetType().Name;
			_rt = GetComponent<RectTransform>();

			Message.AddListener<Event.ShowDialogMsg>(_name, Enter);
			Message.AddListener<Event.HideDialogMsg>(_name, Exit);

			int sibling = EnumExtensions.ParseToInt<Constants.UISibling>(_name);
            UIManager.Instance.SetSibling(_rt, sibling);

			dialogView.SetActive(false);
			OnLoad();
		}

        protected virtual void SetCanvas()
        {
            Debug.Log("SetCanvas");

            if (isKiosk)
                canvasIndex = SceenMrg.I.kioskCanvas.targetDisplay;

            transform.SetParent(SceenMrg.I.CanvasArray[canvasIndex]._addCanvas.transform);
            SceenMrg.I.CanvasArray[canvasIndex].dialog = transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
        }

		protected virtual void OnLoad()
		{
            if (isProjection)
                SetCanvas();
        }

		public void Unload()
		{
			Message.RemoveListener<Event.ShowDialogMsg>(_name, Enter);
			Message.RemoveListener<Event.HideDialogMsg>(_name, Exit);

			OnExit();
			OnUnload();
		}

		protected virtual void OnUnload()
		{
		}

		private void Enter(Event.ShowDialogMsg msg)
		{
			if (dialogView != null)
			{
				if (dialogView.activeSelf)
					return;

				dialogView.SetActive(true);
                isActive = true;
            }
			
			OnEnter();
		}

		private void Exit(Event.HideDialogMsg msg)
		{
            if (dialogView != null)
            {
                dialogView.SetActive(false);
                isActive = false;
            } 

            OnExit();
		}

        public void EnterDialog()
        {
            if (dialogView != null)
            {
                if (dialogView.activeSelf)
                    return;

                dialogView.SetActive(true);
                isActive = true;
            }

            OnEnter();
        }

        public void ExitDialog()
        {
            if (dialogView != null)
            {
                dialogView.SetActive(false);
                isActive = false;
            }
            OnExit();
        }


        protected virtual void OnDestroy()
        {
            Unload();
        }

		protected virtual void OnEnter()
		{
            isActive = true;
        }

		protected virtual void OnExit()
		{
            isActive = false;
        }

        public static void RequestDialogEnter<T>() where T : IDialog
		{
			Message.Send<UI.Event.ShowDialogMsg>(typeof(T).Name, new UI.Event.ShowDialogMsg());
        }

		public static void RequestDialogExit<T>() where T : IDialog
		{
			Message.Send<UI.Event.HideDialogMsg>(typeof(T).Name, new UI.Event.HideDialogMsg());
        }


        /// <summary>
        /// 왠만하면 이 메소드 보다 제네릭 메소드를 사용하세요.
        /// </summary>
        public static void RequestDialogEnter(System.Type t)
		{
			Message.Send<UI.Event.ShowDialogMsg>(t.Name, new UI.Event.ShowDialogMsg());
		}

		/// <summary>
		/// 왠만하면 이 메소드 보다 제네릭 메소드를 사용하세요.
		/// </summary>
		public static void RequestDialogExit(System.Type t)
		{
			Message.Send<UI.Event.HideDialogMsg>(t.Name, new UI.Event.HideDialogMsg());
		}
	}
}
