using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ListarJogadores : MonoBehaviour
{
    public Text[] NomesJogadores;
    public Text[] PontosJogadores;

    void Start()
    {   
        StartCoroutine(listarJogadores());
    }   
    
    IEnumerator listarJogadores()
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("limite", 3);

        UnityWebRequest w = UnityWebRequest.Post("http://localhost/mortal_clicker/listarJogadores.php", wwwf);

        yield return w.SendWebRequest();

        if(w.isHttpError || w.isNetworkError)
        {
            Debug.Log("Erro: " + w.error);
        }
        else
        {
            //Debug.Log("Download: " + w.downloadHandler.text);
            Pontuacao pontuacaoContainer = JsonUtility.FromJson<Pontuacao>(w.downloadHandler.text);

            for (int i = 0; i < pontuacaoContainer.objetos.Length; i++)
            {
                NomesJogadores[i].text = pontuacaoContainer.objetos[i].jogador;
                PontosJogadores[i].text = pontuacaoContainer.objetos[i].pontos.ToString();
            }
        }
        
    }
}
