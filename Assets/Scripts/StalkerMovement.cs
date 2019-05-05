using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerMovement : MonoBehaviour {

    public float speed, visionRange, visionRadius, minDist;
    public int turnDir;
    private float currentHitDistance, turnCounter;
    private int x;
    private GameObject player;
    private Vector3 fwd, origin;
    RaycastHit hit;

    void Start() {
        player = GameObject.FindWithTag("Player");
        minDist = 2f;
        speed = (Random.Range(1.75f, 2.5f));
        visionRange = Mathf.Infinity;
        visionRadius = 0.7f;
        currentHitDistance = visionRange;
        turnDir = (Random.Range(0, 2) == 0) ? 1 : -1;
        turnCounter = 0;
        GetComponent<Rigidbody>().isKinematic = true;
        x = (Random.Range(9, 12));
    }

    void Update() {
        origin = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        /*Change turn direction every x seconds*/
        turnCounter += 1 * Time.deltaTime;
        if (turnCounter % x < 1) {
            turnDir *= -1;
            x = (Random.Range(9, 12));
        }
        if (Physics.SphereCast(origin, visionRadius, fwd, out hit, visionRange)) {
            currentHitDistance = hit.distance;
            if (hit.collider.gameObject.tag == "Player") {
                Chase(player);
            }
            else if (currentHitDistance <= minDist) {
                AvoidWall(turnDir);
            }
            else
                transform.position += transform.forward * Time.deltaTime * speed;
        }
        else {
            transform.position += transform.forward * Time.deltaTime * speed;
            currentHitDistance = visionRange;
        }
    }

    void Chase(GameObject player) {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newDir, 20 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed * 1.5f;
    }

    public void AvoidWall(int direction) {
        Quaternion degrees = Quaternion.Euler(0, direction * speed, 0);
        Quaternion turn = transform.rotation *= degrees;
        transform.rotation = Quaternion.Lerp(transform.rotation, turn, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player") {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + fwd * currentHitDistance);
        Gizmos.DrawWireSphere(origin + fwd * currentHitDistance, visionRadius);
    }
}
