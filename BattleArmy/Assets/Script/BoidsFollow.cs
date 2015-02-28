using UnityEngine;
using System.Collections;

public class BoidsFollow : MonoBehaviour {

    
    public BoidController controller;


    // Calcule du vecteur de séparation entre deux boids
    Vector3 GetSeparationVector(Transform target)
    {
        var diff = transform.position - target.transform.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / controller.neighborDist);
        return diff * (scaler / diffLen);
    }

    void Update()
    {
        var currentPosition = transform.position;
        var currentRotation = transform.rotation;

        // Calcule la velocity avec une coefficiant random.
        var randomCoef = Random.Range(0, 1);
        var velocity = controller.velocity * (1.0f + randomCoef * controller.velocityVariation);

        // Initialisation des vecteurs correspondantes aux 3 règles.
        var separation = Vector3.zero;
        var alignment = controller.transform.forward;
        var cohesion = controller.transform.position;

        // Récupérations des boids voisins.
        var neighborsBoids = Physics.OverlapSphere(currentPosition, controller.neighborDist, controller.searchLayer);

        // Calcule de la "separation", "l'alignement" et de la "cohésion" moyenne de touts les boids.
        foreach (var boid in neighborsBoids)
        {
            if (boid.gameObject == gameObject) continue; // Si le gameObject correspond au boids actuel, on continue.
            var t = boid.transform;
            separation += GetSeparationVector(t);
            alignment += t.forward;
            cohesion += t.position;
        }

        //Division par le nombdre de boids afin de récupérer l'algnement et la cohesion
        var average = 1.0f / neighborsBoids.Length;
        alignment *= average;
        cohesion *= average;
        cohesion = (cohesion - currentPosition).normalized;

        // Calcule de la direction du boids
        var direction = separation + alignment + cohesion;
        var rotation = Quaternion.FromToRotation(Vector3.forward, direction.normalized);

        // Smooth du changement de direction
        if (rotation != currentRotation)
            transform.rotation = Quaternion.Slerp(rotation, currentRotation, 0.8f);

        // Déplacement du boids.
        transform.position = currentPosition + transform.forward * (velocity * Time.deltaTime);
    }
}
