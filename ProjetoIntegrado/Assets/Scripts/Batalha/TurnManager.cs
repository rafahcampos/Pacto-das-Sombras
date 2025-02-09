using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private ActionsManager Actions;

    [SerializeField] private SliderManager playerManager;
    [SerializeField] private SliderManager enemyManager;

    [SerializeField] private EffectManagerScript effectManager;

    [SerializeField] private TMP_Text turn;

    private void Start()
    {
        TextTurn("Jogador");
    }

    private void Update()
    {
        if (playerManager.GetValue(playerManager.hpSlider) == 0 || enemyManager.GetValue(enemyManager.hpSlider) == 0)
        {
            GameManagerScript player = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

            Debug.Log("Batalha: " + playerManager.GetValue(playerManager.manaSlider));
            player.BattleOver(playerManager.GetValue(playerManager.hpSlider), playerManager.GetValue(playerManager.manaSlider));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);


        }

        if (Actions.hasTurn == false)
        {
            TextTurn("Inimigo");
            Disable();
            Invoke("EnemyAction", 2.0f);
            //Actions.hasTurn = true;
        }
    }

    public void TextTurn(string text)
    {
        turn.SetText("Turno: " + text);
    }

    public void EnemyAction()
    {
        if (Actions.hasTurn == false)
        {
            if (Actions.defenseActive == true)
            {
                effectManager.setDefense(true);
                Actions.AttackButton(false);
                effectManager.setDefense(false);
                Actions.defenseActive = false;
                
            }
            else
            {
                Actions.AttackButton(false);
            }

            
        }
        Enable();
        TextTurn("Jogador");
        Actions.hasTurn = true;
    }

    public void Disable()
    {
        playerInput.currentActionMap.Disable();
    }

    public void Enable()
    {
        playerInput.currentActionMap.Enable();
    }
}