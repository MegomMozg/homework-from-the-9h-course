using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class TimeModel : ITimeModel, ITickable
    {
        public IObservable<int> GameTime => _gameTime.Select(f => (int)f);
        private readonly ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}