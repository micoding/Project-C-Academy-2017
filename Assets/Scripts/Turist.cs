using System.Collections;
using UnityEngine;

public class Turist : MonoBehaviour {

    public Vector2 movementSpeed;
    Animator anim;
    Rigidbody2D rB;
    GameController gC;
    public float photoTime;
    public bool isAnimal;
    Transform collisionTransform;
    public double cash;
    SpriteRenderer sR;
    string layer;

    private void Awake()
    {
         GameControllerStatic.turists.Add(this);
    }
    // Use this for initialization
    void Start () {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();
        rB = GetComponent<Rigidbody2D>();

        if(GameControllerStatic.WhichLayerTurist())
            layer = "Player1";
        else
            layer = "Player2";

        MoveToLayer(this.transform, layer);

        rB.velocity = movementSpeed;
        gC.income += 100;
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Speed", rB.velocity.x);
    }

    IEnumerator Look()
    {
        Photo(collisionTransform);
        yield return new WaitForSeconds(photoTime);
        rB.velocity = movementSpeed;
    }

    private void OnTriggerExit2D(Collider2D collision)//check is animal is in the fence and turist have money
    {
        collisionTransform = collision.transform;
        string tag = collision.tag;
        if (collisionTransform == gC.fence0)
        {
            if (GameControllerStatic.fence0Available || cash <= 0)
                isAnimal = false;
            else
                isAnimal = true;
        }
        else if (collisionTransform == gC.fence1)
        {
            if (GameControllerStatic.fence1Available || cash <= 0)
                isAnimal = false;
            else
                isAnimal = true;
        }
        else if (collisionTransform == gC.fence2)
        {
            if (GameControllerStatic.fence2Available || cash <= 0)
                isAnimal = false;
            else
                isAnimal = true;
        }
        else if (collisionTransform == gC.fence3)
        {
            if (GameControllerStatic.fence3Available || cash <= 0)
                isAnimal = false;
            else
                isAnimal = true;
        }

        if (tag == "FenceStop" && isAnimal)
        {
            isAnimal = false;
            rB.velocity = new Vector2(0, 0);
            StartCoroutine(Look());
        }
    }

    void Photo(Transform where)//pay for animal
    {
        anim.SetTrigger("Photo");// starts animation
        double health = gC.animals.Find(item => item.where == where).health;
        double atraction = gC.animals.Find(item => item.where == where).atraction;
        if (health * atraction <= cash)
        {
            gC.income += health * atraction * 4;
            cash -= health * atraction * 4;
        }
        else
        {
            gC.income += cash;
            cash = 0;
        }
    }

    void OnBecameInvisible()
    {
        GameControllerStatic.turists.Remove(this);
        Destroy(this.gameObject);
    }

    void MoveToLayer(Transform root, string layer) // recursive func for changing layer in turist
    {
        root.GetComponent<SpriteRenderer>().sortingLayerName = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }
}