using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlagCapture : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            GameObject player = character.gameObject;
            PlayerController playerControls = player.GetComponent<PlayerController>();

            transform.SetParent(player.transform);
            transform.localPosition = new Vector3(0.4f, 0, -1);
            GetComponent<BoxCollider2D>().enabled = false;

            playerControls.SetFlagCaptured(true);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BringBackFlag : MonoBehaviour {
    private Tilemap returnArea;

    private void Awake() {
        returnArea = GetComponent<Tilemap>();
        GetStartArea();
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            GameObject player = character.gameObject;
            PlayerController playerControls = player.GetComponent<PlayerController>();

            if (playerControls.IsFlagCaptured()) {
                GameObject flag = playerControls.FindWithTag(player, "Flag");

                flag.transform.SetParent(null);
                flag.transform.position = returnArea.WorldToCell(GetStartArea()) + Vector3.one * 0.5f;
                // flag.GetComponent<BoxCollider2D>().enabled = true;

                playerControls.SetFlagCaptured(false);

                Debug.Log("Game Over!");
            }
        }
    }

    private Vector3Int GetStartArea() {
        Vector3Int startTilePosition = Vector3Int.zero;
        BoundsInt.PositionEnumerator tilesPosition = returnArea.cellBounds.allPositionsWithin;
        
        foreach (Vector3Int position in tilesPosition) {
            if (returnArea.HasTile(position)) {
                startTilePosition = position;
                break;
            }
        }

        return startTilePosition;
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTilemap : MonoBehaviour {
    private GameObject player;
    private GameObject flag;
    private Tilemap startTilemap;
    private Tilemap flagTilemap;
    [SerializeField] private Tile playerStartTile;
    [SerializeField] private Tile flagTile;

    private void Awake() {
        player = GameObject.Find("Player");
        flag = GameObject.Find("Flag");

        PlayerController playerControls = player.GetComponent<PlayerController>();

        startTilemap = playerControls.FindWithTag(this.gameObject, "Start Area").GetComponent<Tilemap>();
        flagTilemap = playerControls.FindWithTag(this.gameObject, "Flag Area").GetComponent<Tilemap>();

        startTilemap.SetTile(startTilemap.WorldToCell(player.transform.position), playerStartTile);
        flagTilemap.SetTile(flagTilemap.WorldToCell(flag.transform.position), flagTile);
    }

}

