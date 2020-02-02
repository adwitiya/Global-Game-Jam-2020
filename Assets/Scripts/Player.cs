using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerCanvas inventory;
    public Transform menuCanvas;
    public AboutMenu menuController;

    public CharController ctrl;

    public Transform interactPrompt;

    public float runSpeed = 40f;
    public float climbSpeed = 2f;
    public float throwStrenght = 5;

    private float horizontalMove = 0;
    private float verticalMove = 0;

    private bool jump = false;

    private bool liftThrowPressed = false;

    public List<Transform> wires;
    private int wiresCollected = 0;
    private int wiresUsed = 0;

    public List<Transform> sounds;
    private int brokenSoundsCollected = 0;
    private int fixedSoundsCollected = 0;
    private int fixedSoundsUsed = 0;

    public List<Transform> textures;
    private Transform holdedTexture = null;

    public float interactDistance = 1.5f;
    public float collectDistance = 1f;

    public Transform grayScale;

    private Transform currentInteractible = null;
    private bool interacting = false;

    private bool climbing = false;

    private float myMass;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerCanvas>();
        SetGrayscale(true);
        menuCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {

            if (menuCanvas.gameObject.activeSelf)
            {
                menuCanvas.gameObject.SetActive(false);
                menuController.resume_game();
            }
            else
            {
                menuCanvas.gameObject.SetActive(true);
            }

        }

        if (menuCanvas.gameObject.activeSelf == true)
        {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("LiftThrow"))
        {
            liftThrowPressed = true;
        }
        if (holdedTexture != null)
        {
            holdedTexture.position = transform.position + (transform.forward + transform.up);
        }

        if (Input.GetKeyDown("p"))
        {
            SetGrayscale(!grayScale.gameObject.activeSelf);
        }

        if (Input.GetKeyDown("e"))
        {
            interacting = true;
        }
    }


    void FixedUpdate()
    {

        if (menuCanvas.gameObject.activeSelf == true)
        {
            return;
        }
        if (climbing)
        {
            transform.position = transform.position + Vector3.up * verticalMove * Time.fixedDeltaTime;
           // rb.MovePosition(rb.position + Vector2.up * verticalMove * Time.fixedDeltaTime);
        }
        
        if (verticalMove < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

            // If it hits something...
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.transform.gameObject.tag);
                if (hit.collider.transform.gameObject.tag == "PassThrough")
                {
                    Debug.Log("hey");
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
                }
            }
        }

        ctrl.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

        Transform texture = pickableTexture();
        if (texture != null && liftThrowPressed && holdedTexture == null)
        {
            Debug.Log("Picked");
            holdedTexture = texture;
        }
        else if (liftThrowPressed && holdedTexture != null)
        {
            Debug.Log("Thrown");
            Vector2 throwForce = new Vector2(ctrl.intFaceDir(), 1) * throwStrenght;
            holdedTexture.GetComponent<Rigidbody2D>().AddForce(throwForce, ForceMode2D.Impulse);//(ctrl.intFaceDir() + transform.up) * throwStrenght);
            holdedTexture = null;
        }



        liftThrowPressed = false;


        if (interactPrompt.gameObject.activeSelf && Mathf.Sign(interactPrompt.lossyScale.x) < 0 )
            interactPrompt.localScale = new Vector3(interactPrompt.localScale.x*-1, interactPrompt.localScale.y, interactPrompt.localScale.z);

        if (interacting && currentInteractible)
        {
            if (currentInteractible.name == "TV" && wiresCollected > 0)
            {
                wiresCollected--;
                wiresUsed++;
                inventory.setWires(wiresCollected);
                if (wiresUsed == 3)
                {
                    SetGrayscale(false);
                }
            }
            else if (currentInteractible.name == "SoundFix" && brokenSoundsCollected > 0)
            {
                brokenSoundsCollected--;
                fixedSoundsCollected++;
                inventory.setBrokenSounds(brokenSoundsCollected);
                inventory.setFixedSounds(fixedSoundsCollected);
            }
            else if (currentInteractible.name == "Ipod" && fixedSoundsCollected > 0)
            {
                fixedSoundsCollected--;
                fixedSoundsUsed++;
                inventory.setFixedSounds(fixedSoundsCollected);
            }
            else if (currentInteractible.name == "EmptyDoor")
            {
                Debug.Log("Something is missing here");
            }
            else if (currentInteractible.name == "Door")
            {
                Debug.Log("YOU WIN");
                Application.Quit();
            }
        }

        interacting = false;
    }

    private Transform pickableTexture()
    {
        Transform texture = null;
        float minDistance = 90000;

        foreach (Transform t in textures)
        {
            float curDistance = Vector2.Distance(transform.position, t.position);
            //Debug.Log(Vector3.Distance(transform.position, t.position));
            if (curDistance < interactDistance)
                if (texture == null || curDistance < minDistance)
                {
                    minDistance = curDistance;
                    texture = t;
                }
        }

        return texture;
    }

    private Transform collectibleSound()
    {
        Transform sound = null;
        float minDistance = collectDistance;

        foreach (Transform s in sounds)
        {
            float curDistance = Vector2.Distance(transform.position, s.position);
            //Debug.Log(Vector3.Distance(transform.position, t.position));
            if (curDistance < interactDistance)
                if (sound == null || curDistance < minDistance)
                {
                    minDistance = curDistance;
                    sound = s;
                }
        }

        return sound;
    }

    
    private void SetGrayscale(bool on)
    {
        //bool isGrayScale = grayScale.gameObject.activeSelf;
        grayScale.gameObject.SetActive(on);
        inventory.SetGrayscale(on);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.transform.tag == "Sound")
        {
            brokenSoundsCollected += 1;
            col.transform.gameObject.SetActive(false);
            inventory.setBrokenSounds(brokenSoundsCollected);
        }
        else if (col.transform.tag == "Wire")
        {
            wiresCollected += 1;
            col.transform.gameObject.SetActive(false);
            inventory.setWires(wiresCollected);
        }
        else if (col.transform.tag == "Interactible")
        {
            if (Mathf.Sign(interactPrompt.lossyScale.x) < 0)
                interactPrompt.localScale = new Vector3(interactPrompt.localScale.x * -1, interactPrompt.localScale.y, interactPrompt.localScale.z);
            interactPrompt.gameObject.SetActive(true);
            currentInteractible = col.transform;
        }
        else if (col.transform.tag == "Mushroom")
        {
            col.transform.gameObject.SetActive(false);
            transform.localScale *= 1.5f;
            ctrl.multiplyJumpBy(1.3f);
        }
        else if (col.transform.tag == "Ladder")
        {
            climbing = true;
            //myMass = rb.mass;
            //rb.mass = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Interactible")
        {
            interactPrompt.gameObject.SetActive(false);
            if (currentInteractible.name == col.transform.name)
                currentInteractible = null;
        }
        else if (col.transform.tag == "Ladder")
        {
            climbing = false;
            //rb.mass = myMass;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
