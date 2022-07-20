
using Assets.Scripts.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactionControlModel
{
    public event Action<int> OnWin;

    private Dictionary<int, List<IFactionMember>> _factionsCount;

    public FactionControlModel()
    {
        _factionsCount = new Dictionary<int, List<IFactionMember>>();
    }

    public void Add(IFactionMember factionMember)
    {
        lock (_factionsCount)
        {
            List<IFactionMember> factionList;
            if (_factionsCount.ContainsKey(factionMember.FactionId))
            {
                factionList = _factionsCount[factionMember.FactionId];
            }
            else
            {
                factionList = new List<IFactionMember>();
                _factionsCount.Add(factionMember.FactionId, factionList);
            }
            if (!factionList.Contains(factionMember))
            {
                factionList.Add(factionMember);
            }
        }
    }

    public void Remove(IFactionMember factionMember)
    {
        lock (_factionsCount)
        {
            if (_factionsCount.ContainsKey(factionMember.FactionId))
            {
                List< IFactionMember> factionList = _factionsCount[factionMember.FactionId];
                factionList.Remove(factionMember);
                if (factionList.Count == 0)
                {
                    _factionsCount.Remove(factionMember.FactionId);
                }
            }
        }
    }

    public void CheckStatus(object state)
    {
        lock (_factionsCount)
        {
            if (_factionsCount.Count == 1)
            {
                OnWin?.Invoke(_factionsCount.First().Key);
            }
        }
    }
}
