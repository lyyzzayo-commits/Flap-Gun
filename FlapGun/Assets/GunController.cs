using UnityEngine;

class GunController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform muzzle; // 총구

    [Header("Rebound")]
    [SerializeField] private float recoilForce = 5f;
    [SerializeField] private float torqueForce = 2f;

    private void OnEnable()
    {
        GameSignals.BulletFired += Rebound;
    }

    private void OnDisable()
    {
        GameSignals.BulletFired -= Rebound;
    }

    private void Rebound()
    {
        // 1️⃣ 총알 나가는 방향 (총구의 로컬 Y)
        Vector2 fireDir = muzzle.up.normalized;

        // 2️⃣ 반동 방향 (정확히 반대)
        Vector2 recoilDir = -fireDir;

        rb.AddForce(recoilDir * recoilForce, ForceMode2D.Impulse);

        // 3️⃣ 회전 (빙글 도는 효과)
        rb.AddTorque(torqueForce, ForceMode2D.Impulse);
    }
}
