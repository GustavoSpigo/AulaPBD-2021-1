using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InserirJogador : MonoBehaviour
{
    public InputField infLogin;
    public InputField infSenha;
    public InputField infSenhaConfirmacao;
    public InputField infNome;
    public InputField infDtNascimento;
    public Dropdown ddwGenero;
    public InputField infNacionalidade;
    public Dropdown ddwPreferencia;

    public Text txtMsg;
    public bool ValidarCamposJogador()
    {
        txtMsg.text = string.Empty;
        if (!infLogin.text.Length.Equals(3))
        {
            txtMsg.text = "O login precisa ter 3 caracteres";
            return false;
        }

        if (!infSenha.text.Equals(infSenhaConfirmacao.text))
        {
            txtMsg.text = "Senhas não são iguais";
            return false;
        }

        if (infSenha.text.Length < 4)
        {
            txtMsg.text = "Senha tem que possuir pelo menos 4 caracteres";
            return false;
        }

        DateTime dt_nascimento;
        if(!DateTime.TryParse(infDtNascimento.text, out dt_nascimento)){
            txtMsg.text = "Data não é válida";
            return false;
        }

        return true;
    }

    public void btnCadastrarClicar()
    {
        if (ValidarCamposJogador())
        {
            StartCoroutine(AdicionarJogador());
        }
    }
    IEnumerator AdicionarJogador()
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("login", infLogin.text);
        wwwf.AddField("senha", infSenha.text);
        wwwf.AddField("nome", infNome.text);
        wwwf.AddField("dt_nascimento", infDtNascimento.text);
        wwwf.AddField("genero", ddwGenero.options[ddwGenero.value].text) ;
        wwwf.AddField("nacionalidade", infNacionalidade.text);
        wwwf.AddField("preferencia", ddwPreferencia.options[ddwPreferencia.value].text);

        UnityWebRequest w = UnityWebRequest.Post("http://localhost/mortal_clicker/inserirJogador.php", wwwf);

        infLogin.enabled = false;

        yield return w.SendWebRequest();

        infLogin.enabled = true;

        if (w.isHttpError || w.isNetworkError)
        {
            Debug.Log("Erro: " + w.error);
        }
        else
        {
            Debug.Log(w.downloadHandler.text);
            if(w.downloadHandler.text.Equals("gatoNao"))
            {
                txtMsg.text = "Esse jogo não aceita preferência por gatos";
            }

            if (w.downloadHandler.text.Equals("LoginExiste"))
            {
                txtMsg.text = "Esse login já existe, escolha outro";
            }

            if (w.downloadHandler.text.Equals("DeuBom"))
            {
                txtMsg.text = "Que deliciaaaa cara, deu bom";
                PlayerPrefs.SetString("Login", infLogin.text);
                PlayerPrefs.SetString("Nome", infNome.text);
                SceneManager.LoadScene("Jogo");
            }
        }
    }
}
