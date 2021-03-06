using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public enum BattleState
{
    START,
    PLAYER1TURN,
    PLAYER2TURN,
    END
}

public class BattleSystem : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    
    public Transform player1BattleStation;
    public Transform player2BattleStation;

    Unit _player1Unit;
    Unit _player2Unit;

    public Text dialogueText;

    public BattleHUD player1HUD;
    public BattleHUD player2HUD;

    public BattleState state;
    public Unit curPlayer;
    public Unit curEnemy;

    
    public  void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        dialogueText.text = "The game started ...";
    }
    
    
    IEnumerator  SetupBattle()
    {
      
        GameObject player1GO = Instantiate(player1Prefab, player1BattleStation);
        _player1Unit = player1GO.GetComponent<Unit>();

        GameObject player2GO = Instantiate(player2Prefab, player2BattleStation);
        _player2Unit = player2GO.GetComponent<Unit>();

       // _player1Unit.SetEnemy(_player2Unit);
        //_player2Unit.SetEnemy(_player1Unit);

        player1HUD.SetHUD(_player1Unit);
        player2HUD.SetHUD(_player2Unit);
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYER1TURN;
        PlayerTurn();
    }

   

    int RollADice()
    {
        return Random.Range(1, 7);
    }


    IEnumerator PlayerMove1()
    {
        //if (!curPlayer.moveset.Move1())
        {
            yield return new WaitForSeconds(0.1f);
       //     dialogueText.text = curPlayer.moveset.moveName1+" was in Cooldown";
        }
       // else
        {
            SetHubBar();
            ResetText();
            yield return new WaitForSeconds(0.1f);
            dialogueText.text = RollADice().ToString() + "The attack is successful!";
            yield return new WaitForSeconds(2f);
        }

        /*if (!isDead) yield break;
        state = BattleState.END;
        EndBattle();
    }*/
    }

 
    IEnumerator PlayerMove2()
    {
        bool isDead;


        {
           // isDead = false;

          // if (!curPlayer.moveset.Move2())
           {
               yield return new WaitForSeconds(0.1f);
               dialogueText.text = "The Move was in Cd";
           }
         //  else
           {
               SetHubBar();
               ResetText();
               yield return new WaitForSeconds(0.1f);
               dialogueText.text = "The heal is successful!";
           }


           if (curEnemy.currentHp > 0) yield break;
               state = BattleState.END;
               EndBattle();
           
        }
    }
    IEnumerator PlayerMove3()
    {
        bool isDead;


        {
            // isDead = false;

           // if (!curPlayer.moveset.Ultimate())
            {
                yield return new WaitForSeconds(0.1f);
                dialogueText.text = "The ulti was in Cd";
            }
           // else
            {
                SetHubBar();
                ResetText();
                yield return new WaitForSeconds(0.1f);
                dialogueText.text = "The ultimate is successful!";
            }


            if (curEnemy.currentHp > 0) yield break;
            state = BattleState.END;
            EndBattle();
           
        }
    }
    void ResetText()
    {
        dialogueText.text = "";
    }

    void SetHubBar()
    {
        player1HUD.SetUnitData(_player1Unit.currentHp);
        player2HUD.SetUnitData(_player2Unit.currentHp);
    }

    void EndBattle()
    {
        if (state == BattleState.END)
        {
            dialogueText.text = curPlayer.unitName + " won! Fatality!";
        }
    }

    void PlayerTurn()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            curPlayer = _player1Unit;
            curEnemy = _player2Unit;
            
        }
        else
        {
            curPlayer = _player2Unit;
            curEnemy = _player1Unit;
        }

        curPlayer.emeny.isResetTtd = true;
       // curPlayer.moveset.ReduceCd();
        dialogueText.text = curPlayer.unitName + "'s turn!";
    }


    IEnumerator PlayerEndTurn()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            //if(_player2Unit.isResetTtd) _player2Unit.ResetTtd();
            state = BattleState.PLAYER2TURN;
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            //if(_player1Unit.isResetTtd) _player1Unit.ResetTtd(); 
            state = BattleState.PLAYER1TURN;
        }

        PlayerTurn();
        yield return new WaitForSeconds(2f);
    }

    public void OnMove1Button()
    {
        if (state != BattleState.PLAYER1TURN && state != BattleState.PLAYER2TURN) return;
        StartCoroutine(PlayerMove1());
    }


    public void OnEndButton()
    {
        if (state != BattleState.PLAYER1TURN && state != BattleState.PLAYER2TURN) return;
        StartCoroutine(PlayerEndTurn());
    }

    public void OnMove2Button()
    {
        if (state != BattleState.PLAYER1TURN && state != BattleState.PLAYER2TURN) return;
        StartCoroutine(PlayerMove2());
    }
    public void OnMove3Button()
    {
        if (state != BattleState.PLAYER1TURN && state != BattleState.PLAYER2TURN) return;
        StartCoroutine(PlayerMove3());
    }
} 