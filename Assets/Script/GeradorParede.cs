using System.Collections;
using UnityEngine;

public class GeradorParede : MonoBehaviour
{
    public GameObject paredePrefab;

    public float delayInicial = 1f;

    public float distanciaMinima = 4;
    public float distanciaMaxima = 8;

    private void Start()
    {
        StartCoroutine(GerarParede());
    }

    private float distanciaNecessaria;
    private float distanciaAtual;

    private IEnumerator GerarParede()
    {
        yield return new WaitForSeconds(delayInicial);

        GameObject ultimoInimigoGerado = null;

        while (true)
        {
            bool podeGerar = ultimoInimigoGerado == null ||
                Vector3.Distance(transform.position, ultimoInimigoGerado.transform.position) >= distanciaNecessaria;

            if (podeGerar)
            {
                distanciaNecessaria = Random.Range(distanciaMinima, distanciaMaxima);

                ultimoInimigoGerado = Instantiate(paredePrefab, transform.position, Quaternion.identity);

                // pequena pausa após gerar
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
            {
                // evita loop infinito travar o jogo
                yield return null;
            }
        }
    }
}
