using UnityEngine;
using System.Collections;
 
public class CharControl : MonoBehaviour 
{
    [SerializeField] private float m_jumpPower = 8; // The force added to the ball when it jumps.
    [SerializeField] private float m_speed = 5f; // The force added to the ball when it jumps.
    [SerializeField] private float m_maxSpeed = 5f; // The force added to the ball when it jumps.
    [SerializeField] private float m_rotation_speed = 5f; // The force added to the ball when it jumps.
    [SerializeField] private float m_air_control = 0.001f; // The length of the ray to check if the ball is grounded.
    [SerializeField] private float k_groundRayDist = 1.05f; // The length of the ray to check if the ball is grounded.

    private float jumpPowerInit;
    private Rigidbody m_Rigidbody;
    private Vector2 inputs;
    private bool jumpKey = false;
    private GameObject[] planets;
    private GameObject clostestPlanet;


    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.useGravity = false;
        jumpPowerInit = m_jumpPower;
    }
    public void Update()
    {
        //inputs variables
        inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //GetAxisRaw vs getaxis
        jumpKey = Input.GetKey(KeyCode.Space);
    }
    public void FixedUpdate()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        //movement-------------------
        Vector3 tempVect = new Vector3(0, 0, inputs.y);
        tempVect = tempVect.normalized;

        if(((m_Rigidbody.velocity+transform.TransformDirection(tempVect) * m_speed).magnitude > m_maxSpeed)&&Physics.Raycast(transform.position, dwn, k_groundRayDist))
        {//overspeed & grounded 
           m_Rigidbody.velocity=m_Rigidbody.velocity+transform.TransformDirection(tempVect) * m_speed;
           m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * m_maxSpeed;
           //Debug.Log("overspeed & grounded");
        }
        else  if ((m_Rigidbody.velocity+transform.TransformDirection(tempVect) * m_speed).magnitude > m_maxSpeed)
        {//overspeed & air //works but is a bit hacky
            m_Rigidbody.velocity = m_Rigidbody.velocity+transform.TransformDirection(tempVect) * m_speed * m_air_control;
            //Debug.Log("overspeed & air");
        }
        else
        {//underspeed & whatever
            m_Rigidbody.velocity=m_Rigidbody.velocity+transform.TransformDirection(tempVect) * m_speed;
            //Debug.Log("underspeed & whatever");
        }

        //play controlled rotation-------------------
        transform.Rotate(0f, inputs.x * m_rotation_speed, 0f);

        //jump-------------------
        Debug.DrawRay(transform.position, dwn* k_groundRayDist, Color.red);
        if (Physics.Raycast(transform.position, dwn, k_groundRayDist) && jumpKey)
        {
            m_Rigidbody.AddForce(transform.TransformDirection(Vector3.up)*m_jumpPower, ForceMode.Impulse);
            RevertJump();
        }
    }
    void LateUpdate()
    {
        if (planets == null)
            planets = GameObject.FindGameObjectsWithTag("GravitationalObject");

        foreach (GameObject planet in planets)
        {
            if ((clostestPlanet != null) && 
            planet.GetComponent<GravObject>().pullForceGet(m_Rigidbody) < clostestPlanet.GetComponent<GravObject>().pullForceGet(m_Rigidbody))
            {
                clostestPlanet = planet;
            }
            else if (clostestPlanet == null)
            {
                clostestPlanet = planet;
            }
        }
        clostestPlanet.GetComponent<GravObject>().attractGPull(gameObject);
        //Debug.Log(clostestPlanet);
    }
    public void SetJump(float jumpPower)
    {
        m_jumpPower = jumpPower;
    }
    public void RevertJump()
    {
        m_jumpPower = jumpPowerInit;
    }

}
        
