using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName ="ScriptableObj/CardData", order = int.MaxValue)]
public class ScriptableCardObject : ScriptableObject
{
    [Header("Card Status")]
    [SerializeField] public string CardName;
    [field:SerializeField] public int Size { get; set; }

    [field: SerializeField] public int Volume { get; set; }

    [field: SerializeField] public int Durability { get; set; }

    [field: SerializeField] public bool Stackable = true;

    [Header("[Card production]")]
    [Tooltip("instanciate production")]
    [SerializeField] public AudioClip instSound;
    [SerializeField] public ParticleSystem instEff;

    [Tooltip("clicked production")]
    [SerializeField] public AudioClip clickedSound;
    [SerializeField] public ParticleSystem clickedEff;

    [Tooltip("drag production")]
    [SerializeField] public AudioClip dragSound;
    [SerializeField] public ParticleSystem dragEff;

    [Tooltip("drop production")]
    [SerializeField] public AudioClip dropSound;
    [SerializeField] public ParticleSystem dropEff;

    [Tooltip("stack production")]
    [SerializeField] public AudioClip stackSound;
    [SerializeField] public ParticleSystem stackEff;

    [Tooltip("destroy production")]
    [SerializeField] public AudioClip destroySound;
    [SerializeField] public ParticleSystem destroyEff;
}
