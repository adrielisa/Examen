using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDuelController : MonoBehaviour
{
    public bool esJugadorAzul = true;
    public float velocidad = 6f;

    private Rigidbody2D rb;
    private Vector2 direccion = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var teclado = Keyboard.current;
        if (teclado == null) return;

        if (teclado.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        direccion = Vector2.zero;

        if (esJugadorAzul)
        {
            if (teclado.wKey.isPressed) Mover(Vector2.up, 90f);
            else if (teclado.sKey.isPressed) Mover(Vector2.down, -90f);
            else if (teclado.aKey.isPressed) Mover(Vector2.left, 180f);
            else if (teclado.dKey.isPressed) Mover(Vector2.right, 0f);
        }
        else
        {
            if (teclado.upArrowKey.isPressed) Mover(Vector2.up, 90f);
            else if (teclado.downArrowKey.isPressed) Mover(Vector2.down, -90f);
            else if (teclado.leftArrowKey.isPressed) Mover(Vector2.left, 180f);
            else if (teclado.rightArrowKey.isPressed) Mover(Vector2.right, 0f);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direccion * velocidad;
    }

    void Mover(Vector2 nuevaDireccion, float anguloZ)
    {
        direccion = nuevaDireccion;
        transform.eulerAngles = new Vector3(0, 0, anguloZ);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WeakSpot") && other.transform.parent != this.transform)
        {
            if (esJugadorAzul)
                Debug.Log("¡GANÓ EL JUGADOR AZUL! presiona R para reiniciar");
            else
                Debug.Log("¡GANÓ EL JUGADOR ROJO! presiona R Para reiniciar");

            Time.timeScale = 0f;
        }
    }
}