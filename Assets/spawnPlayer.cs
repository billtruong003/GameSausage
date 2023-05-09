using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    // Vị trí spawn của player
    public Transform spawnPoint;

    // Đối tượng Player prefab
    public GameObject playerPrefab;

    void Start()
    {
        // Tạo một đối tượng Player mới tại vị trí spawnPoint
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        // Đặt parent cho đối tượng mới tạo thành GameObject chứa script này
        newPlayer.transform.SetParent(transform);
    }
}
