using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerMovement : MonoBehaviour {

    public float speed, visionRange, visionRadius, minDist, searchCounter;
    public int turnDir;
    public AudioSource _as;
    public AudioClip[] audioClipArray;
    private float currentHitDistance, chaseCounter, turnCounter, searchTimer;
    private int x, y;
    private GameObject player;
    private Vector3 fwd, origin;
    private bool playerSpotted;
    private bool soundPlay = false;
    RaycastHit hit;

    void Awake() {
        _as = GetComponent<AudioSource>();
    }

    void Start() {
        player = GameObject.FindWithTag("Player");
        minDist = 2f;
        speed = (Random.Range(1.75f, 2.25f));
        visionRange = Mathf.Infinity;
        visionRadius = 0.7f;
        currentHitDistance = visionRange;
        turnDir = (Random.Range(0, 2) == 0) ? 1 : -1;
        turnCounter = 0;
        GetComponent<Rigidbody>().isKinematic = true;
        x = (Random.Range(9, 15));
        y = (Random.Range(15, 35));
        chaseCounter = 0;
        searchTimer = 3;
        searchCounter = 0;
        _as.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
    }

    void Update() {
        origin = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        /*Change turn direction every x seconds*/
        turnCounter += 1 * Time.deltaTime;
        searchCounter += 1 * Time.deltaTime;
        if (turnCounter % x < 1) {
            turnDir *= -1;
            x = (Random.Range(9, 15));
            if (turnCounter > 100)
                turnCounter = 0;
        }
        if (Physics.SphereCast(origin, visionRadius, fwd, out hit, visionRange)) {
            currentHitDistance = hit.distance;
            if (hit.collider.gameObject.tag == "Player") {
                playerSpotted = true;
                chaseCounter = 2;
                Chase(player);
                if (soundPlay == false) {
                    _as.PlayOneShot(_as.clip);
                    soundPlay = true;
                }
            }
            if (searchCounter > y && !playerSpotted) {
                Rotate();
                searchTimer -= 1 * Time.deltaTime;
                if (searchTimer < 0) {
                    searchTimer = 3;
                    searchCounter = 0;
                    y = (Random.Range(15, 35));
                }
                return;
            }
            else if (currentHitDistance <= minDist)
                Rotate();
            else
                transform.position += transform.forward * Time.deltaTime * speed;
        }
        else {
            transform.position += transform.forward * Time.deltaTime * speed;
            currentHitDistance = visionRange;
        }
        /*Chases player for chaseCounter seconds after losing sight*/
        if (chaseCounter >= 0) {
            Chase(player);
            chaseCounter -= 1 * Time.deltaTime;
        }
        else
            playerSpotted = false;
    }

    void Chase(GameObject player) {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newDir, 20 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed * 1.25f;
    }

    public void Rotate() {
        Quaternion degrees = Quaternion.Euler(0, turnDir * speed, 0);
        Quaternion turn = transform.rotation *= degrees;
        transform.rotation = Quaternion.Lerp(transform.rotation, turn, Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + fwd * currentHitDistance);
        Gizmos.DrawWireSphere(origin + fwd * currentHitDistance, visionRadius);
    }

    public void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
