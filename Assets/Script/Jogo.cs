    using System.Collections;
using System.Diagnostics;
using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Jogo : MonoBehaviour
    {
        [Header("Configurações de Velocidade")]
        public float modificadorDeVelocidade = 1f;
        public float velocidade = 4.5f;
        public float velocidadeMaxima = 10f;

        [HideInInspector]
        public bool Rodando = true;

        private void Start()
        {
            Time.timeScale = 1;
            Rodando = true;
        }

        private void Update()
        {
            if (!Rodando || velocidade <= 0) return;

            velocidade = Mathf.Clamp(
                velocidade + modificadorDeVelocidade * Time.deltaTime,
                0,
                velocidadeMaxima
            );
        }

        public void ReiniciarJogo()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void FimDeJogo()
        {
            Rodando = false;
        }
    }
