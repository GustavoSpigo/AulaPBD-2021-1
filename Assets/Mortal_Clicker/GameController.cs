using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    private int pontos = 0;
    public Text txtMsg;

    public Button SoVai;
    public Button Desistir;
    public void BtnVai()
    {
        if(Random.Range(1f, 20f) < 19)
        {
            pontos += 1;
        }
        else
        {
            pontos = 0;
        }

        AtualizaMensagem();
    }
    private void AtualizaMensagem()
    {
        if(pontos == 0)
        {
            txtMsg.text = "Perdeu playboy...";
        }
        else
        {
            txtMsg.text = "Sua pontuação é de " + pontos;
        }
    }
    public void BtnDesistir()
    {
        if(pontos > 0)
        {
            StartCoroutine(AdicionarPontuacao());
            pontos = 0;
            txtMsg.text = "Registrando sua pontuação...";
            SoVai.enabled = false;
            Desistir.enabled = false;
        }
        else
        {
            txtMsg.text = "Começa aí primeiro... só vai...";
        }
    }
    IEnumerator AdicionarPontuacao()
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("jogador", "ABC");
        wwwf.AddField("pontos", pontos);

        UnityWebRequest w = UnityWebRequest.Post("http://localhost/mortal_clicker/inserirPontucao.php", wwwf);

        yield return w.SendWebRequest();

        if (w.isHttpError || w.isNetworkError)
        {
            Debug.Log("Erro: " + w.error);
        }
        else
        {
            if (w.downloadHandler.text.Equals("Ok"))
            {
                Desistir.enabled = true;
                SoVai.enabled = true;

                txtMsg.text = "Sua pontuação foi salva";
            }
            else if(w.downloadHandler.text.Equals("Pontuacao menor, mas Ok"))
            {
                Desistir.enabled = true;
                SoVai.enabled = true;

                txtMsg.text = "Nem salvei hein, faça melhor...";
            }
        }
    }
}
