using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerMovement : MonoBehaviour {

    public float speed, range, radius, minDist;
    private float currentHitDistance;
    private GameObject player;
    private Vector3 fwd, origin;
    RaycastHit hit;

    void Start() {
        player = GameObject.FindWithTag("Player");
        minDist = 3f;
        speed = 2f;
        range = Mathf.Infinity;
        radius = 0.7f;
        currentHitDistance = range;
    }

    void Update() {
        origin = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.SphereCast(origin, radius, fwd, out hit, range)) {
            currentHitDistance = hit.distance;
            if (hit.collider.gameObject.tag == "Player") {
                Chase(player);
            }
            else if (hit.collider.gameObject.tag != "Stalker" && currentHitDistance <= minDist)
                AvoidWall();
            else
                transform.position += transform.forward * Time.deltaTime * speed;
        }
        else {
            transform.position += transform.forward * Time.deltaTime * speed;
            currentHitDistance = range;
        }
    }

    void Chase(GameObject player) {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newDir, 20 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void AvoidWall() {
        Quaternion degrees = Quaternion.Euler(0, speed, 0);
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
        Gizmos.DrawWireSphere(origin + fwd * currentHitDistance, radius);
    }
}
