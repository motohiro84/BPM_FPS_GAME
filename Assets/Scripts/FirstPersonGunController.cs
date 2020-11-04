using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FirstPersonGunController : MonoBehaviour
{
  public bool shootEnabled = true;

  public int maxAmmo = 6;

  private float shootInterval = 0.15f;
  private float shootRange = 50;
  public Vector3 muzzleFlashScale;
  public GameObject muzzleFlashPrefab;
  public GameObject hitEffectPrefab;
  bool shooting = false;
  int ammo;
  GameObject muzzleFlash;
  GameObject hitEffect;
  public GameObject RayPos;
  AudioSource audioSource;
  public AudioClip sound1;
  public AudioClip sound2;


  public int Ammo
  {
    set
    {
      ammo = Mathf.Clamp(value, 0, maxAmmo);
    }
    get
    {
      return ammo;
    }
  }
  void Start()
  {
    audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    InitGun();
  }
  void Update()
  {
    if (shootEnabled & ammo > 0 & GetInput())
    {
      StartCoroutine(ShootTimer());
    }
  }
  void InitGun()
  {
    Ammo = maxAmmo;
  }
  bool GetInput()
  {

    return Input.GetMouseButtonDown(0);
    return false;
  }
  IEnumerator ShootTimer()
  {
    if (!shooting && ammo != 0)
    {
      shooting = true;
      //マズルフラッシュON
      if (muzzleFlashPrefab != null)
      {
        if (muzzleFlash != null)
        {
          muzzleFlash.SetActive(true);
        }
        else
        {
          muzzleFlash = Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
          muzzleFlash.transform.SetParent(gameObject.transform);
          muzzleFlash.transform.localScale = muzzleFlashScale;
        }
      }

      Shoot();
      audioSource.PlayOneShot(sound1);
      yield return new WaitForSeconds(shootInterval);

      //マズルフラッシュOFF

      if (muzzleFlash != null)
      {
        Invoke("FlashMethod", 0.5f);
      }
      //ヒットエフェクトOFF

      if (hitEffect != null)
      {
        if (hitEffect.activeSelf)
        {
          Invoke("HitMethod", 1.0f);
        }
      }
      shooting = false;
    }
    else
    {
      audioSource.PlayOneShot(sound2);
      yield return null;
    }
  }

  void FlashMethod()
  {
    muzzleFlash.SetActive(false);
  }

  void HitMethod()
  {
    hitEffect.SetActive(false);
  }

  void Shoot()
  {
    Ray ray = new Ray(RayPos.transform.position, RayPos.transform.forward);
    RaycastHit hit;
    //レイを飛ばして、ヒットしたオブジェクトの情報を得る
    if (Physics.Raycast(ray, out hit, shootRange))
    {
      //ヒットエフェクトON
      if (hitEffectPrefab != null)
      {
        if (hitEffect != null)
        {
          hitEffect.transform.position = hit.point;
          hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
          hitEffect.SetActive(true);
        }
        else
        {
          hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
        // Destroy(hitEffect, 1f);
        }
      }


    }
    Ammo--;
  }

}