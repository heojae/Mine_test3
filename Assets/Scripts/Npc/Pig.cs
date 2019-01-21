﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{

    [SerializeField] private string animalName;
    [SerializeField] private int hp;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float applySpeed;



    private bool isAction;
    private bool isWalking;
    private bool isRunning;
    private bool isDead;//죽었는지 판별


    [SerializeField] private float walkTime;
    [SerializeField] private float WaittIme;
    [SerializeField] private float RunTime;


    private float currentTime;


    private Vector3 direction;


    [SerializeField]
    private GameObject new_pig;




    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;

    private AudioSource theAudio;

    [SerializeField] private AudioClip[] sound_pig_Normal;
    [SerializeField] private AudioClip sound_pig_Hurt;
    [SerializeField] private AudioClip sound_pig_Dead;





    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        currentTime = WaittIme;
        isAction = true;



        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapsTime();
        }
    }

    private void Move() {

        if (isWalking || isRunning) {
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
        }
    }
    private void Rotation() {
        if (isWalking || isRunning) {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles,new Vector3(0f, direction.y,0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }

    }






    private void ElapsTime() {
        if (isAction) {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) {
                ReSet();
            }

        }

    }

    private void ReSet() {

        isWalking = false;
        isAction = true;
        isRunning = false;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
        applySpeed = walkSpeed;
        

    }



    private void RandomAction() {
        RandomSound();

        int _random = Random.Range(3, 4);


        if (_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            Eat();
        }
        else if (_random == 2)
        {
            Peek();
        }
        else if (_random == 3)
        {
            TryWalk();
        }



    }
    private void Wait() {
        currentTime = WaittIme;
        Debug.Log("대기");
    }

    private void Eat()
    {
        currentTime = WaittIme;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }
    private void Peek()
    {
        currentTime = WaittIme;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        Debug.Log("걷기");
        applySpeed = walkSpeed;
    }

    private void Run(Vector3 _targetPos) {

        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;


        currentTime = RunTime;
        isWalking = false;
        isRunning = true;
        anim.SetBool("Running", isRunning);
        applySpeed = runSpeed;


    }
    public void Damage(int _dmg, Vector3 _targetPos) {
        Debug.Log("llllllllllllll");

        if (!isDead) {

            hp -= _dmg;
            if (hp <= 0)
            {
                Dead();

                //GameObject carrot_plant = (GameObject)Instantiate(Carrot_Plant, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                //Destroy(this);

                return;

            }
            anim.SetTrigger("Hurt");
            Run(_targetPos);

            PlaySE(sound_pig_Hurt);
        }



    }

    



    private void Dead() {

        PlaySE(sound_pig_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");


    }




    private void RandomSound() {
        int _random = Random.Range(0, 3);//일상 사운드 세개
        PlaySE(sound_pig_Normal[_random]);
    }


    private void PlaySE(AudioClip _clip) {
        theAudio.clip = _clip;
        theAudio.Play();

    }



}
