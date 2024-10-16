using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Obtén la entrada del jugador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Crea un vector de movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Mueve el objeto
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}