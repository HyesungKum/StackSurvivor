using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.ParticleSystemJobs;

#region summary
/// <summary>
/// any production delegate     
/// if you want customed or plus additional function in this card, you should be make script about inheritance additional class
/// you must scripting additional abstract part and chain eahc production
/// </summary>
#endregion
public delegate void Production();

#region summary
/// <summary>
/// card tatal status
/// Name, Size, Volume, Druabilty, durability
/// </summary>
#endregion
[Serializable] public class Data
{
    #region summary
    /// <summary>
    /// card name for ui view
    /// </summary>
    #endregion
    public string CardName;
    #region summary
    /// <summary>
    /// card size for stacking 
    /// ex) can stackable zombie 13 -> building 100
    /// ex) cannot stackable zombie 13 -x> backpack 5 Size excess
    /// </summary>
    #endregion
    public int Size;
    #region summary
    /// <summary>
    /// Volume for Stacking can number of stackable Card
    /// ex) able backpack volume 3 {bottle, weapon, flashright}
    /// ex) disalbe backpack volume3 {ant, ant, ant, ant<-volume excess! }
    /// </summary>
    #endregion
    public int Volume;
    #region summary
    /// <summary>
    /// card health for can exist in game board
    /// ex) zombie burabilit have lower durability than 0 zombie was destroied
    /// </summary>
    #endregion
    public int Durability;
    #region summary
    /// <summary>
    /// this card cannot make dec when boolian true 
    /// ex) car stackable = true, zombie stackable = false
    /// </summary>
    #endregion
    public bool Stackable;

    #region summary
    /// <summary>
    /// this list was card obj data inheritance class object was stacked
    /// </summary>
    #endregion
    public List<BaseCard> Dec = new List<BaseCard>();
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class BaseCard : MonoBehaviour
{
    protected Data data = new();

    [Tooltip("Card Status and basic sfx,vfx")]
    [SerializeField] ScriptableCardObject inputBasicData;

    [Tooltip("intial base dec")]
    #region summary
    /// <summary>
    /// must used this property when wanna beforehand stacking basedec, 
    /// do not ues this property stacking in game
    /// </summary>
    #endregion
    [field: SerializeField] protected List<BaseCard> BaseDeck = null;
    #region summary
    /// <summary>
    /// soundplayer for card
    /// </summary>
    #endregion
    protected AudioSource cardSoundPlayer = null;

    #region basic sound data
    protected AudioClip instSound;
    protected AudioClip clickedSound;
    protected AudioClip dragSound;
    protected AudioClip dropSound;
    protected AudioClip stackSound;
    protected AudioClip destroySound;
    #endregion

    #region basic eff data
    protected ParticleSystem instEff;
    protected ParticleSystem clickedEff;
    protected ParticleSystem dragEff;
    protected ParticleSystem dropEff;
    protected ParticleSystem stackEff;
    protected ParticleSystem destroyEff;
    #endregion

    //additional func chain
    protected Production instProd = null;
    protected Production clickedProd = null;
    protected Production dragProd = null;
    protected Production dropProd = null;
    protected Production stackProd = null;
    protected Production destroyProd = null;

    [Header("additional function")]
    [SerializeField] public List<AdditionalFunc> plusInstFunc = null;
    [SerializeField] public List<AdditionalFunc> plusClickedFunc = null;
    [SerializeField] public List<AdditionalFunc> plusDragFunc = null;
    [SerializeField] public List<AdditionalFunc> plusDropFunc = null;
    [SerializeField] public List<AdditionalFunc> plusStackFunc = null;
    [SerializeField] public List<AdditionalFunc> plusDestroyFunc = null;

    protected void Awake()
    {
        cardSoundPlayer = this.GetComponent<AudioSource>();

        #region stacking initial dec
        if (BaseDeck != null)
        {
            for (int i = 0; i < BaseDeck.Count; i++)
            {
                data.Dec.Add(BaseDeck[i]);
            }
        }
        #endregion

        #region scriptable data connection
        data.CardName = inputBasicData.CardName;
        data.Size = inputBasicData.Size;
        data.Volume = inputBasicData.Volume;
        data.Durability = inputBasicData.Durability;
        data.Stackable = inputBasicData.Stackable;

        instSound    = inputBasicData.instSound;
        clickedSound = inputBasicData.clickedSound;
        dragSound    = inputBasicData.dragSound;
        dropSound    = inputBasicData.dropSound;
        stackSound   = inputBasicData.stackSound;
        destroySound = inputBasicData.destroySound;

        instEff    = inputBasicData.instEff;
        clickedEff = inputBasicData.clickedEff;
        dragEff    = inputBasicData.dragEff;
        dropEff    = inputBasicData.dropEff;
        stackEff   = inputBasicData.stackEff;
        destroyEff = inputBasicData.destroyEff;
        #endregion

        #region delegate chain
        for (int i = 0; i < plusInstFunc.Count; i++)
        {
            instProd += plusInstFunc[i].Do;
        }
        for (int i = 0; i < plusClickedFunc.Count; i++)
        {
            clickedProd += plusClickedFunc[i].Do;
        }
        for (int i = 0; i < plusDragFunc.Count; i++)
        {
            dragProd += plusDragFunc[i].Do;
        }
        for (int i = 0; i < plusDropFunc.Count; i++)
        {
            dropProd += plusDropFunc[i].Do;
        }
        for (int i = 0; i < plusStackFunc.Count; i++)
        {
            stackProd += plusStackFunc[i].Do;
        }
        for (int i = 0; i < plusDestroyFunc.Count; i++)
        {
            destroyProd += plusDestroyFunc[i].Do;
        }
        #endregion

        #region initial rigidbody setting
        this.GetComponent<Rigidbody>();
        this.GetComponent<BoxCollider>();
        #endregion
    }

    #region summary
    /// <summary>
    /// call when card instantiate or visible
    /// </summary>
    #endregion
    protected void OnEnable()
    {
        if (instSound != null)
            cardSoundPlayer.clip = instSound;

        if (instEff != null)
            Instantiate(instEff, this.transform);

        if (instProd != null)
            instProd();
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked();
        }
    }

    //카드가 클릭되었을 때
    //카드안에 스택되어있는 카드를 보여준다
    #region summary
    /// <summary>
    /// call when card clicked mouse button
    /// </summary>
    #endregion
    protected void OnClicked()
    {
        if (clickedSound != null)
            cardSoundPlayer.clip = instSound;

        if (clickedEff != null)
            Instantiate(clickedEff, this.transform);

        if (clickedProd != null)
            clickedProd();
    }
    
    //카드를 클릭한 상태로 움직이면
    //카드가 움직일 수 있는 상태라면 마우스를 따라서 움직이고
    #region summary
    /// <summary>
    /// call when dragable card move
    /// </summary>
    #endregion
    protected void OnDrag()
    {
        if (dragSound != null)
            cardSoundPlayer.clip = dragSound;

        if (dragEff != null)
            Instantiate(dragEff, this.transform);

        if (clickedProd != null)
            dragProd();
    }
    #region summary
    /// <summary>
    /// call when draable card dorp gameboard
    /// </summary>
    #endregion
    protected void OnDrop()
    {
        if (dropSound != null)
            cardSoundPlayer.clip = dropSound;

        if (dropEff != null)
            Instantiate(dropEff, this.transform);

        if (dropProd != null)
            dropProd();
    }
    #region summary
    /// <summary>
    /// call when card stacking other card
    /// </summary>
    #endregion
    protected void OnStack()
    {
        if (stackSound != null)
            cardSoundPlayer.clip = stackSound;

        if (stackEff != null)
            Instantiate(stackEff, this.transform);

        if (stackProd != null)
            stackProd();
    }
    #region summary
    /// <summary>
    /// call when this card destroy 
    /// main routine is destroy process coroutine
    /// </summary>
    #endregion
    protected void OnCardDestroy()
    {
        StartCoroutine(destroyProcess());
    }
    protected IEnumerator destroyProcess()
    {
        if (destroySound != null)
            cardSoundPlayer.clip = destroySound;

        if (destroyEff != null)
            yield return Instantiate(destroyEff, this.transform);

        if (destroyProd != null)
            yield return destroyProd;

        Destroy(this.gameObject);
    }
}
