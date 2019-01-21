using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeComponent : MonoBehaviour
{
    //깍일나무 조각들
    [SerializeField]
    private GameObject[] go_treePieces;

    [SerializeField]
    private GameObject go_treeCenter;

    //도끼질 효과
    [SerializeField]
    private GameObject go_hit_effect_prefab;

    //파편제거 시간
    [SerializeField]
    private float debriDestroyTime;

    [SerializeField]
    private float DestroyTime;


    //부모트리 파괴되면 캡슐 콜라이더 제거 
    [SerializeField]
    private CapsuleCollider parentCol;

    //자식트리 쓰러질때 필요한 컴포넌트 활성화 
    [SerializeField]
    private CapsuleCollider childCol;
    [SerializeField]
    private Rigidbody childRigid;

    [SerializeField]
    private string chop_sound;
    [SerializeField]
    private string falldown_sound;
    [SerializeField]
    private string logChange_sound;



    public void Chop(Vector3 _pos, float angleY) {

        Hit(_pos);

        AngleCalc(angleY);

    }

    //적중 이펙트
    private void Hit(Vector3 _pos) {


        //GameObject clone = Instantiate(go_hit_effect_prefab, _pos, Quaternion.Euler(Vector3.zero));
        //Destroy(clone, debriDestroyTime);


    }

    private void AngleCalc(float _angleY) {

        Debug.Log(_angleY);
        if (0 <= _angleY && _angleY <= 70)
            DestroyPiece(2);
        else if (70 <= _angleY && _angleY <= 140)
            DestroyPiece(3);
        else if (140 <= _angleY && _angleY <= 210)
            DestroyPiece(4);
        else if (210 <= _angleY && _angleY <= 280)
            DestroyPiece(0);
        else if (280 <= _angleY && _angleY <= 350)
            DestroyPiece(1);

    }
    private void DestroyPiece(int _num) {

        if (go_treePieces[_num].gameObject != null) {
            Destroy(go_treePieces[_num].gameObject);
        }

    }


}
