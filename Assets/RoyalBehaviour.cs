using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalBehaviour : MonoBehaviour
{
    public TextMesh m_debugText;

    public int Stamina
    {
        get
        {
            return (int)Mathf.Round(m_stamina);
        }
    }

    float m_stamina;

    void Start()
    {
        m_stamina = 100;	
	}

	void Update()
    {
        UpdateStamina();
        
        if (m_debugText)
        {
            m_debugText.text = string.Format("STAMINA: {0:f1}", Stamina);
        }
	}

    const float staminaPerSecond = 5;
    const float staminaPerPixel = 0.02f;

    void UpdateStamina()
    {
        float change = InputSystem.Instance.Change;

        m_stamina = Mathf.Clamp(m_stamina + staminaPerSecond*Time.deltaTime - Mathf.Abs(change) * staminaPerPixel, 0, 100);	
    }
}
