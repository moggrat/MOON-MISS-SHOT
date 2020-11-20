using UnityEngine;
using System.Collections;
 
public class GravObject : MonoBehaviour 
{
    [SerializeField] private float gravConst  = -9.8f ; //-45000 worked atm
    private Vector3 gravCenter = Vector3.zero;
    private int gravObjMask;
    private float pullForce;


    private void Start()
    {
        gravCenter =  gameObject.transform.position;
        gravObjMask = LayerMask.GetMask("GravitationalObject");
    }

    public void attractGPull(GameObject gobj) //"go bj" nice
    {
        Rigidbody rb = gobj.GetComponent<Rigidbody>();
        //inverse square law
        pullForce = gravConst*((rb.mass * rb.mass)/Mathf.Pow(Vector3.Distance(this.transform.position,rb.transform.position),2));
        Vector3 pullVec = rb.transform.position - gravCenter;
        rb.AddForce(pullVec.normalized*pullForce*Time.deltaTime );
        OrientPlayer(rb);
    }
    public void OrientPlayer(Rigidbody rb) 
    {
        RaycastHit hit;
        
        //raycast from player/object to "planet" (instead of down which caused issues based on when the player is rotated)
        Vector3 dir = transform.position - rb.transform.position;
        dir = dir.normalized;

	    float linkDistance = Vector3.Distance(this.transform.position, rb.transform.position);

        Debug.DrawRay(rb.transform.position, dir * linkDistance, Color.green);
        if (Physics.Raycast (rb.transform.position, dir, out hit, linkDistance, gravObjMask)) 
        {
            float rotationSpeedMul = 180f;
            Quaternion futureRot = Quaternion.FromToRotation (rb.transform.up, hit.normal) * rb.transform.rotation;
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, futureRot, rotationSpeedMul * Time.deltaTime);
        }
    }
    public float pullForceGet(Rigidbody rb)
    {
        pullForce = gravConst*((rb.mass * rb.mass)/Mathf.Pow(Vector3.Distance(this.transform.position,rb.transform.position),2));
        return pullForce;
    }

}