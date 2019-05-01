using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerDetect : MonoBehaviour
{
    public float smooth, angle;
    private Quaternion targetRotation;

    public float range, radius;
    private GameObject player;
    private Vector3 fwd, origin;
    RaycastHit hit;

    void Start() {
        targetRotation = transform.rotation;
        player = GameObject.FindWithTag("Player");
        smooth = 1f;
        angle = 2f;
        range = 6;
        radius = 1;
}

    void Update() {
        origin = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.SphereCast(origin, radius, fwd, out hit, range)) {
            if (hit.collider.gameObject.tag == "Player") {
                Debug.Log("Player found");
                LookAt(player, hit);
            }
            else
                Rotate();
        }
        else
            Rotate();
    }

    void LookAt(GameObject player, RaycastHit hit) {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newDir, 20 * smooth * Time.deltaTime);
    }

    void Rotate() {
        targetRotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + fwd * hit.distance);
        Gizmos.DrawWireSphere(origin + fwd * hit.distance, radius);
    }
}
