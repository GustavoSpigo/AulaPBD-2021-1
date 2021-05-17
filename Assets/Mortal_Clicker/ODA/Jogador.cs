[System.Serializable]
public struct Jogador
{
    [System.Serializable]
    public struct LinhaJogador
    {
        public string login;
        public string senha;
    }
    public LinhaJogador[] objetos;
}