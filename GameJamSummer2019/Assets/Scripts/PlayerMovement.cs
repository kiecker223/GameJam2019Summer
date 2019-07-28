using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D m_Rb;

	public GameObject playerModel;

	public Sprite currentSprite;

	private float m_VerticalVelocity;

	private bool m_bFacingRight;

    public Possession possession;

	public bool bIsGrounded
	{
		get
		{
			RaycastHit2D[] hits2d = new RaycastHit2D[12];
			int numHits;

			ContactFilter2D contactFilter = new ContactFilter2D();

			if (currentSprite)
				numHits = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, hits2d, currentSprite.bounds.size.y / 2f);
			else
				numHits = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, hits2d, 0.55f);
			

            /*if (currentSprite)
                numHits = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, hits2d, currentSprite.bounds.size.y / 2f);
            else
                numHits = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, hits2d, 0.55f);*/

            //Climbing for panda
            if (possession.state.canClimb)
            {

              //  bool climbable = Physics2D.Raycast(transform.position, Vector2.left).collider.tag == "climbable" || Physics2D.Raycast(transform.position, Vector2.right).collider.tag == "climbable";
               
                int numHitsPanda = 0;
                    if (currentSprite)
                    {
                        numHitsPanda = Physics2D.Raycast(transform.position, Vector2.right, contactFilter, hits2d, currentSprite.bounds.size.y / 2f) - 1;
                    if (numHits > 0 && hits2d[1].collider.tag == "climbable") return true;
                    numHitsPanda = Physics2D.Raycast(transform.position, Vector2.left, contactFilter, hits2d, currentSprite.bounds.size.y / 2f) - 1;
                            if (numHits > 0 && hits2d[1].collider.tag == "climbable") return true;
                    }
                    else
                    {
                        numHitsPanda = Physics2D.Raycast(transform.position, Vector2.right, contactFilter, hits2d, 0.55f) - 1;
                         if (numHitsPanda > 0 && hits2d[1].collider.tag == "climbable") return true;
                         numHitsPanda = Physics2D.Raycast(transform.position, Vector2.left, contactFilter, hits2d, 0.55f) - 1;
                         if (numHitsPanda > 0 && hits2d[1].collider.tag == "climbable") return true;
                    
                }
                
            }
            else if (possession.state.canFly)
            {
                return true;
            }



            Debug.Log("num of ray hits: " + numHits);
			return numHits > 1;
		}
	}

    void Start()
    {
		m_Rb = GetComponent<Rigidbody2D>();

        possession = GetComponent<Possession>();
        possession.state = possession.ghost;
        possession.state.speed = 3;

    }

    bool isPaused = false;

    void Update()
    {

        if (InputManager.Instance.GetPauseButtonDown_Player1())
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0;
            return;
        }
        
            Time.timeScale = 1;
        


        bool bGrounded = bIsGrounded;
		float inheritedVelocity = m_Rb.velocity.x;
        float horizontalDir = InputManager.Instance.GetHorizontalAxisLeftStick_Player1() * possession.state.speed;// : inheritedVelocity;

		if (horizontalDir > 0.001f)
		{
			m_bFacingRight = true;
		}
		else if (horizontalDir < -0.001f)
		{
			m_bFacingRight = false;
		}
		else { } // Do nothing, Also bad code!!

		bool jumping = InputManager.Instance.GetJumpButtonDown_Player1() && bGrounded;
		
		float verticalVelocity = jumping ? 5f : m_Rb.velocity.y;
		playerModel.transform.right = new Vector3(m_bFacingRight ? 1.0f : -1.0f, 0, 0);
		m_Rb.velocity = new Vector3(horizontalDir, verticalVelocity, 0);
    }
}
