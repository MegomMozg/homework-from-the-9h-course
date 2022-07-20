using Assets.Scripts.Abstractions;
using UnityEngine;
using Zenject;

namespace UserControlSystem.UI.View
{
    public class FactionView : MonoBehaviour, IFactionMember
    {
        public int FactionId => _factionId;
        [SerializeField] private int _factionId;
        [Inject] private FactionControlModel _factionControlModel;

        private void Awake()
        {
            _factionControlModel.Add(this);
        }

        public void SetFaction(int factionId)
        {
            _factionControlModel.Remove(this);
            _factionId = factionId;
            _factionControlModel.Add(this);
        }

        private void OnDestroy()
        {
            _factionControlModel.Remove(this);
        }
    }
}