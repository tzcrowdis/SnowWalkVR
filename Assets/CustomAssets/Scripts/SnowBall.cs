using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class SnowBall : MonoBehaviour
{
    public Material snowBallMat;
    public Material snowBallSelectedMat;
    public float breakThreshold = 0.4f;
    public AudioClip breakSound;
    public AudioClip pickupSound;
    public DecalProjector snowDecal;
    private AudioSource audioSource;
    private Rigidbody rb;
    private ParticleSystem snowParticleSystem;
    private void Awake()
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

    public void playPickupSound()
    {
        audioSource.clip = pickupSound;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();

    }


    private void OnCollisionEnter(Collision collision)
    {

        if (rb.velocity.magnitude > breakThreshold && !collision.collider.CompareTag("Player"))
        {
            // Create snow decal
            DecalProjector decalInstance = Instantiate(snowDecal, transform.position, Quaternion.LookRotation(collision.GetContact(0).normal) * snowDecal.transform.rotation); 
            decalInstance.transform.SetParent(collision.collider.transform.parent);

            // Play particle systemaw
            snowParticleSystem.Play();

            // Play break sound
            audioSource.clip = breakSound;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();

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
