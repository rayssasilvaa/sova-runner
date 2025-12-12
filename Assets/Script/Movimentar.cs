using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentar : MonoBehaviour
{
    // Direção do movimento
    public Vector2 direcao;
    private Jogo jogoScript;

    private void Start()
    {
        GameObject obj = GameObject.Find("Jogo");

        if (obj != null)
        {
            jogoScript = obj.GetComponent<Jogo>();

            if (jogoScript == null)
                Debug.LogError("O objeto 'Jogo' foi encontrado, mas NÃO tem o script Jogo!");
        }
        else
        {
            Debug.LogError("O objeto 'Jogo' NÃO foi encontrado na cena!");
        }
    }

    private void Update()
    {
        // Evita crash se o script não for encontrado
        if (jogoScript == null)
            return;

        // Movimento baseado na velocidade
        transform.Translate(direcao * jogoScript.velocidade * Time.deltaTime);
    }
}
