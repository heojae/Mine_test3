using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{//미완성 클래스

    
    //현재 장착된 hand형 무기
    [SerializeField]
    protected private CloseWeapon currentCloseWeapon;

    //공격중?
    protected private bool isAttack = false;
    protected private bool isSwing = false;

    protected private RaycastHit hitInfo;



    // Update is called once per frame
    
    protected private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("aaaaa1111");
            if (!isAttack)
            {
                //코르틴 실행

                StartCoroutine(AttackCoroutine());

            }
        }

    }

    protected IEnumerator AttackCoroutine()
    {

        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);
        isSwing = true;

        //공격활성화 시점
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB);
        isAttack = false;
    }

    protected abstract IEnumerator HitCoroutine();
    protected private bool CheckObject()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, currentCloseWeapon.range))
        {
            //            print(transform.TransformDirection(Vector3.forward));
            Debug.Log(transform.TransformDirection(Vector3.forward));
            return true;
        }
        return false;
    }
    //완성함수 이지만 추가편짐이 가능한 함수
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }
        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;


        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
    }
}
