using UnityEngine;
using System.Collections;
 
public class JumpPad : MonoBehaviour 
{

    [SerializeField] private float m_jumpSetBoost = 8; 

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<CharControl>().SetJump(m_jumpSetBoost);
        }
    }
    private void OnCollisionExit (Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<CharControl>().RevertJump();
        }
    }
}