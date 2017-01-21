using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

    public static InputSystem Instance { get; private set; }
     
    public GameObject m_cursor;
    public TextMesh m_debugText;

    Camera m_camera;

    private void OnEnable()
    {
        InputSystem.Instance = this;

        Cursor.visible = false;
        m_camera = Camera.main;
        m_prevPos = Input.mousePosition;
        m_halfWaveDone = 0;
        m_accumulatedChange = 0;
        m_wavingTime = 0;
    }

    private void OnDisable()
    {
        InputSystem.Instance = null;
    }

    void Update()
    {
        DetectWave();

        if (m_cursor)
        {
            Vector3 worldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            m_cursor.transform.position = new Vector3(worldPos.x, worldPos.y);
        }

        if (m_debugText)
        {
            if (IsWaving)
                m_debugText.text = string.Format("Waving! t:{0:f1}", m_wavingTime);
            else
                m_debugText.text = string.Format("change={0} hw:{1}", m_accumulatedChange, m_halfWaveDone);

        }
	}

    public bool IsWaving { get; private set; }
    public float Change { get { return m_change; } }
 
    const float minWave = 60;

    Vector3 m_prevPos;
    float m_change;

    float m_accumulatedChange;
    float m_halfWaveDone;

    float m_wavingTime;

    void DetectWave()
    {
        Vector3 mousePos = Input.mousePosition;
        m_change = mousePos.x - m_prevPos.x;
        if (Mathf.Sign(m_change) != Mathf.Sign(m_accumulatedChange))
            m_accumulatedChange = 0;
        m_accumulatedChange += m_change;
        m_prevPos = mousePos;

        if (Mathf.Abs(m_accumulatedChange) >= minWave)
        {
            if (m_halfWaveDone != 0 && Mathf.Sign(m_halfWaveDone) != Mathf.Sign(m_accumulatedChange))
            {
                IsWaving = true;
                m_wavingTime = 0.5f;
                m_halfWaveDone = 0;
            }
            else
            {
                m_halfWaveDone = Mathf.Sign(m_accumulatedChange);
            }
        }
        
        if (IsWaving)
        {
            m_wavingTime -= Time.deltaTime;
            if (m_wavingTime <= 0)
                IsWaving = false;
        }
    }
}
