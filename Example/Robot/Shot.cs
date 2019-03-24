using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour {
    void Start() {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30, ForceMode.Impulse);

        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay() {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        var health = collision.collider.GetComponent<Health>();
        if (health != null) {
            health.current -= 10;
        }
    }
}
