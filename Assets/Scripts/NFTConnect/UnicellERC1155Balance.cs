using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class UnicellERC1155Balance: MonoBehaviour
{
    public string tokenId = "38943131031766143704984983154691040388593436270428817556432674370870428303370";

    public bool isUnicellAvailable;

    async void Start()
    {
       
            string chain = "polygon";
            string network = "testnet";
            string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963";
            string account = PlayerPrefs.GetString("Account");
            string tokenId = "38943131031766143704984983154691040388593436270428817556432674370870428303370";

            BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
            print(balanceOf);

            if (balanceOf > 0)
        
         isUnicellAvailable = true;

        
        else isUnicellAvailable = false;
      
    }
}