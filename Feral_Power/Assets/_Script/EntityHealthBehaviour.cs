using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthBehaviour : MonoBehaviour
{
    public float CurrentHealth = 1;
    public float MaxHealth = 1;
    public bool IFrame = false;

    // The duration counts in frame instead of second
    // It's call I-FRAME for a reason
    public int IFrameDuration = 30;
    private int FrameSinceDamage = 0;
    private void FixedUpdate()
    {
        if(FrameSinceDamage >= IFrameDuration)
        {
            IFrame = false;
        }

        if(IFrame)
        {
            FrameSinceDamage++;
        }

        if(CurrentHealth <= 0)
        {
            this.transform.gameObject.SetActive(false);
        }
    }

    void TakeDamage(int DamageTaken)
    {
        if (!IFrame)
        {
            CurrentHealth -= DamageTaken;

            if(IFrameDuration != 0)
                IFrame = true;
        }
    }
}
