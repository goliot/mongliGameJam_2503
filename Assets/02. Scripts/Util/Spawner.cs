using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float _spawnTime;
    private float _time = 0f;

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time > _spawnTime)
        {
            _time = 0f;
            Spawn(GetRandomPosition());
        }    
    }

    private Vector3 GetRandomPosition()
    {
        // Ÿ�ϸ��� ���� ��������
        BoundsInt bounds = tilemap.cellBounds;

        while (true)
        {
            // ������ x, y ��ǥ ���� (Ÿ�ϸ��� �� ��ǥ ����)
            int randomX = Random.Range(bounds.xMin, bounds.xMax);
            int randomY = Random.Range(bounds.yMin, bounds.yMax);

            Vector3Int cellPosition = new Vector3Int(randomX, randomY, 0);

            // �ش� ���� Ÿ���� �ִ��� Ȯ��
            if (tilemap.HasTile(cellPosition))
            {
                return tilemap.GetCellCenterWorld(cellPosition); // Ÿ�� �߽� ��ġ ��ȯ
            }
        }
    }

    private void Spawn(Vector3 position)
    {
        GameObject zombie = PoolManager.Instance.GetObject(EObjectType.Zombie);
        zombie.transform.position = position;
    }
}
