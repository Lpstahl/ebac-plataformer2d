using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;

    private Coroutine _currentCorotine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentCorotine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.S))
            {
                if (_currentCorotine != null) StopCoroutine(_currentCorotine);
            }
    }

    IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
    }
}
