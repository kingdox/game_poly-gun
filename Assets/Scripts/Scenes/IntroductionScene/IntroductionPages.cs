﻿#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class IntroductionPages : MonoBehaviour
{
    #region Variables
    //[Header("IntroductionPages Setup")]
    private IntroductionPage[] pages;
    private GameObject[] pagesG;
    #endregion
    #region Events
    private void Start()
    {
        PageLoad();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisa si esta correcto las paginas
    /// </summary>
    public void PageLoad(){
        //conocemos el largo de paginas
        int length = transform.childCount;
        //si hay diferencia de longitudes de pagina
        pages = new IntroductionPage[length];
        pagesG = new GameObject[length];

        for (int x = 0; x < length; x++)
        {
            if (pagesG != null) LoadPage(x);
        }
    }

    /// <summary>
    /// Carga la pagina en el arreglo
    /// </summary>
    public void LoadPage(int x){
        pagesG[x] = transform.GetChild(x).gameObject;
        pages[x] = pagesG[x].GetComponent<IntroductionPage>();
        pagesG[x].SetActive(false);
    }

    /// <summary>
    /// Actualiza la pagina 
    /// </summary>
    public void ReloadPage(int i)
    {
        pages[i].ReloadPage();
    }

    public GameObject[] GetObjectsRef() => pagesG;
    #endregion
}