using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;


public class BattleScripts : MonoBehaviour
{
    public Spinner spinnerScripts;

    private float startSpinnerSpeed;
    private float currentSpinnerSpeed;
    public Image spinnerSpeedBar_Image;
    public TextMeshProUGUI spinSpeedRatio_Text;

    public float common_Damage_Coefficient = 0.04f;

    [Header("Player Type Damage Coefficients")]
    public float doDamage_Coefficient_Attacker = 10f;//do more dmg than defender
    public float getDamage_Cofficient_Attacker = 1.2f;// but get more dmg

    public float doDamage_Coefficient_Defender = 0.75f;//do less dmg than attacker
    public float getDamage_Cofficient_Defender = 0.2f;// but get less dmg

    public bool isAttacker;
    public bool isDefender;




    private void Awake()
    {
        startSpinnerSpeed = spinnerScripts.spinSpeed;
        currentSpinnerSpeed = spinnerScripts.spinSpeed;

        spinnerSpeedBar_Image.fillAmount = currentSpinnerSpeed / startSpinnerSpeed;
    }

    private void CheckPlayerType()
    {
        if (gameObject.name.Contains("Attacker"))
        {
            isAttacker = true;
            isDefender = false;
        }else if (gameObject.name.Contains("Defender"))
        {
            isAttacker = false;
            isDefender = true;

            spinnerScripts.spinSpeed = 4400f;
            startSpinnerSpeed = spinnerScripts.spinSpeed;
            currentSpinnerSpeed = spinnerScripts.spinSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Comparing the speed of the Spinner
            float mySpeed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            float otherPlayerSpeed = collision.collider.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log("My speed" + mySpeed + "...other player speed" + otherPlayerSpeed);

            if (mySpeed > otherPlayerSpeed)
            {
                Debug.Log("You damage the other player");

                float default_Damage_Ammount = gameObject.GetComponent<Rigidbody>().velocity.magnitude * 3600 * common_Damage_Coefficient;
                if (isAttacker)
                {
                    default_Damage_Ammount *= doDamage_Coefficient_Attacker;
                }
                else if (isDefender)
                {
                    default_Damage_Ammount *= doDamage_Coefficient_Defender;
                }

                if (collision.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {

                    //Apply Damage to the slower player
                    collision.collider.gameObject.GetComponent<PhotonView>().RPC("DoDamage", RpcTarget.AllBuffered, default_Damage_Ammount);
                }

            }

        }
        
    }

    [PunRPC]
    public void DoDamage(float _damageAmount)
    {

        if (isAttacker)
        {
            _damageAmount *= getDamage_Cofficient_Attacker;
        }else if (isDefender)
        {
            _damageAmount *= getDamage_Cofficient_Defender;
        }

        spinnerScripts.spinSpeed -= _damageAmount;
        currentSpinnerSpeed = spinnerScripts.spinSpeed;

        spinnerSpeedBar_Image.fillAmount = currentSpinnerSpeed / startSpinnerSpeed;
        spinSpeedRatio_Text.text = currentSpinnerSpeed.ToString("F0") + "/" + startSpinnerSpeed;

    }

    void Start()
    {
        CheckPlayerType();
    }

    void Update()
    {
        
    }
}
