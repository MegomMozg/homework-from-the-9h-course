using Assets.Scripts.Abstractions;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        public int FactionId => _factionId;
        [SerializeField] private int _factionId;

        public void SetFaction(int factionId)
        {
            _factionId = factionId;
        }
    }
}