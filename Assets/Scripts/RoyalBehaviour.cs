using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalBehaviour : MonoBehaviour
{
    public static RoyalBehaviour Instance { get; private set; }

    public TextMesh m_debugTextSTA;
    public TextMesh m_debugTextJOY;
    public float CarriageSpeed;

    public int Stamina
    {
        get
        {
            return (int)Mathf.Round(m_stamina);
        }
    }

    public int Joy
    {
        get
        {
            return (int)Mathf.Round(m_joy);
        }
    }

    float m_stamina;
    float m_joy;

    void Start()
    {
        Instance = this;
        m_stamina = 100;
        m_joy = 20;  	
	}

    private void OnDestroy()
    {
        Instance = null;
    }

    void Update()
    {
        UpdateStamina();
        DecrementJoy();
        
        if (m_debugTextSTA)
        {
            m_debugTextSTA.text = string.Format("STAMINA: {0:f1}", Stamina);
        }

        if (m_debugTextJOY)
        {
            m_debugTextJOY.text = string.Format("JOY: {0:f1}", Joy);
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

    const float joyPerSecond = 2;
    const float maxJoy = 100f;

    // Needed for the UI bar?
    const float joyPerPixel = 0.02f;

    public void AddJoy( float amountOfJoy )
    {
        m_joy = Mathf.Clamp( m_joy + amountOfJoy, 0, maxJoy);
    }

    void DecrementJoy()
    {
        m_joy = Mathf.Clamp( m_joy - joyPerSecond * Time.deltaTime , 0, maxJoy);
    }
}
