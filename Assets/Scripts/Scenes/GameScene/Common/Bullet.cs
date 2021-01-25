﻿#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Bullet : MonoX
{
    #region Variables

    private Movement movement;
    private Vector3 initPos;
    [Header("Bullet Settings")]
    [Space]
    public BulletShot bulletShot;
    public Vector3 direction;
    [Space]
    public bool canFollow = false;
    //private Vector3 lastPos_follow;
    private Quaternion provitionalRotate;
    #endregion
    #region Events
    private void Awake(){
        Get(out movement);
        initPos = transform.position;
    }

    private void Start()
    {
        //Si puede seguirlo hace un calculo extra...
        if (canFollow)
        {
            //Buscarmos un enemigo random de la colección
            Transform tran_finder = GameManager.GetEnemiesContainer();
            direction = transform.position - tran_finder.GetChild(tran_finder.childCount-1).position;
            PrintX(direction);
            provitionalRotate = Quaternion.LookRotation(direction);
            

        }
    }
    private void Update(){ 

        if (PassedRange()){
            Destroy(gameObject);
        }else{
            if (canFollow)
            {
                //transform.rotation = Quaternion.LookRotation(direction);
                //Quaternion.Slerp(transform.rotation, provitionalRotate, Time.deltaTime);
                //TODO problemas aquí, me pone  -1, 0 ó 1...
                //SetDirection(Quaternion.LookRotation(lastPos_follow));
            }
            movement.Move(direction, bulletShot.speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.transform.tag;
        
        if (XavLib.XavHelpTo.Know.IsEqualOf(tag, "obstacle", "enemy"))
        {
            if (tag.Equals("enemy")){

                Enemy enemy = other.GetComponent<Enemy>();
                enemy.CheckDamage(bulletShot.damage);
            }

            Destroy(gameObject);
        }
    }
    #endregion
    #region Methods

    /// <summary>
    /// Asignamos una dirección (de tipo axis) basada en una rotación
    /// </summary>
    public void SetDirection(Quaternion rotation) => direction = Vector3.Normalize(rotation * Vector3.forward);

    /// <summary>
    /// Preguntamos si ha pasado el rango desde el punto inicial con el actual
    /// </summary>
    private bool PassedRange() => Vector3.Distance(initPos, transform.position) > bulletShot.range;

    #endregion
}