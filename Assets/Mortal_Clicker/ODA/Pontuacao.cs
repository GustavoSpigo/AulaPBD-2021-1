[System.Serializable]
public struct Pontuacao
{
    [System.Serializable]
    public struct LinhaPontuacao
    {
        public string jogador;
        public int pontos;
    }

    public LinhaPontuacao[] objetos;
}