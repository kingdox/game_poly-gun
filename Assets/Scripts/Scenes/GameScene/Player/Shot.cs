﻿#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion

public class Shot : MonoX
{
    #region
    [Header("ShotSettings")]
    public Transform parent_bullet;
    public Bullet pref_bullet;
    [Space] // creación
    public bool canShot;
    public float timer_bullet;
    public float timeCount_bullet;

    #endregion
    #region Events
    private void Update(){
        // si no puede disparar y termina su vuelta de tiempo... (el tiempo no corre si pudiese disparar
        CanPassedTime(ref canShot, ref timeCount_bullet, timer_bullet);
    }
    #endregion
    #region


    /// <summary>
    /// Creamos una bala y se le coloca los parametros basado en <see cref="Shot"/>
    /// <para>Devuelve true si ha completado el disparo</para>
    /// </summary>
    public bool ShotBullet(Character character){

        if (!canShot) return false;
        canShot = false;

        //crea balas extras pero con un flag de perseguidora
        //estas solo aparecen si en el rango hay algún objetivo?
        if (character.canExtraShots)
        {
            //CreateBullet(character).canFollow = true;
            StartCoroutine(CreateFollowBullet(character));
        }

        CreateBullet(character);

        return true;
    }

    /// <summary>
    /// Crea yba bala y le asigna los valores
    /// </summary>
    private Bullet CreateBullet(Character character, Transform t = null)
    {
        Default(ref t, transform);

        Bullet newBullet = Instantiate(pref_bullet, t.position, Quaternion.LookRotation(t.position), parent_bullet);
        newBullet.SetDirection(transform.rotation);
        newBullet.bulletShot.GetShotOf(character);
        return newBullet;
    }

    IEnumerator CreateFollowBullet(Character character)
    {
        yield return new WaitForSeconds(1);
        CreateBullet(character).canFollow = true;

    }
    #endregion
}
/// <summary>
/// Información acerca de los disparo de balas
/// </summary>
public struct BulletShot {
    public float speed;
    public float range;
    public float damage;
    public CharacterType owner;

    public BulletShot(float speed, float range, float damage, CharacterType owner)
    {
        this.speed  = speed;
        this.range  = range;
        this.damage = damage;
        this.owner = owner;
    }
    
    /// <summary>
    /// Devuelve los datos del disparo de el personaje
    /// </summary>
    public static BulletShot GetShotOf(Character character, BulletShot bulletShot){
        bulletShot.speed = character.atkSpeed + character.speed;
        bulletShot.range = character.range;
        bulletShot.damage = character.damage;
        return bulletShot;
    }
    public BulletShot GetShotOf(Character character) => this = GetShotOf(character, this);
}