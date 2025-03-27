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
        // 타일맵의 범위 가져오기
        BoundsInt bounds = tilemap.cellBounds;

        while (true)
        {
            // 랜덤한 x, y 좌표 선택 (타일맵의 셀 좌표 기준)
            int randomX = Random.Range(bounds.xMin, bounds.xMax);
            int randomY = Random.Range(bounds.yMin, bounds.yMax);

            Vector3Int cellPosition = new Vector3Int(randomX, randomY, 0);

            // 해당 셀에 타일이 있는지 확인
            if (tilemap.HasTile(cellPosition))
            {
                return tilemap.GetCellCenterWorld(cellPosition); // 타일 중심 위치 반환
            }
        }
    }

    private void Spawn(Vector3 position)
    {
        GameObject zombie = PoolManager.Instance.GetObject(EObjectType.Zombie);
        zombie.transform.position = position;
    }
}
