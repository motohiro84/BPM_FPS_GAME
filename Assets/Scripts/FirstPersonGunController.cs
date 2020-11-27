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
  private Vector3 shotogun_vector;
  public static int damage;
  string tagName;
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

    if (Motion.state != "Relord" && Motion.state != "RelordEnd")
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
    if (Motion.state == "_Relord")
    {
      motion.RelordEndMotion();
      InitGun();
    }
    else if (Motion.state != "Relord")
    {
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
        shootRange = 50;
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
      // CircleHorizon();
      shotogun_fire();
      ray = new Ray(RayPos.transform.position, shotogun_vector);
    }

    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, shootRange))
    {
      tagName = hit.collider.gameObject.tag;
      if (hitEffectPrefab != null)
      {
        if (tagName == "Ground" || tagName == "rock")
        {
          hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
          hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
          Destroy(hitEffect, 1f);
        }
      }
      if (tagName == "Enemy")
      {
        EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
        enemy.Hp -= damage;
      }
    }
    Ammo--;

  }

  bool GunRelord()
  {
    return Input.GetKeyDown(KeyCode.R);
  }

  void shotogun_fire()
  {
    //trandform.upを軸に、-20~20度で回転するクォータニオンを求める
    var y = Quaternion.AngleAxis(Random.Range(-20f, 20f), RayPos.transform.up);
    var x = Quaternion.AngleAxis(Random.Range(-20f, 20f), RayPos.transform.right);

    //そのクォータニオンにtransform.forwardをかけて、ランダムなショットガンの弾のベクトルを求める
    var vy = y * RayPos.transform.forward;
    var vx = x * RayPos.transform.forward;

    shotogun_vector = vx + vy;
    Debug.DrawRay(RayPos.transform.position, shotogun_vector, Color.red, 50);
  }
}