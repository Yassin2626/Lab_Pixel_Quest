using UnityEngine;

public class HW2PlayerMovement : MonoBehaviour
{
    // Movement Controls
    private Rigidbody2D _rigidbody2D;
    private float xSpeed;
    private float ySpeed;
    public float speed = 3f;

    // Input Axis Names
    private string inputX = "Horizontal";
    private string inputY = "Vertical";

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xSpeed = Input.GetAxis(inputX);
        ySpeed = Input.GetAxis(inputY);

        _rigidbody2D.velocity = new Vector2(xSpeed, ySpeed) * speed;
    }
}
