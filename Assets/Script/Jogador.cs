using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    [Header("Componentes")]
    public Rigidbody2D rb;

    [Header("Pulo")]
    public float forcaPulo = 12f;
    public LayerMask layerChao;
    public float distanciaMinimaChao = 0.3f; 
    private bool estaNoChao;

    [Header("Pontuação")]
    private float pontos;
    private float highScore;
    public float multiplicadorPontos = 1;

    public Text pontosText;
    public Text highScoreText;

    [Header("Elos")]
    public Image eloImg;
    public int[] pontosParaElo = { 0, 100, 300, 600, 1000, 1500, 2100, 2800, 3600 };

    [Header("Áudios")]
    public AudioSource somPulo;
    public AudioSource cemAudioSource;
    public AudioSource fimDeJogoAudioSource;

    [Header("Game Over")]
    public GameObject restartButton;

    void Start()
    {
        // Garantir que o jogo sempre comece rodando
        Time.timeScale = 1f;

        // Carregar HighScore corretamente
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = $"HI {Mathf.FloorToInt(highScore)}";

        // Garantir que o botão esteja desativado no início
        if (restartButton != null)
            restartButton.SetActive(false);
    }

    void Update()
    {
        // Atualizar pontuação
        pontos += Time.deltaTime * multiplicadorPontos;
        int pontosArredondados = Mathf.FloorToInt(pontos);

        pontosText.text = pontosArredondados.ToString();

        // Som a cada 100 pontos
        if (pontosArredondados > 0 &&
            pontosArredondados % 100 == 0 &&
            !cemAudioSource.isPlaying)
        {
            cemAudioSource.Play();
        }

        AtualizarElo();

        // Pular
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pular();
        }
    }

    private void FixedUpdate()
    {
        // Debug para ver o Raycast no editor
        Debug.DrawRay(transform.position, Vector2.down * distanciaMinimaChao, Color.red);

        // Detectar chão
        estaNoChao = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            distanciaMinimaChao,
            layerChao
        );
    }

    void Pular()
    {
        if (estaNoChao)
        {
            rb.AddForce(Vector2.up * forcaPulo);
            somPulo.Play();
        }
    }

    void AtualizarElo()
    {
        int scoreAtual = Mathf.FloorToInt(pontos);

        for (int i = pontosParaElo.Length - 1; i >= 0; i--)
        {
            if (scoreAtual >= pontosParaElo[i])
            {
                Sprite novoElo = Resources.Load<Sprite>("Elos/Elo" + (i + 1));

                if (novoElo != null)
                    eloImg.sprite = novoElo;
                else
                    Debug.LogWarning("Sprite não encontrado: Elo" + (i + 1));

                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("Inimigo"))
        {
            // Atualizar HighScore
            if (pontos > highScore)
            {
                highScore = pontos;
                PlayerPrefs.SetFloat("HighScore", highScore);
            }

            // Som de morte
            fimDeJogoAudioSource.Play();

            // Mostrar botão de reiniciar
            restartButton.SetActive(true);

            // Pausar o jogo
            Time.timeScale = 0f;
        }
    }
}
