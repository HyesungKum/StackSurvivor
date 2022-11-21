using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : AdditionalFunc
{
    [SerializeField] AudioSource sound = null;
    public override void Do()
    {
        sound.Play();
    }
}
