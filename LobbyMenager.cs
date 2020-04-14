using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LobbyMenager : MonoBehaviourPunCallbacks
{
    //Login UI 
    [Header("Login UI")]
    public InputField playerNameInputField;
    public GameObject uI_LoginGameObject;

    [Header("Lobby UI")]
    public GameObject uI_LobbyGameobject;
    public GameObject uI_3DGameobject;

    [Header(" Connection Status UI")]
    public GameObject uI_ConnectionStatusGameobject;
    public Text connectionStatusText;
    public bool showConnectionStatus = false;


    #region UNITY Methods
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {

            //Activating only Lobby UI
            uI_LobbyGameobject.SetActive(true);
            uI_3DGameobject.SetActive(true);
            uI_ConnectionStatusGameobject.SetActive(false);

            uI_LoginGameObject.SetActive(false);
        }
        else
        {
            //Activating only Login UI sincewe didnt connect to Photon yet.
            uI_LobbyGameobject.SetActive(false);
            uI_3DGameobject.SetActive(false);
            uI_ConnectionStatusGameobject.SetActive(false);

            uI_LoginGameObject.SetActive(true);
        }
    }

    void Update()
    //Connection Status
    //If connection Status is true we activate connection  status UI
    {
        if (showConnectionStatus)
        {
            connectionStatusText.text = "Connection Status" + PhotonNetwork.NetworkClientState;
        }
    }
    #endregion

     
    #region UI Callback Methods

    //Connect to the serwer with using Nickname
    public void onEnterGameButtonClicked()
    {



        //We introduce a user nick
        string playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            //Change scene where username log in to the serwer
            uI_LobbyGameobject.SetActive(false);
            uI_3DGameobject.SetActive(false);
            uI_ConnectionStatusGameobject.SetActive(true);

            showConnectionStatus = true;
            uI_LoginGameObject.SetActive(false);

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }

        }
        else
        {
            Debug.Log("Player name is invalid or empty");
        }
        
    }

    public void onQuickMatchButtonClicked()
    {
        //Load Scene
        // SceneManager.LoadScene("Scene_Loading");
        SceneLoader.Instance.LoadScene("Scene_PlayerSelection");
    }

    #endregion

    #region PHOTON Callback Methods
    //Connect to the Photon Serwer
    public override void OnConnected()
    {
        Debug.Log("We connected to Internet");

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName = "is connected to the Photon Serwer");
        //Next change scene after connect we show game menu 
        uI_LobbyGameobject.SetActive(true);
        uI_3DGameobject.SetActive(true);
        uI_ConnectionStatusGameobject.SetActive(false);

        uI_LoginGameObject.SetActive(false);
    }

    #endregion
}
