using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image maxHealthBar;
    [SerializeField] private Image currHealthBar;

    private void Start()
    {
        maxHealthBar.fillAmount = playerHealth.currHealth / 3;
    }
    // Update is called once per frame
    void Update()
    {
        currHealthBar.fillAmount = playerHealth.currHealth / 3;
    }
}
