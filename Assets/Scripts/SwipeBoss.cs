using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeBoss : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    bool isStart = true;
    bool isRight = false;
    bool isUp = false;
    ShakeCamera shakeCamera;
    public Animator animator;
    public GameObject panelLose;
    public GameObject panelWin;
    public Animator girlAnim;
    [SerializeField]
    private SaveController _saveController;
    Shop shop; 
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //shop.coins = PlayerPrefs.GetInt("SaveCoins");
        panelLose.SetActive(false);
        panelWin.SetActive(false);
        shakeCamera = GameObject.FindGameObjectWithTag("ShakeScreen").GetComponent<ShakeCamera>();
        if(isStart == true)
        {
            animator.SetTrigger("isFirstLevel");
            StartCoroutine(isStartTouch());
        }
    }

    IEnumerator isStartTouch()
    {
        yield return new WaitForSeconds(2);
        isStart = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isStart == false)
        {
            if (Mathf.Abs(eventData.delta.x) > (Mathf.Abs(eventData.delta.y)))
            {       
                if (eventData.delta.x > 0)
                {
                    isRight = true;

                }
                else if(isRight == true && eventData.delta.x < 0)
                {
                    shakeCamera.camShake();
                    isStart = true;
                    girlAnim.SetTrigger("isLose");
                    StartCoroutine(panelLoseActivation());                
                    return;
                }
                else if (isUp == true && eventData.delta.x < 0 )
                {
                    isUp = false;
                    girlAnim.SetTrigger("isWin");
                    isStart = true;
                    StartCoroutine(panelWinActivation());                  
                    return;
                }
            } else
            {
                if (isRight == true && eventData.delta.y > 0)
                {
                    isRight = false;
                    isUp = true;
                }
            }
        }
    }

    IEnumerator panelLoseActivation()
    {
        yield return new WaitForSeconds(1.6f);
        panelLose.SetActive(true);
    }
    IEnumerator panelWinActivation()
    {
        yield return new WaitForSeconds(1.6f);
        panelWin.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
