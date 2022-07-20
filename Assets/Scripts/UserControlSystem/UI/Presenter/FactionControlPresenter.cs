using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class FactionControlPresenter : MonoBehaviour
{
    [Inject] FactionControlModel _factionControlModel;
    private bool _activeChecking;
    [SerializeField] private GameObject _winPrefab;
    private bool _instantiateWinCanvas = false;
    private int _winFaction = 0;

    private void Awake()
    {
        _factionControlModel.OnWin += OnWin;
        _activeChecking = true;
    }

    private void Update()
    {
        if (_activeChecking)
            ThreadPool.QueueUserWorkItem(_factionControlModel.CheckStatus);
        if (_instantiateWinCanvas)
            InstantiateWinCanvas();
    }

    private void OnDestroy()
    {
        _factionControlModel.OnWin -= OnWin;
    }

    private void OnWin(int factionId)
    {
        _activeChecking = false;
        _instantiateWinCanvas = true;
        _winFaction = factionId;
    }

    private void InstantiateWinCanvas()
    {
        _instantiateWinCanvas = false;

        GameObject go = Instantiate(_winPrefab);
        TextMeshProUGUI textMeshPro = go.GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro != null)
            textMeshPro.text = $"Faction {_winFaction} Win";
    }
}
