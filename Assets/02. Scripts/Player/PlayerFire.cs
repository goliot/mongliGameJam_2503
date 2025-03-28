using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private Transform Muzzle;
    [SerializeField] private GameObject EmptyCartridge;

    private float _timer = 0f;

    [SerializeField] private GunSpin gunSpin;
    
    private FollowCamera mainCamera;
    private PlayerDataSO _playerData;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerData = GetComponent<Player>().PlayerData;
        mainCamera = Camera.main.GetComponent<FollowCamera>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            if(_timer >= _playerData.NowAtkSpeed)
            {
                _timer = 0;
                Fire();
            }
        }
    }

    private void Fire()
    {
        gunSpin.TriggerRecoil();

        if (_player.BulletCount < 0)
        {
            return;
        }
        _player.BulletCount--;

        mainCamera.Shake();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - Muzzle.position).normalized;

        GameObject bullet = PoolManager.Instance.GetObject(EObjectType.Bullet);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        bullet.transform.position = Muzzle.position;
        bullet.GetComponent<Bullet>().Direction = direction;
        bullet.GetComponent<Bullet>().Damage = _playerData.NowDamage;
        bullet.transform.rotation = rotation;

        DropCartridge();
    }

    private void DropCartridge()
    {
        GameObject cartridge = PoolManager.Instance.GetObject(EObjectType.Cartridge);
        cartridge.transform.position = Gun.transform.position;
        //cartridge.GetComponent<Fragment>().cartridge.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-50, 50), ForceMode2D.Impulse);

    }
}