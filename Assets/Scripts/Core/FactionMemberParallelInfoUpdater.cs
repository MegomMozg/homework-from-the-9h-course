using Assets.Scripts.Abstractions;
using UnityEngine;
using Zenject;

namespace Core
{
    public class FactionMemberParallelInfoUpdater : MonoBehaviour, ITickable
    {
        [Inject] private IFactionMember _factionMember;

        public void Tick()
        {
            AutoAttackEvaluator.FactionMembersInfo.AddOrUpdate(
                gameObject
                , new AutoAttackEvaluator.FactionMemberParallelInfo(transform.position, _factionMember.FactionId)
                , (go, value) =>
                {
                    value.Position = transform.position;
                    value.Faction = _factionMember.FactionId;
                    return value;
                });
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.FactionMembersInfo.TryRemove(gameObject, out _);
        }
    }
}