using System.Collections;
using System.Collections.Generic;
using CellBig.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class AloneGameSkillDialog : IDialog
    {
        public GameObject parent;
        public GameObject skill_Item;
        public Text txtPotential;

        protected override void OnLoad()
        {

        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<SetSkillScrollMsg>(SetSkillScroll);
        }

        private void SetSkillScroll(SetSkillScrollMsg msg)
        {
            Skill_Table.Sheet tempSkill = msg.skill_Table.sheets[0];
            txtPotential.text = string.Format("잠재력 : {0}", msg.potential);

            if (parent.transform.childCount == tempSkill.list.Count)
            {
                for(int i = 0; i < tempSkill.list.Count; i++)
                {
                    parent.transform.GetChild(i).GetComponent<Skill_Item_Controller>().InitSkillLvAndPrice(msg.listSkillLv[i], tempSkill.list[i].price * (msg.listSkillLv[i] + 1));
                }
                return;
            }
            else
            {
                for (int i = 0; i < parent.transform.childCount; i++)
                {
                    Destroy(parent.transform.GetChild(i).gameObject);
                }
            }

            float height = skill_Item.GetComponent<RectTransform>().sizeDelta.y;
            float blank = 10.0f;
            float parentHeight = (height + blank) * (tempSkill.list.Count) + blank;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, parentHeight);

            for (int i = 0; i < tempSkill.list.Count; i++)
            {
                GameObject temp_Obj = GameObject.Instantiate(skill_Item) as GameObject;
                temp_Obj.transform.parent = parent.transform;
                temp_Obj.transform.localScale = new Vector3(1, 1, 1);
                temp_Obj.transform.localPosition = new Vector3(0, -(blank + height) * i, 0);
                temp_Obj.GetComponent<Skill_Item_Controller>().InitItemButton(tempSkill.list[i].Index, tempSkill.list[i].name, tempSkill.list[i].price.ToString(), tempSkill.list[i].content, tempSkill.list[i].path, msg.listSkillLv[i]);
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<SetSkillScrollMsg>(SetSkillScroll);
        }
    }
}