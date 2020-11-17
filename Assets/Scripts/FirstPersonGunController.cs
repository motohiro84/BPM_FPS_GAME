using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FirstPersonGunController : MonoBehaviour
{
  public bool shootEnabled = true;
  public int maxAmmo;
  private int ammo;

  private float shootInterval = 0.15f;
  private float shootRange = 50;
  public Vector3 muzzleFlashScale;
  public GameObject muzzleFlashPrefab;
  public GameObject hitEffectPrefab;
  bool shooting = false;
  GameObject muzzleFlash;
  GameObject hitEffect;
  public GameObject RayPos;
  AudioSource audioSource;
  public AudioClip sound1;
  public AudioClip sound2;
  Motion motion;
  public GameObject motionObj;
  private Animator animator;
  private Vector3 Circle;
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
    motion = motionObj.GetComponent<Motion>();
    animator = motionObj.GetComponent<Animator>();
    InitGun();
  }

  void Update()
  {
    if (ammo < maxAmmo && GunRelord())
    {
      RelordKey();
    }

    if (animator.GetBool("Idle"))
    {
      if (shootEnabled && ammo > 0 && GetInput())
      {
        StartCoroutine(ShootTimer());
        motion.FireShootMotion();
      }
      else if (shootEnabled && ammo == 0 && GetInput())
      {
        audioSource.PlayOneShot(sound2);
        if (WeponChange.Key == 1)
        {
          motion.DryShootMotion();
        }
      }
    }
  }

  void RelordKey()
  {
    if (Motion.state == "RelordEnd")
    {
      Debug.Log("2");
      InitGun();
    }
    else
    {
      Debug.Log("1");
      motion.RelordMotion();
    }
  }

  void InitGun()
  {
    Ammo = maxAmmo;
  }

  bool GetInput()
  {
    return Input.GetMouseButtonDown(0);
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

      if (WeponChange.Key == 1)
      {
        shootRange = 30;
        Shoot();
      }
      else if (WeponChange.Key == 2)
      {
        shootRange = 10;
        for (int i = 0; i < 10; i++)
        {
          Shoot();
        }
      }

      audioSource.PlayOneShot(sound1);
      yield return new WaitForSeconds(shootInterval);

      if (muzzleFlash != null)
      {
        Invoke("FlashMethod", 0.1f);
      }
      shooting = false;
    }
    else
    {
      yield return null;
    }
  }

  void FlashMethod()
  {
    muzzleFlash.SetActive(false);
  }

  void Shoot()
  {
    Ray ray;
    ray = new Ray(RayPos.transform.position, RayPos.transform.forward);

    if (WeponChange.Key == 2)
    {
      CircleHorizon();
      ray = new Ray(RayPos.transform.position, Circle);
    }

    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, shootRange))
    {
      if (hitEffectPrefab != null)
      {
        hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
        hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        Destroy(hitEffect, 1f);
      }
    }

    Ammo--;

  }

  bool GunRelord()
  {
    return Input.GetKeyDown(KeyCode.R);
  }

  void CircleHorizon()
  {
    float angle = Random.Range(0, 360);
    float radius = Random.Range(0, 0.2f);
    float rad = angle * Mathf.Deg2Rad;
    float px = Mathf.Cos(rad) * radius;
    float pz = Mathf.Cos(rad) * radius;
    float py = Mathf.Sin(rad) * radius;
    Circle = new Vector3(px, py, pz) + RayPos.transform.forward;
  }

}