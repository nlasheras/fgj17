using UnityEngine;

public class Peasant : MonoBehaviour {

    // Movement
    public Vector2 Direction = Vector2.left;

    // Happiness
    public float HappinessTickRate = 1f;
    public float HappinessAmount = 1f;
    public GameObject HappinessText;

    // Sprites
    public Sprite HappySprite;
    public Sprite NeutralSprite;

    private float lastTick;

    private Vector2 m_movement;
    private SpriteRenderer m_spriteRenderer;
    
    // Use this for initialization
    void Awake()
    {     
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        // Peasants entering the view are not pleased right away ;(
        lastTick = Time.time;
        m_spriteRenderer.flipX = false;
        SetToNeutral();
    }

    private void SetToNeutral()
    {
        m_spriteRenderer.sprite = NeutralSprite;
    }

    // Update is called once per frame
    void Update () {

        Move();
        
        if (InputSystem.Instance.IsWaving && (Time.time > HappinessTickRate + lastTick))
        {
            GenerateHappiness();
        }
    }

    private void GenerateHappiness()
    {
        Debug.Log(gameObject.name+": I AM SO HAPPY!");
        m_spriteRenderer.sprite = HappySprite;

        // Happiness amount added popup text
        Vector3 textPosition = new Vector3( transform.position.x, transform.position.y + 1 );
        Instantiate( HappinessText, textPosition, transform.rotation );

        Invoke("SetToNeutral", HappinessTickRate * 0.5f);
        lastTick = Time.time;       
    }

    public void Move()
    {
        m_movement = Direction * RoyalBehaviour.Instance.CarriageSpeed * Time.deltaTime;
        transform.Translate( m_movement, Space.World );

        if (transform.position.x < 0)
        {
           m_spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER TRIGGER");
    }

    void OnTriggerExit2D(Collider2D other)
    {
       if (other.CompareTag("PeasantKiller"))
        {
            gameObject.SetActive( false );
        }
    }
}
