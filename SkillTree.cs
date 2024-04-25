using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CGP
{
    public class SkillTree : MonoBehaviour
    {
        public static SkillTree skilltree;
        private void Awake() => skilltree = this;

        public static string[] skillNames;
        public static string[] skillDescriptions;
        public static bool[] skillStatus;
        public static int fireOn = 0;
        public static int IceOn = 0;
        public static int AirOn = 0;


        public List<Skill> SkillList;
        public GameObject SkillHolder;

        public int skillPoints;

        public void Start()
        {
            skillPoints = 5;
            skillNames = new[] { "Inferno Blade", "Pyro Strike", "Flame Fury", "Tempest Edge", "Whirlwind Whirl", "Gale Slash", "Glacial Thrust", "Frostbite Slash", "Icicle Impale" };
            skillDescriptions = new[] { "Unleashes fiery inferno", "Engulfs in searing flames", "Rains down fiery fury", "Cuts through with gusts", "Creates a cyclone of air", "Whirls with relentless winds", "Freezes with biting frost", "Pierces with icy cold", "Impales with sharp icicles" };
            skillStatus = new[] { false, false, false, false, false, false, false, false, false };



            foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
            for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

            updateAllSkillUI();

        }

        public int GetFireValue()
        {
            if (skillStatus[0])
            {
                return 1;
            }

            if (skillStatus[1])
            {
                return 2;
            }

            if (skillStatus[2])
            {
                return 3;
            }

            return -1;

        }

        public int GetAirValue()
        {
            if (skillStatus[5])
            {
                return 1;
            }

            if (skillStatus[4])
            {
                return 2;
            }

            if (skillStatus[3])
            {
                return 3;
            }

            return -1;

        }

        public int GetIceValue()
        {
            if (skillStatus[8])
            {
                return 1;
            }

            if (skillStatus[7])
            {
                return 2;
            }

            if (skillStatus[6])
            {
                return 3;
            }

            return -1;
        }

        public void Update()
        {
            updateAllSkillUI();
        }


        public void updateAllSkillUI()
        {
            foreach (var skill in SkillList) skill.UpdateUI();
        }

    }

}