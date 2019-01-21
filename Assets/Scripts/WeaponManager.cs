﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon;
    [SerializeField]
    public float changeWeaponDelayTime;
    [SerializeField]
    public float changeWeaponEndDelayTime;

    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private CloseWeapon[] hands;
    [SerializeField]
    private CloseWeapon[] axes;
    [SerializeField]
    private CloseWeapon[] pickaxes;
    


    //관리차원에서 쉽게 무기 접근이 가능하도록 만듬
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();
    


    //필요한 컴포넌트
    [SerializeField]
    private GunController theGunController;
    [SerializeField]
    private HandController theHandController;
    [SerializeField]
    private AxeController theAxeController;
    [SerializeField]
    private PickaxeController thePickaxeController;
    



    //현재 무기의 타입
    [SerializeField]
    private string currentWeaponType;

    //현재 무기와 현재무기의 애니메이션
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChangeWeapon) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeaponCoroutine("HAND","맨손"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SuvMachineGun1"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ChangeWeaponCoroutine("AXE", "Axe"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StartCoroutine(ChangeWeaponCoroutine("PICKAXE", "Pickaxe"));
            }
            
        }



    }



    public IEnumerator ChangeWeaponCoroutine(string _type, string _name) {

        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type,_name);


        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;

    }
    private void CancelPreWeaponAction() {
        switch (currentWeaponType) {
            case "GUN":
                GunController.isActivate = false;
                break;
            case "HAND":
                HandController.isActivate = false;
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController.isActivate = false;
                break;
            


        }
    }

    private void WeaponChange(string _type, string _name) {
        if (_type == "GUN") {
            theGunController.GunChange(gunDictionary[_name]);
        }
        else if (_type == "HAND")
        {
            theHandController.CloseWeaponChange(handDictionary[_name]);
        }
        else if (_type == "AXE")
        {
            theAxeController.CloseWeaponChange(axeDictionary[_name]);
        }
        else if (_type == "PICKAXE")
        {
            thePickaxeController.CloseWeaponChange(pickaxeDictionary[_name]);
        }
        

    }


}
