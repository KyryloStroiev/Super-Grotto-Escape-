using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;  // Кількість сили, що додається під час стрибка гравця.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f; // Величина maxSpeed, застосована до присідання. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // Наскільки згладити рух
    [SerializeField] private bool m_AirControl = false; // Чи може гравець керувати під час стрибка;
    [SerializeField] private LayerMask m_WhatIsGround; // Маска, що визначає, що заземлено для персонажа
    [SerializeField] private Transform m_GroundCheck; // Позначення позиції, де перевірити, чи гравець заземлений.
    [SerializeField] private Transform m_CeilingCheck; // Позиція яка перевіряє чи є стеля над персонажем
    [SerializeField] private Collider2D m_CrouchDisableCollider; // Колайдер, який буде вимкнено під час присідання
	/*public float delayTime = 0.8f;*/ // Час для затримки стрибку
    const float k_GroundedRadius = .2f; // Радіус кола перекриття для визначення заземлення
    private bool m_Grounded; // Чи гравець на землі.
	const float k_CeilingRadius = .2f; // Радіус кола перекриття, щоб визначити, чи може гравець встати
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true; // Для визначення того, куди гравець зараз дивиться.
	private Vector3 m_Velocity = Vector3.zero;

	public UnityEvent OnLandEvent;

	[System.Serializable]
    public class BoolEvent: UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}
		if (OnCrouchEvent == null)
        {
            OnCrouchEvent = new BoolEvent();
        }
	}
	//public  void LookandUp(bool isLookinUp)
	//{

	//}
	

	private void FixedUpdate()
	{
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        // Гравець припиняється, якщо кругове закидання в позицію гри на землю влучає в щось, що позначено як земля
        // Це можна зробити за допомогою шарів, але Sample Assets не перезапише налаштування вашого проекту.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		
	}

	public void Move(float move, bool crouch, bool jump)
    {
		// Якщо персонаж присідає, перевірте, чи може персонаж встати( не має стелі над ним)
		if (!crouch)
        {
			// Якщо над персонажем є стеля, яка заважає йому встати, тримайте його навпочіпки
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround) )
			{
				crouch = true;
				
			}
		}
		if (m_Rigidbody2D.velocity.y > 0 && Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
		}
		//Керуєм персонажем, лише якщо він на землі або увімкнено airControl
		if (m_Grounded || m_AirControl )
		{
			
			//Якщо на землі
			if (crouch )
			{
				if(!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
					
				}
				// Зменшити швидкість на множник crouchSpeed
				move *= m_CrouchSpeed;
				// Вимкнення одного з колайдерів під час присідання
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Увімкнути коллайдер, коли він не присідає
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if(m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
			// Переміщення персонажа шляхом визначення цільової швидкості
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// А потім згладити його та застосувати до персонажа
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity,
				ref m_Velocity, m_MovementSmoothing);
			//Якщо гравець переміщується вправо, а гравець дивиться вліво...
			if (move > 0 && !m_FacingRight)
			{
				// повернути гравця
				Flip();
			}
			// Інакше, якщо введення рухає гравця ліворуч, а гравець дивиться праворуч...
			else if (move < 0 && m_FacingRight)
			{
				// повернути гравця
				Flip();
			}
		}
		// Якщо гравець повинен стрибнути...
		if(m_Grounded && jump)
		{
			// Додати гравцю вертикальну силу.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}
	private void Flip()
	{
		// Перемкнути спосіб позначення гравця як обличчя.
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0, 180, 0);
	
	}

}
