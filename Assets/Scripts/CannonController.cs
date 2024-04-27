using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    private Transform cannonTransform, shootPoint;
   
    [SerializeField]
    private float missileCooldown = 1f;

    [SerializeField]
    private GameObject beamPrefab, missilePrefab;

    public int maxMissiles = 10;
    public int currentMissiles = 10;
    public TextMeshProUGUI missileText;
    public TextMeshProUGUI maxMissileText;
    public float beamSpeed;
    public float missileSpeed;
    private float missileTimer;
    public Camera mainCamera;
    private AudioSource audioSource;
    public AudioClip beamClip;
    public AudioClip missileClip;
    // Start is called before the first frame update
    void Start()
    {
        Pause.isPaused = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        maxMissileText.text = maxMissiles.ToString();
        missileText.text = currentMissiles.ToString();
        missileTimer -= Time.deltaTime;
    }

    void OnBeam()
    {
        if (!Pause.isPaused)
        {

            GameObject newProjectile = Instantiate(beamPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody beamRB = newProjectile.GetComponent<Rigidbody>();
            audioSource.PlayOneShot(beamClip);
            beamRB.velocity = mainCamera.transform.forward * beamSpeed;


        }





    }

    void OnMissile()
    {
        if (!Pause.isPaused)
        {
            if (missileTimer <= 0f && currentMissiles > 0)
            {
                GameObject newProjectile = Instantiate(missilePrefab, shootPoint.position, Quaternion.identity);
                Quaternion missileRotation = mainCamera.transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
                newProjectile.transform.rotation = missileRotation;
                Rigidbody missileRB = newProjectile.GetComponent<Rigidbody>();
                audioSource.PlayOneShot(missileClip);
                missileRB.velocity = mainCamera.transform.forward * missileSpeed;
                currentMissiles--;
                missileTimer = missileCooldown;
            }
        }
        
    }
}
