using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerMovement : MonoBehaviour
{
    public float smooth, angle;
    private Quaternion targetRotation;

    public float range, radius;
    private float currentHitDistance;
    private GameObject player;
    private Vector3 fwd, origin;
    RaycastHit hit;

    void Start() {
        targetRotation = transform.rotation;
        player = GameObject.FindWithTag("Player");
        smooth = 1f;
        angle = 2f;
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
                Debug.Log("Player found");
                Chase(player, hit);
            }
            else
                transform.position += transform.forward * Time.deltaTime * 2f;
        }
        else {
            transform.position += transform.forward * Time.deltaTime * 2f;
            currentHitDistance = range;
        }
    }

    void Chase(GameObject player, RaycastHit hit) {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newDir, 20 * smooth * Time.deltaTime);

        transform.position += transform.forward * Time.deltaTime * 2f;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + fwd * currentHitDistance);
        Gizmos.DrawWireSphere(origin + fwd * currentHitDistance, radius);
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Hit");
        if (collision.gameObject.name == "Player") {
            Destroy(gameObject);
            SceneManager.LoadScene("Level 1");
        }
        else {
            transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }
    }
}
