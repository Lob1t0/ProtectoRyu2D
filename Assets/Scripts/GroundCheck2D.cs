using UnityEngine;

public class GroundCheck2D : MonoBehaviour
{
    private bool isGrounded = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.CompareTag("PISO"))
       {
            isGrounded=true;
            Debug.Log("Collision con suelo");
       }
    }

    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PISO"))
        {
            isGrounded=false;
            Debug.Log("Ya no");
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
