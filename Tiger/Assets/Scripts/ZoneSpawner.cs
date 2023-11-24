using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> zoneList;

    private Transform zoneTransform;
    private float spawnPointY = 5.6f;
    private bool isReadyToSpawn = true;
    private int previousZoneIndex = -1;

    private void Update()
    {
        SpawnNewZone();
    }

    private void SpawnNewZone()
    {
        if (isReadyToSpawn)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, zoneList.Count);
            } while (randomIndex == previousZoneIndex);

            zoneTransform = Instantiate(zoneList[randomIndex]).transform;
            zoneTransform.position = new Vector2(0, spawnPointY);
            isReadyToSpawn = false;

            previousZoneIndex = randomIndex;
        }

        if (zoneTransform != null && zoneTransform.Find("TopPoint").transform.position.y < spawnPointY - 0.5f)
        {
            isReadyToSpawn = true;
        }
    }

}
