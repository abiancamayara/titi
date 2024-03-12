using System;

//cria o youtube
public static class AudioObserverManager
{
    // permite que um usuario se inscreva no canal (analogia do youtube)
    public static event Action<float> OnVolumeChanged;
    
    // permite que o criador de conteudo mande um video novo
    public static void VolumeChanged(float volume)
    {
        // permite que os escritos recebam a notificação
        OnVolumeChanged?.Invoke(volume);
    }
}
