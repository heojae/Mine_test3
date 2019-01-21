using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandController : CloseWeaponController
{
    //활성화 여부
    public static bool isActivate = false;

    [SerializeField]
    private GameObject[] Carrot_jip;

    private GameObject Carrot_Fruit;
    private GameObject Dirt_Pile;
    private GameObject Carrot_Plant;


    private int timer=0;


    private Dictionary<int,GameObject> Alive_Dirty_Pile = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> Alive_Carrot_Plant = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> Alive_Carrot_Fruit = new Dictionary<int, GameObject>();


    void Start() {
        Carrot_Fruit = Carrot_jip[0];
        
        Carrot_Plant= Carrot_jip[2];


        Dirt_Pile= Carrot_jip[1];
    }



    void Update()
    {
        timer = timer + 1;

        if (isActivate)
        {
            Debug.Log("aaaaa2222");
            TryAttack();
            MakingCarrot();


        }

    }

    private void MakingCarrot() {
        float randomX = Random.Range(-0.5f, 0.5f);
        float checkTime;
        GameObject checkGameObject;
        Vector3 checkposition;
        Vector3 checkposition2;

        if (Input.GetButton("Fire2"))
        {
            GameObject dirty_pile = (GameObject)Instantiate(Dirt_Pile, new Vector3(transform.position.x+1, 1, transform.position.z), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            Alive_Dirty_Pile.Add(timer,dirty_pile);
            Debug.Log(Alive_Dirty_Pile.Count);
        }
        

        for (int i = 0; i < Alive_Dirty_Pile.Count; i++) {
            var keys = new List<int>(Alive_Dirty_Pile.Keys);
            checkTime = keys[i];
            if (timer - checkTime >= 300) {
                checkGameObject = Alive_Dirty_Pile[keys[i]];
                checkposition = checkGameObject.transform.position;

                Destroy(checkGameObject);
                GameObject carrot_plant = (GameObject)Instantiate(Carrot_Plant, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                Alive_Dirty_Pile[keys[i]] = carrot_plant;
            }
        }


        for (int i = 0; i < Alive_Dirty_Pile.Count; i++)
        {
            var keys = new List<int>(Alive_Dirty_Pile.Keys);
            checkTime = keys[i];
            if (timer - checkTime >= 600)
            {
                checkGameObject = Alive_Dirty_Pile[keys[i]];
                checkposition = checkGameObject.transform.position;
                Destroy(checkGameObject);
                GameObject carrot_fruit = (GameObject)Instantiate(Carrot_Fruit, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                Alive_Dirty_Pile[keys[i]] = carrot_fruit;
            }
        }




    }

    





    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;
    }


}
