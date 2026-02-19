using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [System.Serializable]
    public class Heart
    {
        public GameObject main;
        public Slider slider;
    }

    [SerializeField] public List<Heart> Hearts = new List<Heart>();

    public void setHearts(PlayerHealth health)
    {
        float currentHp = health.currentHealth;

        int intHp = Mathf.FloorToInt(currentHp);

        for (int i = 0; i < Hearts.Count; i++)
        {

            if(i < intHp) //full heart
            {
                Hearts[i].main.SetActive(true);
                Hearts[i].slider.value = 1f;
            }
            else if(i == intHp) //Percent Heart
            {
                float remainder = currentHp - intHp;

                if (remainder > 0) 
                {
                    Hearts[i].main.SetActive(true);
                    Hearts[i].slider.value = remainder;
                }
                else
                {
                    Hearts[i].main.SetActive(false);
                }
                
            }
            else // Inactive heart
            {
                Hearts[i].main.SetActive(false);
            }
        }
    }

}
