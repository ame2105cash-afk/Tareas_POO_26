using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBola : MonoBehaviour
{
    public Transform CamaraPrincipal;
    public Rigidbody rb;

    //Variables para apuntar y limitar
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = -2f;
    public float limiteDerecho = 2f;


    public float fuerzaDeLanzamiento = 1000f;

    private bool haSidoLanzada = false;
    //if aninado, controlan otros 

    // Velocidad de suavizado de la cámara
    public float suavizadoCamara = 0.15f;
    public Vector3 offsetCamara = new Vector3(0, 2f, -6f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (haSidoLanzada == false)
        {
            Apuntar();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Lanzar();
            }
        }
        else
        {
            SeguirConCamara();
        }
    }

    void Apuntar()
    {
        //1.Leer un input Horizontal de tipo Axis, te pernite registrar
        //entradas con las teclas A y D y Flecha izquierda y Flecha derecha
        float inputHorizontal = Input.GetAxis("Horizontal");
        //2. Mover la bola hacia los lados
        transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);
        //3. Delimitar el movimiento de la bola
        Vector3 posicionActual = transform.position;
        //transform.position me perimte saber cual es la posicion actual de la escena
        posicionActual.x=Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);
        transform.position = posicionActual;//se coloca de nuevo pq se actualiza la posicion
    }

    void Lanzar()// empieza un metodo
    {
        haSidoLanzada = true;
        rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);
    }

    void SeguirConCamara()
    {
        if (CamaraPrincipal != null)
        {
            // Posición deseada de la cámara (detrás de la bola con offset)
            Vector3 posicionDeseada = transform.position + offsetCamara;

            // Movimiento suave con Lerp
            CamaraPrincipal.position = Vector3.Lerp(CamaraPrincipal.position, posicionDeseada, suavizadoCamara);

            // Mirar hacia la bola
            CamaraPrincipal.LookAt(transform.position);
        }
    }

}//Bienvenido a la entrada al infierno
