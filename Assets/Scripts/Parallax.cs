using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public float ParallaxSpeed = 0;

    private GameObject m_copy;
    private Vector2 m_movement;
    private Vector2 m_initialPosition;
    private float m_width;

    void Start()
    {
        // Don't do this in AWAKE(), Unity no like
        m_width = GetComponent<Renderer>().bounds.extents.x * 2;
        m_initialPosition = transform.position;
        m_copy = Instantiate(gameObject, new Vector3(transform.position.x + m_width, transform.position.y, transform.position.z), transform.rotation);

        Destroy(m_copy.GetComponent<Parallax>());
    }

    // Update is called once per frame
    void Update ()
    {
        m_movement = Vector2.left * ParallaxSpeed / 10 * Time.deltaTime;

        m_copy.transform.Translate( m_movement );
        transform.Translate( m_movement );

        if ( transform.position.x + m_width < m_initialPosition.x )
        {
            transform.Translate(Vector3.right *  m_width);
            m_copy.transform.Translate(Vector3.right * m_width);
        }
    }
}
