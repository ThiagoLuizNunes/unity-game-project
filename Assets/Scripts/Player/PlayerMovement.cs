using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
    public float speed = 6f;	// Velocidade do Jogador
    Vector3 movement;			// Vetor de movimento
    Animator anim;				// Responsável pela transição da animação
    Rigidbody playerRigidbody;	// Responsável pela fisica do objeto
    int floorMask;				// Mascara de chão
    float camRayLenght = 100f;	// Informações para raycast

    void Awake() {
		
		floorMask = LayerMask.GetMask("Floor");	// Atribuir a máscara da camada
		anim = GetComponent<Animator> ();		// Atribuir as refêrências
		playerRigidbody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate() {
    	float h = Input.GetAxisRaw("Horizontal");
    	float v = Input.GetAxisRaw("Vertical");

    	Move(h, v);
    	Turning();
    	Animating(h, v);
    }

    // Movimenta player
    void Move(float h, float v) {
    	movement.Set(h, 0f, v);											// Determina o movimento
    	movement = movement.normalized * speed * Time.deltaTime;		// Normalize o movimento
    	playerRigidbody.MovePosition(transform.position + movement);	// Efetuar movimento no personagem
    }

    // Girar player
    void Turning() {
    	Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    	RaycastHit floorHit;
    	if (Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask)) {
    		Vector3 playerToMouse = floorHit.point - transform.position;
    		playerToMouse.y = 0f;

    		Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
    		playerRigidbody.MoveRotation(newRotation);
    	}
    }

    void Animating(float h, float v) {
    	bool walking = (h != 0f || v != 0f);
    	anim.SetBool("IsWalking", walking);
    }
}
