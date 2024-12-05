using UnityEngine;

public class Movement : MonoBehaviour {

    public Vector2 movementSpeed;
    Rigidbody2D rB;
    SpriteRenderer sR;
    bool flip;

    // Use this for initialization
    void Start () {
        rB = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        float randVal= Random.value;
        if (randVal <= 0.5)
            flip = true;
        else
            flip = false;
        sR.flipX = flip;
        rB.velocity = movementSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}