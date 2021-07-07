using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;
    Animator animator;
    Vector3 startGamePosition;
    Quaternion startGameRotation;
    float laneOffset;
    public float laneChangeSpeed = 15;
    Rigidbody rb;
    Vector3 targetVelocity;
    float pointStart;
    float pointFinish;
    bool isMoving = false;
    Coroutine movingCoroutine;
    float lastVectorX;
    bool isJumping = false;
    float jumpPower = 15;
    float jumpGravity = -40;
    float realGravity = -9.8f;
    public int coins = 0;
    public Text coinCount;
    public GameObject BuyScreen;
    bool isExtralife = false;
    public Button buy;
    public bool isBonus = false;
    public GameObject heart;
    public GameObject BossPanel;
    private void Awake()
    {
        instance = this;
        isExtralife = false;
    }
    private void Start() {
        BossPanel.SetActive(false);
        heart.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StartPlay());
        BuyScreen.SetActive(false);
        laneOffset = MapGenerator.instance.laneOffset;
        
        startGamePosition = transform.position;
        startGameRotation = transform.rotation;
       
        SwipeManager.instance.MoveEvent += MovePlayer;        
    }

    IEnumerator StartPlay()
    {
        transform.Rotate(0, 180, 0);
        animator.SetBool("isAttack",true);
        yield return new WaitForSeconds(2);
        transform.Rotate(0 , -180 , 0);
        animator.SetBool("isAttack", false);
        StartGame();
    }
    void MovePlayer(bool[] swipes)
    {

        if (swipes[(int)SwipeManager.Direction.Left] && pointFinish > -laneOffset)
        {
            MoveHorizontal(-laneChangeSpeed);
        }
        if (swipes[(int)SwipeManager.Direction.Right] && pointFinish < laneOffset)
        {
            MoveHorizontal(laneChangeSpeed);
        }
        if (swipes[(int)SwipeManager.Direction.Up] && isJumping == false)
        {
            Jump();
        }
    }

    
    void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());
    }

    IEnumerator StopJumpCoroutine()
    {
        do
        {
            yield return new WaitForSeconds(0.02f);
        } while (rb.velocity.y != 0);
        isJumping = false;
        Physics.gravity = new Vector3(0, realGravity, 0);
    }

    void MoveHorizontal(float speed)
    {
        animator.applyRootMotion = false;
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * laneOffset;
        if (isMoving) 
        { 
            StopCoroutine(movingCoroutine);
            isMoving = false;
        }
        movingCoroutine = StartCoroutine(MoveCoroutine(speed));  
    }

    IEnumerator MoveCoroutine(float vectorX)
    {
        isMoving = true;
        while(Mathf.Abs(pointStart - transform.position.x) < laneOffset) 
        {
            yield return new WaitForFixedUpdate();

            rb.velocity = new Vector3(vectorX, rb.velocity.y, 0);
            lastVectorX = vectorX;
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);
        if(transform.position.y > 1)
        {
            rb.velocity = new Vector3(rb.velocity.x, -10, rb.velocity.z);
        }
        isMoving = false;
    }

    public void StartGame()
    {
        
        coins = PlayerPrefs.GetInt("SaveCoins");
        Debug.Log(coins);
        coinCount.text = coins.ToString();
        animator.SetBool("isRun", true);
        GeneratorRoad.instance.StartLevel();
    
    }

    public void ResetLevel()
    {
        isExtralife = false;
        Debug.Log(isExtralife);
        rb.velocity = Vector3.zero;
        pointStart = 0;
        pointFinish = 0;
        animator.applyRootMotion = true;
        animator.SetBool("isRun", false);
        transform.position = startGamePosition;
        transform.rotation = startGameRotation;
        GeneratorRoad.instance.ResetLevel();
        SceneManager.LoadScene(0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ramp")){
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
        if (other.gameObject.CompareTag("StartBoss"))
        {
            BossPanel.SetActive(true);
            StartCoroutine(BossStart());
        }
        if (other.gameObject.CompareTag("Lose"))
        {
            if (isBonus == false)
            {
                if (isExtralife == true)
                {
                    PlayerPrefs.SetInt("SaveCoins", coins);
                    ResetLevel();
                    isExtralife = false;
                    Debug.Log(isExtralife);
                }
                else if (isExtralife == false)
                {
                    isExtralife = true;
                    coins = PlayerPrefs.GetInt("SaveCoins");
                    Time.timeScale = 0f;
                    BuyScreen.SetActive(true);

                    if (coins >= 200)
                    {
                        buy.interactable = true;
                    }
                    else
                    {
                        buy.interactable = false;
                    }
                }
            }else if (isBonus == true)
            {
                Debug.Log("Потратили жизнь");
                heart.SetActive(false); 
                isBonus = false;
                
            }


        }
        if (other.gameObject.CompareTag("Food"))
        {
            coins = PlayerPrefs.GetInt("SaveCoins");
            coins ++;
            PlayerPrefs.SetInt("SaveCoins", coins);
            coinCount.text = coins.ToString();

        }
        if (other.gameObject.CompareTag("Bonus"))
        {
            heart.SetActive(true);
        }
    }

    IEnumerator BossStart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

    public void onBuy()
    {
        coins = PlayerPrefs.GetInt("SaveCoins");
        Debug.Log(coins);
        coins -= 200;
        PlayerPrefs.SetInt("SaveCoins", coins);
         Debug.Log(coins);
        coinCount.text = coins.ToString();
        BuyScreen.SetActive(false);
        Time.timeScale = 1f;

    }

    public void onMenu()
    {
        ResetLevel();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ramp"))
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        if (collision.gameObject.CompareTag("NotLose"))
        {
            MoveHorizontal(-lastVectorX);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RampPlane"))
        {
            if(rb.velocity.x == 0 && isJumping == false)
            {
                rb.velocity = new Vector3(rb.velocity.x, -10, rb.velocity.z);
            }
        }
    }
}
