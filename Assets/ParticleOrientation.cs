using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TreeEditor;
using UnityEngine;
    
public class ParticleOrientation : MonoBehaviour
{
        /// <summary>
        /// Emit particle depending on the speed of the player and in the direction of him.
        /// If you are below the speed minimum desire it will emit the minimum set.
        /// If you are above the speed maximum desire it will emit the maximum set.
        /// </summary>
      
    [TitleGroup("Parameters")]
    [SerializeField,MinMaxSlider(0,500,true)] private Vector2 emissionOverSpeed = new Vector2(0,80);
    [SerializeField,MinMaxSlider(0,500,true)] private Vector2 speedDesire = new Vector2(120,300);
    
    [SerializeField] public float transitionSpeed = 20;
    private Rigidbody rb;

    private GameObject objParticuleSystem;

    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        objParticuleSystem = (particleSystem = gameObject.GetComponentInChildren<ParticleSystem>()).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = rb.velocity;
        // velocity.y = 0;
        var trans = transform;
        var speed = velocity.magnitude;
        objParticuleSystem.SetActive(true);
        var velocityNormalize =trans.position + velocity.normalized;
        var fwd =trans.position + (trans.forward*transitionSpeed);
        
        Vector3 middle = new Vector3(
            (fwd.x + velocityNormalize.x) / 2, 
            (fwd.y + velocityNormalize.y) / 2,
            (fwd.z + velocityNormalize.z) / 2);
        
        var lookAt = middle;
        trans.LookAt(lookAt);
        
        var percent =Mathf.Clamp((speed- speedDesire.x) / (speedDesire.y - speedDesire.x),0,1);
        // Debug.Log("Percent : " + percent);
        var particles = Mathf.Lerp(emissionOverSpeed.x, emissionOverSpeed.y, percent);
        particleSystem.Emit((int)particles);
        

        // Debug.Log("numberOfParticle = " + particles);
        // emission.rateOverDistance = particles;

        
    }
}
