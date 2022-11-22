using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName ="ScriptableObj/CardData", order = int.MaxValue)]
public class ScriptableCardObject : ScriptableObject
{
    [Header("Card Status")]

    [Tooltip("Card name to show")]
    [SerializeField] public string CardName;

    [Tooltip("card size for stacking \n" +
             "ex) can stackable zombie 13 -> building 100\n" +
             "ex) cannot stackable zombie 13 -x> backpack 5 Size excess")]
    [SerializeField] public int Size;

    [Tooltip("Volume for Stacking can number of stackable Card\n" +
        "ex) able backpack volume 3 \n {bottle, weapon, flashright}\n" +
        "ex) disalbe backpack volume3 \n{ant, ant, ant, ant<-volume excess! }")]
    [SerializeField] public int Volume;

    [Tooltip("card health for can exist in game board\n" +
        "ex) zombie burabilit have lower durability than 0 zombie was destroied")]
    [SerializeField] public int Durability;

    [Tooltip("this card cannot make dec when boolian true\n" +
        "ex) car stackable = true, \n zombie stackable = false")]
    [SerializeField] public bool Stackable;

    [Header("[Card production]")]
    [Tooltip("instanciate production")]
    [SerializeField] public AudioClip instSound;
    [SerializeField] public GameObject instEff;

    [Tooltip("clicked sound production")]
    [SerializeField] public AudioClip clickedSound;
    [Tooltip("clicked eff production")]
    [SerializeField] public GameObject clickedEff;

    [Tooltip("drag sound production")]
    [SerializeField] public AudioClip dragSound;
    [Tooltip("darg eff production")]
    [SerializeField] public GameObject dragEff;

    [Tooltip("drop sound production")]
    [SerializeField] public AudioClip dropSound;
    [Tooltip("drop eff production")]
    [SerializeField] public GameObject dropEff;

    [Tooltip("stack sound production")]
    [SerializeField] public AudioClip stackSound;
    [Tooltip("stack eff production")]
    [SerializeField] public GameObject stackEff;

    [Tooltip("destroy sound production")]
    [SerializeField] public AudioClip destroySound;
    [Tooltip("destroy eff production")]
    [SerializeField] public GameObject destroyEff;
}
