using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellBig.UI.Event;
using CellBig.Constants;

namespace CellBig.UI
{
    public class StoreMainDialog : IDialog
    {
        public Button btnCashShop;
        public Button btnSkinShop;
        public Button btnAvatarlShop;

        protected override void OnLoad()
        {
            btnCashShop.onClick.AddListener(() => Message.Send<OpenStoreDialogMsg>(new OpenStoreDialogMsg(StoreMenu.CashShop)));
            btnSkinShop.onClick.AddListener(() => Message.Send<OpenStoreDialogMsg>(new OpenStoreDialogMsg(StoreMenu.SkinShop)));
            btnAvatarlShop.onClick.AddListener(() => Message.Send<OpenStoreDialogMsg>(new OpenStoreDialogMsg(StoreMenu.IdolShop)));
        }
    }
}