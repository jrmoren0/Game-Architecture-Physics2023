using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float _maxHealth;


    public Action<float> OnHealthUpdate;

    private float _health;





    void Start()
    {
        _health = _maxHealth;

        OnHealthUpdate(_maxHealth);
    }

  public void DeductHealth(float value)
    {
        _health -= value;

        Debug.Log(_health);
        if(_health <= 0)
        {

            ///Playerd Is DED
            _health = 0;
        }

       OnHealthUpdate(_health);
    }
}
