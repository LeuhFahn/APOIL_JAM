﻿using UnityEngine;
using System.Collections;

public class GlobalVariable : MonoBehaviour {

	public static GameObject PF_MENU;
	public GameObject pf_Menu;

	public static GameObject PF_CAMERA;
	public GameObject pf_Camera;

	public static GameObject PF_PLAYER_MAN;
	public GameObject pf_PLayerMan;

	public static GameObject PF_PLAYER_WOMAN;
	public GameObject pf_PLayerWoman;

	public static GameObject PF_COEUR;
	public GameObject pf_Coeur;

	public static GameObject PF_FLEUR;
	public GameObject pf_Fleur;

	public static GameObject PF_PETAL;
	public GameObject pf_Petale;

	public static GameObject PF_NOUNOURS;
	public GameObject pf_Nounours;

	public static GameObject PF_NUAGE;
	public GameObject pf_Nuage;

	public static GameObject PF_SOLEIL;
	public GameObject pf_Soleil;

	public static GameObject PF_ARCENCIEL;
	public GameObject pf_ArcEnCiel;

	public static GameObject PF_RAYON;
	public GameObject pf_Rayon;

	public static GameObject PF_LAUNCHER_COEUR;
	public GameObject pf_launcher_Coeur;

	public static GameObject PF_LAUNCHER_FLEUR;
	public GameObject pf_launcher_Fleur;

	public static GameObject PF_LAUNCHER_NOUNOURS;
	public GameObject pf_launcher_Nounours;

	public static GameObject PF_LAUNCHER_RAYON;
	public GameObject pf_launcher_Rayon;

	public static GameObject PF_LAUNCHER_NUAGE;
	public GameObject pf_launcher_Nuage;

	public static GameObject PF_COIN;
	public GameObject pf_Coin;

	public static float F_DISTANCE_AMITIE;
	public float f_distanceAmitie = 500.0f;

	public static float F_TIME_TRANSFORMATION_RAYON_ARC;
	public float f_timeTransformationRayonArc = 2.0f;

	public static float F_TIME_TRANSFORMATION_FLEUR_PETALE;
	public float f_timeTransformationFleurPetale = 2.0f;

	public static float F_FLEUR_DISTANCE_EXPLOSION;
	public float f_fleurDistanceExplosion = 500.0f;

	public static float F_NUAGE_DISTANCE_EXPLOSION;
	public float f_NuageDistanceExplosion = 500.0f;

	public static float F_NOUNOURS_ANGLE_PER_SEC;
	private float f_NounoursAnglePerSec = Mathf.PI / 10.0f;

	public static float F_NOUNOURS_VELOCITY;
	public float f_NounoursVelocity = 100.0f;
	
	public static int N_NB_PETALE;
	public int n_NbPetale = 8;

	public static float F_PLAYER_VELOCITY;
	public float f_playerVelocity = 2.0f;
	
	// Use this for initialization
	void Awake () 
	{
		GlobalVariable.PF_MENU = pf_Menu;
		GlobalVariable.PF_CAMERA = pf_Camera;
		GlobalVariable.PF_PLAYER_MAN = pf_PLayerMan;
		GlobalVariable.PF_PLAYER_WOMAN = pf_PLayerWoman;
		GlobalVariable.PF_COEUR = pf_Coeur;	
		GlobalVariable.PF_FLEUR = pf_Fleur;
		GlobalVariable.PF_PETAL = pf_Petale;		
		GlobalVariable.PF_NOUNOURS = pf_Nounours;		
		GlobalVariable.PF_NUAGE = pf_Nuage;		
		GlobalVariable.PF_SOLEIL = pf_Soleil;		
		GlobalVariable.PF_ARCENCIEL = pf_ArcEnCiel;		
		GlobalVariable.PF_RAYON = pf_Rayon;
		GlobalVariable.PF_LAUNCHER_COEUR = pf_launcher_Coeur;		
		GlobalVariable.PF_LAUNCHER_FLEUR = pf_launcher_Fleur;	
		GlobalVariable.PF_LAUNCHER_NOUNOURS = pf_launcher_Nounours;
		GlobalVariable.PF_LAUNCHER_RAYON = pf_launcher_Rayon;
		GlobalVariable.PF_LAUNCHER_NUAGE = pf_Nuage;
		GlobalVariable.PF_COIN = pf_Coin;
		GlobalVariable.F_DISTANCE_AMITIE = f_distanceAmitie;
		GlobalVariable.F_TIME_TRANSFORMATION_RAYON_ARC = f_timeTransformationRayonArc;
		GlobalVariable.F_TIME_TRANSFORMATION_FLEUR_PETALE = f_timeTransformationFleurPetale;
		GlobalVariable.F_NOUNOURS_ANGLE_PER_SEC = f_NounoursAnglePerSec;
		GlobalVariable.F_NOUNOURS_VELOCITY = f_NounoursVelocity;
		GlobalVariable.F_FLEUR_DISTANCE_EXPLOSION = f_fleurDistanceExplosion;
		GlobalVariable.F_NUAGE_DISTANCE_EXPLOSION = f_NuageDistanceExplosion;
		GlobalVariable.F_PLAYER_VELOCITY = f_playerVelocity;

		GlobalVariable.N_NB_PETALE = n_NbPetale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
