using DevionGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class PlayerWeaponManager: MonoBehaviour
    {
        [SerializeField] PlayerManager player;
        IKControl ikControl;

        //[HideInInspector]
        // only tested with new character (yes it says Schoulder not Shoulder, not my asset but animation rig breaks if you change it)
        public string handPath = "Armature/Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Schoulder_R/Elbow_R/Hand_R";

        [Header("Current Weapon Model")]
        public GameObject swordModel;
        public GameObject weaponModelClone;
        public GameObject weaponModelCloneHandle;
        public GameObject weaponModelCloneSprintHandle;
        [SerializeField] LayerMask itemLayer = 12;


        //private bool isDoneSprinting = true;

        // Start is called before the first frame update
        void Start()
        {
            weaponModelClone = Instantiate(swordModel);
            LoadWeapon(weaponModelClone);

            weaponModelCloneHandle = weaponModelClone.FindChild("Handle", true);
            weaponModelCloneSprintHandle = weaponModelClone.FindChild("SprintHandle", true);
            ikControl = GetComponentInParent<IKControl>();
            ikControl.leftHandObj = weaponModelCloneHandle;

            player = GetComponentInParent<PlayerManager>();

            player.hitbox = weaponModelClone.FindChild("Hitbox", true).GetComponent<ItemHitbox>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player.isSprinting)
            {
                ikControl.leftHandObj = weaponModelCloneSprintHandle;
            }
            else
            {
                ikControl.leftHandObj = weaponModelCloneHandle;
            }
        }

        public void UnloadWeapon()
        {
            if (weaponModelClone != null)
            {
                Destroy(weaponModelClone);
            }
        }

        public void LoadWeapon(GameObject weaponModel)
        {
            weaponModelClone = weaponModel;

            weaponModelClone.transform.parent = player.transform.Find(handPath);
            weaponModelClone.transform.localPosition = Vector3.zero;
            weaponModelClone.transform.localRotation = Quaternion.identity;
            weaponModelClone.transform.localScale = Vector3.one;
            weaponModelClone.layer = itemLayer; // set layer to item layer
        }
    }
}
