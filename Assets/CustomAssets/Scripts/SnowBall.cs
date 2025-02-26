using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnowBall : MonoBehaviour
{
    public Material snowBallMat;
    public Material snowBallSelectedMat;
    public float breakThreshold = 0.4f;
    public AudioClip breakSound;
    private AudioSource audioSource;
    private Rigidbody rb;
    private ParticleSystem snowParticleSystem;
    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        snowParticleSystem = GetComponent<ParticleSystem>();
    }

    public void setSnowballHover(bool setValue)
    {
        if (setValue && !GetComponent<XRGrabInteractable>().isSelected)
        {
            GetComponent<MeshRenderer>().material = snowBallSelectedMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = snowBallMat;
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (rb.velocity.magnitude > breakThreshold && !collision.collider.CompareTag("Player"))
        {
            // Play particle system
            snowParticleSystem.Play();

            // Play break sound
            audioSource.clip = breakSound;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();

            // TODO: spawn snow particles at point of collision
            ParticleSystem poof = transform.GetChild(1).GetComponent<ParticleSystem>();
            poof.transform.parent = null;
            poof.transform.localScale = Vector3.one;
            poof.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
            poof.Play();

            // Hide and freeze object
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<XRGrabInteractable>().enabled = false;
            rb.isKinematic = true;

            // Destroy after sound effect is done
            Destroy(gameObject, 1f);
        }

        // TODO: decal on object collided with
    }
}
