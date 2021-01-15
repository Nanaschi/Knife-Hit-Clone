using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] int availableKnives;

    public int AvailableKnives { get => availableKnives;  }



    [SerializeField] Sprite firstWheel;
    [SerializeField] Sprite secondWheel;
    [SerializeField] Sprite thirdWheel;

    [SerializeField] bool isBoss;

    [Header("Prefabs:")]

    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject knifePrefab;

    [Header("Settings:")] 
    [SerializeField] float rotationZ;
   
    public List<Level> levels;

    [HideInInspector]
    public List<Knife> knives;


    private int levelIndex;

    public void DestroyKnife()
    {
        foreach (var knife in knives)
        {
            Destroy(knife.gameObject);
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isBoss)
        {
            if (GameManager.Instance.Stage < 5)
            {
                GetComponent<SpriteRenderer>().sprite = firstWheel;
            } else if (GameManager.Instance.Stage > 5 && GameManager.Instance.Stage < 10){
                GetComponent<SpriteRenderer>().sprite = secondWheel;
            }
            else if (GameManager.Instance.Stage > 5 && GameManager.Instance.Stage > 10)
            {
                GetComponent<SpriteRenderer>().sprite = thirdWheel;
            }
        }
     

        levelIndex = UnityEngine.Random.Range(0, levels.Count);
        RotateWheel();

        if (levels[levelIndex].appleChance > UnityEngine.Random.value)
        {
            SpawnApple();
        }
        SpawnKnives();

    }


    private void Update()
    {
        RotateWheel();
    }
    public void RotateWheel()
    {
        transform.Rotate(0f, 0f, rotationZ * Time.deltaTime);
    }


    private void SpawnKnives()
    {
        foreach (float knifeAngle in levels[levelIndex].knifeAngleFromWheel)
        {
            GameObject knifeTemporary = Instantiate(knifePrefab);
            knifeTemporary.transform.SetParent(transform);
            SetRotationFromWheel(transform, knifeTemporary.transform, knifeAngle, 0.25f, 180);

            knifeTemporary.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);


        }
    }
    private void SpawnApple()
    {
        foreach (float appleAngle in levels[levelIndex].appleAngleFromWheel)
        {
            GameObject appleTemporary = Instantiate(applePrefab);
            appleTemporary.transform.SetParent(transform);
            SetRotationFromWheel(transform, appleTemporary.transform, appleAngle, 0.25f, 0f);
            appleTemporary.transform.localScale = Vector3.one;
        }
    }

 

    public void SetRotationFromWheel (Transform wheel, Transform objectToPlace, float angle, float spaceFromObject, float objectRotation)
    {
        Vector2 offSet = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (wheel.GetComponent<CircleCollider2D>().radius + spaceFromObject);
        objectToPlace.localPosition = (Vector2) wheel.localPosition + offSet;
        objectToPlace.localRotation = Quaternion.Euler(0, 0, -angle + objectRotation);

    }

    public void KnifeHit(Knife knife)
    {
        knife.myRigidbody.isKinematic = true;
        knife.myRigidbody.velocity = Vector2.zero;
        knife.transform.SetParent(transform);
        knife.hit = true;



        knives.Add(knife);
        if (knives.Count >= availableKnives)
        {
            LevelManager.Instance.NextLevel();
        }

        GameManager.Instance.Score++;
    }


}

[Serializable]
public class Level
{
    [UnityEngine.Range(0, 1)] [SerializeField] public float appleChance;

    public List<float> appleAngleFromWheel = new List<float>();
    public List<float> knifeAngleFromWheel = new List<float>();
}
