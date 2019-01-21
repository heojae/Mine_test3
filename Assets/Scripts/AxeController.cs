using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    //활성화 여부
    public static bool isActivate = false;




    



    void Update()
    {

        if (isActivate)
        {
            Debug.Log("aaaaa2222");
            TryAttack();
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

                if (hitInfo.transform.tag == "Tree") {
                    hitInfo.transform.GetComponent<TreeComponent>().Chop(hitInfo.point,transform.eulerAngles.y);

                }




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
