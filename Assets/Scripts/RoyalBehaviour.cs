using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalBehaviour : MonoBehaviour
{
    public static RoyalBehaviour Instance { get; private set; }

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
        Instance = this;
        m_stamina = 100;	
	}

    private void OnDestroy()
    {
        Instance = null;
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

    public bool HasEnoughStaminaForChange(float change)
    {
        return StaminaNeededForChange(change) < m_stamina;
    }

    float StaminaNeededForChange(float change)
    {
        return Mathf.Abs(change) * staminaPerPixel;
    }

    void UpdateStamina()
    {
        float change = InputSystem.Instance.Change;

        m_stamina = Mathf.Clamp(m_stamina + staminaPerSecond*Time.deltaTime - StaminaNeededForChange(change), 0, 100);	
    }
}
