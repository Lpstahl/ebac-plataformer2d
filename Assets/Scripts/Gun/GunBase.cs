using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public Transform playerSideReference;

    private Coroutine _currentCorotine;

    private Player _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentCorotine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (_currentCorotine != null)
                StopCoroutine(_currentCorotine);
        } 
    }

    private void PlayShotVfx()
    {
        VFXManager.instance.PlayVFXByType(VFXManager.VFXType.SHOOT, transform.position);
        
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
        projectile.side = _player.transform.localScale.x;
        PlayShotVfx();
    }
}
