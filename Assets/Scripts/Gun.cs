using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleFlashEffect;
    public Transform gunBarrelTransform;
    public LayerMask targetLayer;
    public LineRenderer bulletLineRenderer;

    public float timeBetShot = 0.3f;
    public int shotDamage = 20;

    private AudioSource shotAudio;
    private float fireDistance = 50f;
    private float shootEffectInterval = 0.05f;
    private float lastFireTime;

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    public void Fire()
    {
        if (Time.time > lastFireTime + timeBetShot)
        {
            lastFireTime = Time.time;
            _ = ShootAsync();
        }
    }

    private async Task ShootAsync()
    {
        Vector3 hitPosition;

        Ray ray = new Ray(gunBarrelTransform.position, gunBarrelTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, fireDistance, targetLayer))
        {
            hitPosition = hit.point;

            IDamgable target = hit.collider.GetComponent<IDamgable>();
            if (target != null)
            {
                target.onDamage(shotDamage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = gunBarrelTransform.position + gunBarrelTransform.forward * fireDistance;
        }

        // 총 발사 이펙트 비동기 실행
        await ShotEffectAsync(hitPosition);
    }

    private async Awaitable ShotEffectAsync(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shotAudio.Play();

        bulletLineRenderer.SetPosition(0, gunBarrelTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        await Awaitable.WaitForSecondsAsync(shootEffectInterval);
        bulletLineRenderer.enabled = false;
    }
}
