using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using System;

public class SoundManager : PersistentSingleton<SoundManager>
{
    IObjectPool<SoundEmitter> _soundEmitterPool;
    readonly List<SoundEmitter> _soundEmitters = new();
    public readonly Dictionary<SoundData, int> Counts = new();

    [SerializeField] SoundEmitter _soundEmitterPrefab;
    [SerializeField] bool _collectionCheck = true;
    [SerializeField] int _defaultCapacity = 10;
    [SerializeField] int _maxPoolSize = 100;
    [SerializeField] int _maxSoundInstances = 30;

    private void Start()
    {
        InitializeSoundEmitterPool();
    }

    public SoundBuilder CreateSound() => new SoundBuilder(this);

    public void ReturnToPool(SoundEmitter soundEmitter)
    {
        _soundEmitterPool.Release(soundEmitter);
    }

    public bool CanPlaySound(SoundData soundData)
    {
        return !Counts.TryGetValue(soundData, out int count) || count < _maxSoundInstances;
    }

    private SoundEmitter CreateSoundEmitter()
    {
        var soundEmitter = Instantiate(_soundEmitterPrefab);

        soundEmitter.gameObject.SetActive(false);

        return soundEmitter;
    }

    private void OnTakeFromPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(true);

        _soundEmitters.Add(emitter);
    }

    private void OnDestroyPoolObject(SoundEmitter emitter)
    {
        Destroy(emitter.gameObject);
    }

    private void OnReturnToPool(SoundEmitter emitter)
    {
        if(Counts.TryGetValue(emitter.SoundData, out int count))
        {
            Counts[emitter.SoundData] -= count > 0 ? 1 : 0;
        }

        emitter.gameObject.SetActive(false);

        _soundEmitters.Remove(emitter);
    }

    private void InitializeSoundEmitterPool()
    {
        _soundEmitterPool = new ObjectPool<SoundEmitter>(
            CreateSoundEmitter, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, _collectionCheck, _defaultCapacity, _maxPoolSize);
    }

    public SoundEmitter GetSoundEmitter()
    {
        return _soundEmitterPool.Get();
    }
}
