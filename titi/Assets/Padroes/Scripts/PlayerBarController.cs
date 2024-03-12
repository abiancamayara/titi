using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manda o conteudo
public class PlayerBarController : MonoBehaviour
{
    public int coin;
    
    // Start is called before the first frame update
    void Update()
    {
        Space();
    }
    
    private void CoinPlayerChanged(int valuePlayer)
    {
        //Manda o video para o youtube
        PlayerObserverManager.PlayerChanged(valuePlayer);
    }

    public void Space()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coin = coin + 1;
            CoinPlayerChanged(coin);
        }
        
        Debug.Log(coin);
    }
}
